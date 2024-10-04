using freak_store.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace freak_store.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> DataUsers { get; set; }  

    public DbSet<Category> DataCategories { get; set; }

    public DbSet<Discount> DataDiscounts { get; set; }

    public DbSet<Inventory> DataInventory { get; set; }

    public DbSet<Contact> DataContact { get; set; }

    public DbSet<Product> DataProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Discount>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Inventory>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Product>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<User>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Contact>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

    }
}
