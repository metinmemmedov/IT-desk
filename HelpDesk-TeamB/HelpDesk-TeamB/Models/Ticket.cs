using System.ComponentModel.DataAnnotations;
namespace HelpDesk.Models;
public enum Category { Hardware, Software, Network, Account, Other }
public enum Priority { Low, Medium, High, Critical }
public enum TicketStatus { New, InProgress, Resolved, Closed }

public class Ticket
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Başlıq mütləqdir"), StringLength(120, MinimumLength = 5, ErrorMessage = "Başlıq 5-120 simvol olmalıdır")]
    public string Title { get; set; } = "";
    [Required(ErrorMessage = "Təsvir mütləqdir"), StringLength(2000, MinimumLength = 10, ErrorMessage = "Təsvir 10-2000 simvol olmalıdır")]
    public string Description { get; set; } = "";
    public Category Category { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public TicketStatus Status { get; set; } = TicketStatus.New;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int CreatedById { get; set; }
    public AppUser? CreatedBy { get; set; }
    public int? AssignedToId { get; set; }
    public AppUser? AssignedTo { get; set; }
}
