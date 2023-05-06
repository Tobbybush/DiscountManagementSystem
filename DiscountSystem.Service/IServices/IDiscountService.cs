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
  public interface IDiscountService
  {
    Task<Result<ResponseModel<List<CustomerDiscountDto>>>> GetAllDiscount();
    Task<Result<ResponseModel>> CreateDiscountAsync(CustomerDiscountDto request);
  }
}
