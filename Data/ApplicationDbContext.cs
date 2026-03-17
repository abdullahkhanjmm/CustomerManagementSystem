using CustomerManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerType> CustomerTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .HasOne(c => c.CustomerType)
            .WithMany(ct => ct.Customers)
            .HasForeignKey(c => c.CustomerTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.Address).HasMaxLength(50).IsRequired();
            entity.Property(e => e.City).HasMaxLength(50).IsRequired();
            entity.Property(e => e.State).HasMaxLength(2).IsRequired();
            entity.Property(e => e.Zip).HasMaxLength(10).IsRequired();
            entity.Property(e => e.LastUpdated)
                  .HasColumnType("datetime2(7)")
                  .HasDefaultValueSql("getdate()")
                  .IsRequired();
        });

        modelBuilder.Seed();
    }
}
