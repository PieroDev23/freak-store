using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace freak_store.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    // DbSets para cada modelo
    public DbSet<Models.User> DataUsers { get; set; }  // Cambié "DataUser" a "DataUsers" para mantener consistencia en plural

    public DbSet<Models.Category> DataCategories { get; set; }

    public DbSet<Models.Discount> DataDiscounts { get; set; }

    public DbSet<Models.Inventory> DataInventories { get; set; }

    // Agregar el DbSet para Products
    public DbSet<Models.Product> DataProducts { get; set; }

}
