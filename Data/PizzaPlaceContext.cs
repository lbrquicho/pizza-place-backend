using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using pizza_place_backend.Models;

namespace pizza_place_backend.Data;

public partial class PizzaPlaceContext : DbContext
{
    public PizzaPlaceContext()
    {
    }

    public PizzaPlaceContext(DbContextOptions<PizzaPlaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<PizzaType> PizzaTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("orders");

            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Time).HasColumnName("time");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailsId);

            entity.ToTable("order_details");

            entity.Property(e => e.OrderDetailsId)
                .ValueGeneratedNever()
                .HasColumnName("order_details_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PizzaId)
                .HasMaxLength(50)
                .HasColumnName("pizza_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.ToTable("pizzas");

            entity.Property(e => e.PizzaId)
                .HasMaxLength(50)
                .HasColumnName("pizza_id");
            entity.Property(e => e.PizzaTypeId)
                .HasMaxLength(50)
                .HasColumnName("pizza_type_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .HasColumnName("size");
        });

        modelBuilder.Entity<PizzaType>(entity =>
        {
            entity.ToTable("pizza_types");

            entity.Property(e => e.PizzaTypeId)
                .HasMaxLength(50)
                .HasColumnName("pizza_type_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Ingredients)
                .HasMaxLength(100)
                .HasColumnName("ingredients");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
