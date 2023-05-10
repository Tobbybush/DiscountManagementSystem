using System;
using CSharpFunctionalExtensions;
using DiscountSystem.Data.Dto;
using DiscountSystem.Data.Model;
using DiscountSystem.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiscountSystem.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly ICustomerService _customerService;
    private readonly ILogger<Customer> _logger;

    public CustomerController(ICustomerService customerService, ILogger<Customer> logger)
    {
      _customerService = customerService;
      _logger = logger;
    }
    /// <summary>
    /// Get all customer
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllCustomer")]
    [ProducesDefaultResponseType(typeof(CustomerDto))]
    public async Task<IActionResult> GetCustomer()
    {
      var response = await _customerService.GetAllCustomerAsync();
       Result res = Result.Combine(response);
      if (res.IsFailure)
      {
        if (response.Equals(null))
          _logger.LogError(res.Error);
          return BadRequest(res.Error);
      }        
      return Ok(response.Value);
    }
    /// <summary>
    /// Create customer
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("AddCustomer")]
    [ProducesDefaultResponseType(typeof(CustomerDto))]
    public async Task<IActionResult> AddCustomer(CustomerDto request)
    {
      var response = await _customerService.AddCustomerAsync(request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
      {
        if (response.Equals(null))
          _logger.LogError(res.Error);
          return BadRequest(res.Error);
      }        
      return Ok(response.Value);
    }
    /// <summary>
    /// /check customer eligible for discount
    /// </summary>
    /// <param name="PhoneNumber"></param>
    /// <returns></returns>
    [HttpGet("CheckCustomerEligibleForDiscount/{PhoneNumber}")]
    [ProducesDefaultResponseType(typeof(CustomerDto))]
    public async Task<IActionResult> CustomerByNumber (string PhoneNumber)
    {
      var response1 = await _customerService.GetCustomerByPhoneAsync(PhoneNumber);
      Result res = Result.Combine(response1);
      if (res.IsFailure)
      {
        if (response1.Equals(null))
          _logger.LogError(res.Error);
        return BadRequest(res.Error);
      }

      DateTime createdTime = response1.Value.Data.CreatedDate;
      var response2 = await _customerService.CheckDiscountAsync(createdTime, null);
      Result res2 = Result.Combine(response1);
      if (res2.IsFailure)
      {
        if (response2.Equals(null))
          _logger.LogError(res2.Error);
        return BadRequest(res2.Error);
      }
      return Ok(response2.Value);
    }
    /// <summary>
    /// Assign Discount to customer
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="discountId"></param>
    /// <returns></returns>
    [HttpPut("AssignDiscount")]
    [ProducesDefaultResponseType(typeof(CustomerDto))]
    public async Task<IActionResult> AssignDiscount(string phoneNumber, int discountId)
    {      
      var response = await _customerService.AssignDiscountToCustomer(phoneNumber, discountId);
      Result res = Result.Combine(response);
      if (res.IsFailure)
      {
        if (response.Equals(null))
          _logger.LogError(res.Error);
        return BadRequest(res.Error);
      }
      return Ok(response.Value);
    }
  }
}
