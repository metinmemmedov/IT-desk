using System.ComponentModel.DataAnnotations;
namespace HelpDesk.Models;
public class AppUser
{
    public int Id { get; set; }
    [Required, StringLength(100)] public string FullName { get; set; } = "";
    [Required, EmailAddress, StringLength(150)] public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    /// <summary>Student | Staff | ITSpecialist | Admin</summary>
    public string Role { get; set; } = "Student";
}
