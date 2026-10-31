using System.Security.Claims;
using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HelpDesk.Controllers;

[Authorize]
public class TicketController(AppDbContext db) : Controller
{
    bool IsIt => User.IsInRole("ITSpecialist") || User.IsInRole("Admin");
    int Uid => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public async Task<IActionResult> Index(Category? category, TicketStatus? status)
    {
        var q = db.Tickets.Include(t => t.CreatedBy).AsQueryable();
        if (!IsIt) q = q.Where(t => t.CreatedById == Uid);
        if (category != null) q = q.Where(t => t.Category == category);
        if (status != null) q = q.Where(t => t.Status == status);
        return View(await q.OrderByDescending(t => t.CreatedAt).ToListAsync());
    }

    public IActionResult Create() => View(new Ticket());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,Category,Priority")] Ticket t)
    {
        if (!ModelState.IsValid) return View(t);
        t.Title = t.Title.Trim(); t.Description = t.Description.Trim();
        t.CreatedById = Uid; t.CreatedAt = DateTime.UtcNow; t.Status = TicketStatus.New;
        db.Tickets.Add(t); await db.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id = t.Id });
    }

    public async Task<IActionResult> Details(int id)
    {
        var t = await db.Tickets.Include(x => x.CreatedBy).Include(x => x.AssignedTo).FirstOrDefaultAsync(x => x.Id == id);
        if (t == null) return NotFound();
        if (!IsIt && t.CreatedById != Uid) return Forbid();
        return View(t);
    }

    [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "ITSpecialist,Admin")]
    public async Task<IActionResult> ChangeStatus(int id, TicketStatus status)
    {
        var t = await db.Tickets.FindAsync(id);
        if (t == null) return NotFound();
        t.Status = status; t.UpdatedAt = DateTime.UtcNow;
        if (status == TicketStatus.InProgress && t.AssignedToId == null) t.AssignedToId = Uid;
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id });
    }
}
