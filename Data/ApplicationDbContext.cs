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

    public DbSet<Models.Admins> DataAdmins { get; set; }

    public DbSet<Models.AdminType> DataAdminsType { get; set; }

    public DbSet<Models.Categories> DataCategories { get; set; }

    public DbSet<Models.Discounts> DataDiscounts { get; set; }

    public DbSet<Models.Inventory> DataInventory { get; set; }

    public DbSet<Models.OrderDetails> DataOrderDetails { get; set; }

    public DbSet<Models.OrderItems> DataOrderItems { get; set; }

    public DbSet<Models.PaymentDetails> DataPaymentDetails { get; set; }

    public DbSet<Models.Products> DataProducts { get; set; }

    public DbSet<Models.UserPayments> DataUserPayments { get; set; }

    public DbSet<Models.UserAddresses> DataUserAddresses{ get; set; }

}
