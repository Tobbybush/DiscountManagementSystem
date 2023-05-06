using System.Reflection;
using DiscountSystem.Data.Dto;

namespace DiscountSystem.Api.Configuration
{
  public static class AutoMapperConfig
  {
    public static void Configure(IServiceCollection services, params Assembly[] additionalAssemblies)
    {
      services.AddAutoMapper(typeof(CustomerDtoRequestMappingConfig));
      services.AddAutoMapper(typeof(CustomerDiscountDtoRequestMappingConfig));
    }
  }
}
