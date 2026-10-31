using HelpDesk.Models;
using Microsoft.AspNetCore.Identity;
namespace HelpDesk.Data;
public static class Seed
{
    public static void Run(AppDbContext db)
    {
        var h = new PasswordHasher<AppUser>();
        void Ensure(string name, string email, string pw, string role)
        {
            var u = db.Users.FirstOrDefault(x => x.Email == email);
            if (u == null) { u = new AppUser { FullName = name, Email = email, Role = role }; db.Users.Add(u); }
            if (u.PasswordHash == "" || u.PasswordHash == "CHANGE_ME") u.PasswordHash = h.HashPassword(u, pw);
            db.SaveChanges();
        }
        Ensure("IT Mütəxəssis", "it@uni.az", "It12345!", "ITSpecialist");
        Ensure("Test Tələbə", "student@uni.az", "Student123!", "Student");
        if (!db.Tickets.Any())
        {
            var s = db.Users.First(u => u.Email == "student@uni.az").Id;
            db.Tickets.AddRange(
                new Ticket { Title = "Wi-Fi qoşulmur", Description = "Kitabxanada Wi-Fi şəbəkəsinə qoşula bilmirəm.", Category = Category.Network, Priority = Priority.High, CreatedById = s },
                new Ticket { Title = "Parol sıfırlama", Description = "Tələbə portalının parolunu unutmuşam.", Category = Category.Account, Priority = Priority.Medium, CreatedById = s, Status = TicketStatus.Resolved },
                new Ticket { Title = "Printer işləmir", Description = "204-cü otaqdakı printer kağızı tutur.", Category = Category.Hardware, Priority = Priority.Low, CreatedById = s, Status = TicketStatus.Closed });
            db.SaveChanges();
        }
    }
}
