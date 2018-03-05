using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DanielStoreFront.Models
{
    public partial class DanielTestContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
    {
        public DanielTestContext(): base()
        {

        }

        public DanielTestContext(DbContextOptions options) : base(options)
        {

        }
               
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsCategories> ProductsCategories { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }
          
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //This is called Fluent API - it allows for more specific customization of database rules
            modelBuilder.Entity<Order>().HasKey(prop => prop.ID);
            modelBuilder.Entity<Order>()
                .Property(prop => prop.ID)
                .ValueGeneratedOnAdd();

            //Fluent API can almost be translated into a sentance:
            //Order Has Property Tracking Number whose value is generated when added
            modelBuilder.Entity<Order>().Property(prop => prop.TrackingNumber)
               .ValueGeneratedOnAdd();

            //Order has many line items, each line item has an order, which is required
            modelBuilder.Entity<Order>().HasMany(o => o.LineItems).WithOne(l => l.Order).IsRequired();

            //Line items have one order, with many line items.
            modelBuilder.Entity<LineItem>().HasOne(l => l.Order).WithMany(o => o.LineItems);
            modelBuilder.Entity<LineItem>().HasOne(l => l.Product).WithMany(o => o.LineItems);

            modelBuilder.Entity<Products>().HasMany(p => p.LineItems).WithOne(l => l.Product).IsRequired();





            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AspNetUserId).HasMaxLength(128);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateLastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<ProductsCategories>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CategoryId });

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRoductsCategories_Categories");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsCategories_Products");
            });
        }
    }
}
