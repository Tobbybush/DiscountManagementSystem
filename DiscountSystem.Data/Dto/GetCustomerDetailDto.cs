using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DiscountSystem.Data.Model;

namespace DiscountSystem.Data.Dto
{
  public class GetCustomerDetailDto
  {
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int DiscountId { get; set; }
    public DateTime CreatedDate { get; set; }
  }
  public class GetCustomerDetailDtoRequestMappingConfig : Profile
  {
    public GetCustomerDetailDtoRequestMappingConfig()
    {
      CreateMap<Customer, GetCustomerDetailDto>().ReverseMap();
    }
  }
}
