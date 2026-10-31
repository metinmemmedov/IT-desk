using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
namespace HelpDesk.Data;
public class AppDbContext(DbContextOptions<AppDbContext> o) : DbContext(o)
{
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    protected override void OnModelCreating(ModelBuilder m)
    {
        m.Entity<AppUser>().HasIndex(u => u.Email).IsUnique();
        m.Entity<Ticket>().Property(t => t.Category).HasConversion<string>().HasMaxLength(20);
        m.Entity<Ticket>().Property(t => t.Priority).HasConversion<string>().HasMaxLength(20);
        m.Entity<Ticket>().Property(t => t.Status).HasConversion<string>().HasMaxLength(20);
        m.Entity<Ticket>().HasOne(t => t.CreatedBy).WithMany().HasForeignKey(t => t.CreatedById).OnDelete(DeleteBehavior.Restrict);
        m.Entity<Ticket>().HasOne(t => t.AssignedTo).WithMany().HasForeignKey(t => t.AssignedToId).OnDelete(DeleteBehavior.Restrict);
    }
}
