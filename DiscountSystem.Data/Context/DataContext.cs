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
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerDiscount> CustomerDiscounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CustomerDiscount>()
        .HasMany(x => x.Customer)
        .WithOne(x => x.CustomerDiscount)
        .HasForeignKey(fk => fk.CustomerDiscountId);
    }
  }
}
