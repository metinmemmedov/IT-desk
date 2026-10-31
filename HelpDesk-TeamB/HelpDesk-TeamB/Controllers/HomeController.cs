using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HelpDesk.Controllers;
public class HomeController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var counts = await db.Tickets.GroupBy(t => t.Status).Select(g => new { g.Key, N = g.Count() }).ToListAsync();
        var d = Enum.GetValues<TicketStatus>().ToDictionary(s => s, s => counts.FirstOrDefault(c => c.Key == s)?.N ?? 0);
        ViewBag.Total = d.Values.Sum();
        return View(d);
    }
}
