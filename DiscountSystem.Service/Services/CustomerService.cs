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
    public async Task<Result<ResponseModel<CustomerDto>>> AddCustomerAsync(CustomerDto request)
    {
      var response = new ResponseModel<CustomerDto>();
      var existingCustomer = _context.Customers.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);
      if (existingCustomer == null)
      {
        var customer = _mapper.Map<Customer>(request);
        customer.CreatedDate = DateTime.UtcNow;
        customer.CustomerDiscountId = 1;
        await _unitOfWork.CustomerRepository.Add(customer);
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
        return Result.Failure<ResponseModel<CustomerDto>>($"{CustomerResponseModel.ErrorMessages.CustomerNotExist}");
      }
      return response;
    }
    public async Task<Result<ResponseModel<IEnumerable<CustomerDto>>>> GetAllCustomerAsync()
    {
      var response = new ResponseModel<IEnumerable<CustomerDto>>();
      var customer = await _unitOfWork.CustomerRepository.GetAll();
      response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customer);
      response.IsSuccessful = true;
      return response;
    }
    public async Task<Result<ResponseModel<GetCustomerDetailDto>>> GetCustomerByPhoneAsync(string phoneNumber)
    {
      var response = new ResponseModel<GetCustomerDetailDto>();

      var customer = await _unitOfWork.CustomerRepository.FirstOrDefault(x => x.PhoneNumber == phoneNumber);

      if (customer == null)
        return Result.Failure<ResponseModel<GetCustomerDetailDto>>($"{CustomerResponseModel.ErrorMessages.CustomerNotExist}");
      else
      {
        response.IsSuccessful = true;
        response.Message = CustomerResponseModel.Messages.CustomerByPhoneNumberSuccessfull;
        response.Data = _mapper.Map<GetCustomerDetailDto>(customer);

      }
      return response;
    }
    public async Task<Result<ResponseModel>> CheckDiscountAsync(DateTime from, DateTime? to)
    {
      var response = new ResponseModel();

      int month;
      DateTime CurrentDate;
      DateTime CustomerCreatedDate;

      var discountList = await _unitOfWork.DiscountRepository.GetAll(); //_context.CustomerDiscounts.ToList();

      CustomerCreatedDate = from;
      CurrentDate = DateTime.UtcNow;

      month = (CurrentDate.Year - CustomerCreatedDate.Year) * 12 + CurrentDate.Month - CustomerCreatedDate.Month;

      //for (int i = 0; i < discountList.Count; i++)
      foreach(var discount in discountList)
      {
        if (month >= discount.Duration)
        {
          response.Message = $"you are eligible to have {discount.DiscountRate}% on your product ";
          
        }
        else
        {
          response.Message = "you are not eligible to have any discount on your product ";
        }
      }
      return response;
    }
    public async Task<Result<ResponseModel>> AssignDiscountToCustomer(string phoneNumber, int discountId)
    {
      var response = new ResponseModel();      
      try
      {
        var customer = await _unitOfWork.CustomerRepository.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        if (customer != null)
        {

          customer.CustomerDiscountId = discountId;
          _unitOfWork.CustomerRepository.Update(customer);
          await _unitOfWork.SaveAsync();
          response.IsSuccessful = true;
          response.Message = CustomerResponseModel.Messages.CustomerUpdatedSuccessful;
        }
      }
      catch (Exception ex)
      {
        return Result.Failure<ResponseModel>($"{CustomerResponseModel.ErrorMessages.CustomerUpdateFailed} - {ex.Message} : {ex.InnerException}");
      }

      return response;
    }
  }
}
