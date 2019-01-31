﻿namespace ShoppingCartStore.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ShoppingCartStore.Models;

    public class ShoppingCartStoreDbContext : IdentityDbContext<Customer>
    {
        public ShoppingCartStoreDbContext(DbContextOptions<ShoppingCartStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Cart> Carts{ get; set; }

        public DbSet<Item> Products { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(b => b.CustomerId);

            builder.Entity<Item>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Items)
                .HasForeignKey(i => i.ProductId);

            builder.Entity<Item>()
                .HasOne(i => i.Cart);

            builder.Entity<ProductsOrder>()
                .HasKey(po => new { po.OrderId, po.ProductId});
            builder.Entity<ProductsOrder>()
                .HasOne(po => po.Order)
                .WithMany(o => o.ProductsOrder)
                .HasForeignKey(po => po.OrderId);
            builder.Entity<ProductsOrder>()
                .HasOne(po => po.Product)
                .WithMany(c => c.ProductsOrder)
                .HasForeignKey(po => po.ProductId);

            base.OnModelCreating(builder);
        }
    }
}
