using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountSystem.Data.Model
{
  public class CustomerDiscount
  {    
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DiscountRate { get; set; }
    public int Duration { get; set; }    
    public List<Customer> Customer { get; set; }
  }
}
