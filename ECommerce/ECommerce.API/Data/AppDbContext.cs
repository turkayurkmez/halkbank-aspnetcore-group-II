using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Product>().Property(p => p.SKU).IsRequired().HasMaxLength(50);


            modelBuilder.Entity<Product>().HasOne(p => p.Category)
                                          .WithMany(c => c.Products)
                                          .HasForeignKey(p => p.CategoryId)
                                          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>().HasIndex(p => p.SKU).IsUnique();

            modelBuilder.Entity<Customer>(e =>
            {
                e.Property(c => c.FullName).HasMaxLength(200).IsRequired();
                e.Property(c => c.Email).HasMaxLength(200).IsRequired();
            });


            modelBuilder.Entity<Category>().HasIndex(c => c.Name);
        }


    }
}
