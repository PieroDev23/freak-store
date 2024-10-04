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

<<<<<<< HEAD
    public DbSet<User> DataUser { get; set; }
=======
    // DbSets para cada modelo
    public DbSet<Models.User> DataUsers { get; set; }  // Cambié "DataUser" a "DataUsers" para mantener consistencia en plural
>>>>>>> 0795fdfc49e42f3ec14d934b735ca2dbf9385ab4

    public DbSet<Category> DataCategories { get; set; }

    public DbSet<Discount> DataDiscounts { get; set; }

<<<<<<< HEAD
    public DbSet<Inventory> DataInventory { get; set; }

    public DbSet<Contact> DataContact { get; set; }

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
=======
    public DbSet<Models.Inventory> DataInventories { get; set; }

    // Agregar el DbSet para Products
    public DbSet<Models.Product> DataProducts { get; set; }

>>>>>>> 0795fdfc49e42f3ec14d934b735ca2dbf9385ab4
}
