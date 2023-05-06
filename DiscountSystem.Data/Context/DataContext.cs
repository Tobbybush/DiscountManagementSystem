using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountSystem.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DiscountSystem.Data.Context
{
  public class DataContext : DbContext
  {
    public DataContext()
    {

    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //  optionsBuilder.UseInMemoryDatabase("Seyidb");
    //}
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerDiscount> CustomerDiscounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>()
        .HasOne(d => d.CustomerDiscount)
        .WithOne(c => c.Customer)
        .HasForeignKey<Customer>(fk => fk.DiscountId);
    }
  }
}
