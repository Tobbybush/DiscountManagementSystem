using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountSystem.Data.Model
{
  public class Customer
  {    
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? CustomerDiscountId { get; set; }
    public CustomerDiscount CustomerDiscount { get; set; }
  }
}
