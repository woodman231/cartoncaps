using Microsoft.EntityFrameworkCore;
using CartonCapsDbContext.Models;

namespace CartonCapsDbContext;

public class CartonCapsEFDbContext : DbContext
{
    public CartonCapsEFDbContext(DbContextOptions<CartonCapsEFDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Invitation> Invitations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invitation>()
            .HasOne(i => i.SourceAccount)
            .WithMany(a => a.InvitationsSent)
            .HasForeignKey(i => i.SenderAccountID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Invitation>()
            .HasOne(i => i.AcceptedAccount)
            .WithMany(a => a.InvitationsAccepted)
            .HasForeignKey(i => i.AcceptedAccountID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
