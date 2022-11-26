using System;
using System.Diagnostics;
using Api.Models;
using Api.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Api.Data
{
    public class DbConnect : DbContext
    {

        public DbConnect(DbContextOptions<DbConnect> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Auth> Authorisation { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderProduct>()
        //        .HasKey(t => new { t.OrdersId, t.ProductsId });

        //    modelBuilder.Entity<OrderProduct>()
        //        .HasOne(pt => pt.Order)
        //        .WithMany(p => p.OrderProducts)
        //        .HasForeignKey(pt => pt.OrdersId);

        //    modelBuilder.Entity<OrderProduct>()
        //        .HasOne(pt => pt.Product)
        //        .WithMany(t => t.OrderProducts)
        //        .HasForeignKey(pt => pt.ProductsId);
        //}
    }
}

