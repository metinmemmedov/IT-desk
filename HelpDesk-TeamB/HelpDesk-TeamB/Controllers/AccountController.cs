using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HelpDesk.Controllers;

public class LoginVm
{
    [Required(ErrorMessage = "Email mütləqdir"), EmailAddress(ErrorMessage = "Email formatı yanlışdır")] public string Email { get; set; } = "";
    [Required(ErrorMessage = "Parol mütləqdir"), DataType(DataType.Password)] public string Password { get; set; } = "";
}
public class RegisterVm
{
    [Required(ErrorMessage = "Ad mütləqdir"), StringLength(100, MinimumLength = 3)] public string FullName { get; set; } = "";
    [Required, EmailAddress(ErrorMessage = "Email formatı yanlışdır")] public string Email { get; set; } = "";
    [Required, DataType(DataType.Password), MinLength(8, ErrorMessage = "Parol ən azı 8 simvol olmalıdır")]
    [RegularExpression(@"^(?=.*\d)(?=.*[A-Za-z]).+$", ErrorMessage = "Parolda həm hərf, həm rəqəm olmalıdır")] public string Password { get; set; } = "";
    [Compare(nameof(Password), ErrorMessage = "Parollar uyğun gəlmir"), DataType(DataType.Password)] public string ConfirmPassword { get; set; } = "";
}

public class AccountController(AppDbContext db) : Controller
{
    readonly PasswordHasher<AppUser> _h = new();

    [HttpGet] public IActionResult Login(string? returnUrl) { ViewBag.ReturnUrl = returnUrl; return View(new LoginVm()); }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm vm, string? returnUrl)
    {
        if (!ModelState.IsValid) return View(vm);
        var email = vm.Email.Trim().ToLower();
        var u = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (u == null || _h.VerifyHashedPassword(u, u.PasswordHash, vm.Password) == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("", "Email və ya parol yanlışdır");
            return View(vm);
        }
        var id = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()),
            new Claim(ClaimTypes.Name, u.FullName),
            new Claim(ClaimTypes.Role, u.Role)
        }, "Cookies");
        await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(id));
        return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl!) : RedirectToAction("Index", "Ticket");
    }

    [HttpGet] public IActionResult Register() => View(new RegisterVm());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var email = vm.Email.Trim().ToLower();
        if (await db.Users.AnyAsync(x => x.Email == email)) { ModelState.AddModelError("Email", "Bu email artıq qeydiyyatdadır"); return View(vm); }
        var u = new AppUser { FullName = vm.FullName.Trim(), Email = email, Role = "Student" }; // IT rolunu yalnız admin verir
        u.PasswordHash = _h.HashPassword(u, vm.Password);
        db.Users.Add(u); await db.SaveChangesAsync();
        return RedirectToAction(nameof(Login));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() { await HttpContext.SignOutAsync("Cookies"); return RedirectToAction("Index", "Home"); }
}
