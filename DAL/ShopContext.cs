using BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Entities
{
    public class ShopContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ShopContext(DbContextOptions<ShopContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("Connection"));
        }

        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Check> Checks => Set<Check>();
        public DbSet<Discount> Discounts => Set<Discount>();
        public DbSet<HairType> HairTypes => Set<HairType>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductType> ProductTypes => Set<ProductType>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Supply> Supplies => Set<Supply>();

        //public DbSet<User> Users => Set<User>();
        //public DbSet<Role> Roles => Set<Role>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Product)
                .WithOne(e => e.Brand)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HairType>()
                .HasMany(e => e.Product)
                .WithOne(e => e.HairType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Check>()
                .Property(e => e.TotalPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CheckForProduct>()
                .HasKey(u => new { u.CheckID, u.ProductID });

            modelBuilder.Entity<CheckForProduct>()
            .HasOne(pt => pt.Check)
            .WithMany(p => p.CheckForProducts)
            .HasForeignKey(pt => pt.CheckID);

            modelBuilder.Entity<CheckForProduct>()
                .HasOne(pt => pt.Product)
                .WithMany(t => t.CheckForProducts)
                .HasForeignKey(pt => pt.ProductID);

            //modelBuilder.Entity<Supply>()
            //    .HasMany(e => e.SupplyForProducts)
            //    .WithOne(e => e.Supply)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplyForProduct>()
            .HasKey(u => new { u.SupplyID, u.ProductID });

            modelBuilder.Entity<SupplyForProduct>()
                .Property(e => e.PurchasingPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SupplyForProduct>()
            .HasOne(pt => pt.Supply)
            .WithMany(p => p.SupplyForProducts)
            .HasForeignKey(pt => pt.SupplyID);

            modelBuilder.Entity<SupplyForProduct>()
                .HasOne(pt => pt.Product)
                .WithMany(t => t.SupplyForProducts)
                .HasForeignKey(pt => pt.ProductID);

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.SupplyForProducts)
                .WithOne(e => e.Product)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.Products)
                .WithOne(e => e.ProductType)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Supply)
                .WithOne(e => e.Supplier)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Check)
            //    .WithOne(e => e.User)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Supply)
            //    .WithOne(e => e.User)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
