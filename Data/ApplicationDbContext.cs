using freak_store.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace freak_store.Data
{
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

        // Nuevos DbSets para ShoppingCart y ShoppingCartItem
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

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

            // Configuración de las relaciones de ShoppingCart y ShoppingCartItem si es necesario
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.ShoppingCart)
                .WithMany(sc => sc.Items)
                .HasForeignKey(sci => sci.ShoppingCartId);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.Entity is not null && (e.State == EntityState.Added || e.State == EntityState.Modified)))
            {
                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.ClrType == typeof(DateTime))
                    {
                        if (property.CurrentValue is DateTime dateTimeValue)
                        {
                            if (dateTimeValue.Kind == DateTimeKind.Local)
                            {
                                property.CurrentValue = dateTimeValue.ToUniversalTime();
                            }
                        }
                    }
                    else if (property.Metadata.ClrType == typeof(DateTime?))
                    {
                        DateTime? nullableDateTimeValue = property.CurrentValue as DateTime?;
                        if (nullableDateTimeValue.HasValue && nullableDateTimeValue.Value.Kind == DateTimeKind.Local)
                        {
                            property.CurrentValue = nullableDateTimeValue.Value.ToUniversalTime();
                        }
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
