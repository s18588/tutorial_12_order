
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tutorial_12_Order.Models
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Confectionery> Confectionery { get; set; }
        public DbSet<Confectionery_Order> Confectionery_order { get; set; }

        public OrderDbContext()
        {
        }

        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Confectionery_Order>()
                .HasKey(c => new {c.IdConfectionary, c.IdOrder});
        }
    }
    
    public class Employee {
        
        [Key] public int IdEmployee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Customer
    {
        [Key] public int IdClient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Order
    {
        [Key] public int IdOrder { get; set; }
        public DateTime DateAccepted { get; set; }
        public DateTime DateFinished { get; set; }
        public string Notes { get; set; }
        [ForeignKey("Client")] public int IdClient { get; set; }
        [ForeignKey("Employee")] public int IdEmployee { get; set; }
    }

    public class Confectionery
    {
        [Key] public int IdConfectionery { get; set; }
        public string Name { get; set; }
        public float PricePerItem { get; set; }
        public string Type { get; set; }
    }

    public class Confectionery_Order
    {
        [Key] [ForeignKey("Confection")] public int IdConfectionary { get; set; }
        [Key] [ForeignKey("Order")] public int IdOrder { get; set; }
        public int quantity { get; set; }
        public string Notes { get; set; }
    }
    
}