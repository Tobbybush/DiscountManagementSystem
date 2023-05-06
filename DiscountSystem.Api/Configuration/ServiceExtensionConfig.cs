using DiscountSystem.Data.IRepository;
using DiscountSystem.Data.Repository;
using DiscountSystem.Service.IServices;
using DiscountSystem.Service.Services;

namespace DiscountSystem.Api.Configuration
{
  public static class ServiceExtensionConfig
  {
    public static void ConfigureServices(IServiceCollection services)
    {
      // Repository
      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddTransient<ICustomerRepository, CustomerRepository>();
      services.AddTransient<IDiscountRepository, DiscountRepository>();      
      services.AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));


      //Services
      services.AddScoped<ICustomerService, CustomerService>();
      services.AddScoped<IDiscountService, DiscountService>();
    }
  }
}
