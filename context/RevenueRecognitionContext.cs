using apbd_project.models;
using Microsoft.EntityFrameworkCore;

namespace apbd_project.context;

public class RevenueRecognitionContext : DbContext
{
    public RevenueRecognitionContext(DbContextOptions<RevenueRecognitionContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasDiscriminator<string>("ClientType")
            .HasValue<Individual>("Individual")
            .HasValue<Company>("Company");

        modelBuilder.Entity<Contract>()
            .HasMany(c => c.Payments)
            .WithOne(p => p.Contract)
            .HasForeignKey(p => p.ContractId);
    }
}