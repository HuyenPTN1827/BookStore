using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStore.Models
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
				var builder = new ConfigurationBuilder()
							  .SetBasePath(Directory.GetCurrentDirectory())
							  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
				IConfigurationRoot configuration = builder.Build();
				optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.DoB).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fullname).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(11);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Accounts_Roles");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Author).HasMaxLength(255);

                entity.Property(e => e.BookName).HasMaxLength(255);

                entity.Property(e => e.Cover)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("(N'cover-default.jpg')");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PublisherId).HasColumnName("PublisherID");

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK_Books_Publishers");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK_Books_SubCategories");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Orders_Accounts");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.BookId });

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_OrderDetails_Books");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetails_Orders");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PushlisherId);

                entity.Property(e => e.PushlisherId).HasColumnName("PushlisherID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(11);

                entity.Property(e => e.PublisherName).HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.SubCategoryName).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_SubCategories_Categories");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
