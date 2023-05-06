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
    [HttpGet("CheckCustomerEligibleForDiscount")]
    [ProducesDefaultResponseType(typeof(CustomerDto))]
    public async Task<IActionResult> CheckeCustomerByNumber (string PhoneNumber)
    {
      var response = await _customerService.GetDiscountByPhoneAsync(PhoneNumber);
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
