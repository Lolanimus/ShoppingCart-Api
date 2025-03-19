using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Infrastracture;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartProduct> CartProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<WebUser> WebUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,5433;Database=ShoppingCart;User Id=sa;Password=!Lolanimus;TrustServerCertificate=true");
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CartProduct");

            entity.Property(e => e.ProductSize).HasMaxLength(2);
            entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.WebUser).WithMany()
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK_CartProduct_WebUser");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartProduct_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ProductDesc).HasMaxLength(1024);
            entity.Property(e => e.ProductGender).HasMaxLength(50);
            entity.Property(e => e.ProductImageUri).HasMaxLength(256);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductPrice).HasColumnType("money");
            entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<WebUser>(entity =>
        {
            entity.ToTable("WebUser");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.UserCity).HasMaxLength(100);
            entity.Property(e => e.UserEmail).HasMaxLength(100);
            entity.Property(e => e.UserFirstName).HasMaxLength(100);
            entity.Property(e => e.UserLastName).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserPassword).HasMaxLength(100);
            entity.Property(e => e.UserPhone).HasMaxLength(12);
            entity.Property(e => e.UserStreet).HasMaxLength(256);
            entity.Property(e => e.UserZip).HasMaxLength(6);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
