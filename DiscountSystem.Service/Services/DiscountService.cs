using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using DiscountSystem.Data.Common;
using DiscountSystem.Data.Dto;
using DiscountSystem.Data.IRepository;
using DiscountSystem.Data.Model;
using DiscountSystem.Data.ResponseModel;
using DiscountSystem.Service.IServices;

namespace DiscountSystem.Service.Services
{
  public class DiscountService : IDiscountService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DiscountService (IUnitOfWork unitOfWork, IMapper mapper)
      {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
   public async Task<Result<ResponseModel>> CreateDiscountAsync (CustomerDiscountDto request)
    {
      var response = new ResponseModel();
      if (request == null)
      {
        response.Message = DiscountResponseModel.ErrorMessages.InvalidRequest;
      }
      else
      {
        var Discount = _mapper.Map<CustomerDiscount>(request);
        await _unitOfWork.DiscountRepository.Add(Discount);
        try 
        {
          await _unitOfWork.SaveAsync();
          response.IsSuccessful = true;
          response.Message = DiscountResponseModel.Messages.DiscountCreatedSuccessfull;
        }
        catch(Exception ex)
        {
          return Result.Failure<ResponseModel>($"{CustomerResponseModel.ErrorMessages.CustomerCreationFailed} - {ex.Message} : {ex.InnerException}");
        }
      }
      return response;
    } 
    public async Task<Result<ResponseModel<List<CustomerDiscountDto>>>> GetAllDiscount()
    {
      var response = new ResponseModel<List<CustomerDiscountDto>>();
      var DiscountList = await _unitOfWork.DiscountRepository.GetAll();
      response.Data = _mapper.Map<List<CustomerDiscountDto>>(DiscountList);
      return response;
    }
    //public async Task<Result<ResponseModel>> ProfileDiscountToCustomer(string phoneNumber)
    //{
    //  var response = new ResponseModel();
    //  var customer = await _unitOfWork.CustomerRepository.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
      
    //  if (customer == null)
    //    return Result.Failure<ResponseModel>($"{DiscountResponseModel.ErrorMessages.CourseCreationFailed}");
    //  else
    //  {

    //  }
    //}
  }
}
