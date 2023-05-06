using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiscountSystem.Data.Model;

namespace DiscountSystem.Data.Dto
{
  public class CustomerDto
  {    
    public string PhoneNumber { get; set; }
    public string Address { get; set; }    
    public int DiscountId { get; set; }    
  }
  public class CustomerDtoRequestMappingConfig : Profile
  {
    public CustomerDtoRequestMappingConfig()
    {
      CreateMap<Customer, CustomerDto>().ReverseMap();
    }
  }
}
