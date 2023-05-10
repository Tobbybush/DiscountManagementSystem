using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiscountSystem.Data.Model;

namespace DiscountSystem.Data.Dto
{
  public class CustomerDiscountDto
  {    
    public string Name { get; set; }
    public decimal DiscountRate { get; set; }
    public int Duration { get; set; }    

  }
  public class CustomerDiscountDtoRequestMappingConfig : Profile
  {
    public CustomerDiscountDtoRequestMappingConfig()
    {
      CreateMap<CustomerDiscount, CustomerDiscountDto>().ReverseMap();
    }
  }
}
