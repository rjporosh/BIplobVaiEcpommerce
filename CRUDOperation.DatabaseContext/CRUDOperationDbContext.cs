using CRUDOperation.DatabaseContext.FluentConfiguration;
using CRUDOperation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperation.DatabaseContext
{
    public class CRUDOperationDbContext : IdentityDbContext<ApplicationUser> //use for authentication
    {
        public long CurrentUserId { get; set; }

        public CRUDOperationDbContext(DbContextOptions options) : base(options)
        {

        }

        public CRUDOperationDbContext()
        {

        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Variant> Variants { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
        //public object Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies(false)
                .UseSqlServer("Server=(local);Database=EcommerceWebApplication; Integrated Security=true")
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//used for authentication

            modelBuilder.Entity<IdentityUser>(b =>
            {
                modelBuilder.Entity<Product>()
                .HasQueryFilter(p => p.IsActive); //use for isActive filtering

                modelBuilder.Entity<Product>()
                   .HasOne(c => c.Stock)
                   .WithOne(c => c.Product)
                   .IsRequired(false);

                modelBuilder.Entity<Stock>()
                    .HasOne(c => c.Product)
                    .WithOne(c => c.Stock)
                    .IsRequired(false);


                /*Product order relationship---------Start-------------*/
                //modelBuilder.ApplyConfiguration(new ProductFluentConfiguration());
                modelBuilder.Entity<ProductOrder>().HasKey(c => new { c.ProductId, c.OrderId });

                modelBuilder.Entity<ProductOrder>()
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(pt => pt.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<ProductOrder>()
                    .HasOne(pt => pt.Order)
                    .WithMany(t => t.Products)
                    .HasForeignKey(pt => pt.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                /*Product order relationship---------End-------------*/
            });
        }
    }
}
