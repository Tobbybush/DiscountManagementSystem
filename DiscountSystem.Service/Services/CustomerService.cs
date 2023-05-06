using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using DiscountSystem.Data.Common;
using DiscountSystem.Data.Context;
using DiscountSystem.Data.Dto;
using DiscountSystem.Data.IRepository;
using DiscountSystem.Data.Model;
using DiscountSystem.Data.ResponseModel;
using DiscountSystem.Service.IServices;

namespace DiscountSystem.Service.Services
{
  public class CustomerService : ICustomerService
  {
    private readonly DataContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(DataContext context, IUnitOfWork unitOfWork, IMapper mapper)
    {
      _context = context;
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    public async Task<Result<ResponseModel<CustomerDto>>> AddCustomerAsync (CustomerDto request)
    {
      var response = new ResponseModel<CustomerDto>();
      var ExistingCustomer = _context.Customers.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);
      if (ExistingCustomer == null)
      {
        var Customer = _mapper.Map<Customer>(request);
        Customer.CreatedDate = DateTime.UtcNow;
        await _unitOfWork.CustomerRepository.Add(Customer);
        try
        {
          await _unitOfWork.SaveAsync();
          response.IsSuccessful = true;
          response.Message = CustomerResponseModel.Messages.CustomerCreatedSuccessfull;
        }
        catch (Exception ex)
        {
          return Result.Failure<ResponseModel<CustomerDto>>($"{CustomerResponseModel.ErrorMessages.CustomerCreationFailed} - {ex.Message} : {ex.InnerException}");
        }
      }
      else
      {
        return Result.Failure<ResponseModel<CustomerDto>>($"{CustomerResponseModel.ErrorMessages.CustomerCreationFailed}");
      }
      return response;
    }
    public async Task<Result<ResponseModel<IEnumerable<CustomerDto>>>> GetAllCustomerAsync()
    {
      var response = new ResponseModel<IEnumerable<CustomerDto>>();
      var Customer = await _unitOfWork.CustomerRepository.GetAll();
      response.Data = _mapper.Map<IEnumerable<CustomerDto>>(Customer);
      return response;
    }
    public async Task<Result<ResponseModel>> GetDiscountByPhoneAsync (string phoneNumber)
    {
      var response = new ResponseModel();

      int month;
      DateTime CurrentDate;
      DateTime CustomerCreatedDate;

      var customer =await _unitOfWork.CustomerRepository.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
      var discountList = _context.CustomerDiscounts.ToList();
      if (customer == null)
        return Result.Failure<ResponseModel>($"{CustomerResponseModel.ErrorMessages.CustomerNotExist}");
      else
      {
        CustomerCreatedDate = customer.CreatedDate;
        CurrentDate = DateTime.UtcNow;
        
         month = (CurrentDate.Year - CustomerCreatedDate.Year) * 12 + CurrentDate.Month - CustomerCreatedDate.Month;

        for (int i = 0; i < discountList.Count; i++)        
        {
          if (month >= discountList[i].Duration)
          {
            response.Message = $"you are eligible to have {discountList[i].DiscountRate}% on your product ";
          }
          else
          {
            response.Message = "you are not eligible to have any discount on your product ";
          }
        }        
      }
      return response;
    }
  }
}
