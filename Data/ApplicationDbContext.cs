using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace freak_store.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Models.User> DataUser { get; set; }

    public DbSet<Models.Category> DataCategories { get; set; }

    public DbSet<Models.Discount> DataDiscounts { get; set; }

    public DbSet<Models.Inventory> DataInventory { get; set; }


    
}
