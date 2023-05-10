using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DiscountSystem.Data.Common;
using DiscountSystem.Data.Dto;
using DiscountSystem.Data.Model;
using DiscountSystem.Data.ResponseModel;

namespace DiscountSystem.Service.IServices
{
  public interface ICustomerService
  {
    Task<Result<ResponseModel<CustomerDto>>> AddCustomerAsync(CustomerDto request);
    Task<Result<ResponseModel<IEnumerable<CustomerDto>>>> GetAllCustomerAsync();
    Task<Result<ResponseModel<GetCustomerDetailDto>>> GetCustomerByPhoneAsync(string phoneNumber);
    Task<Result<ResponseModel>> CheckDiscountAsync(DateTime from, DateTime? to);
    Task<Result<ResponseModel>> AssignDiscountToCustomer(string phoneNumber, int discountId);
  }
}
