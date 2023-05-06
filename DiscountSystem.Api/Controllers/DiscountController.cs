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
  public class DiscountController : ControllerBase
  {
    private readonly IDiscountService _discountService;
    private readonly ILogger<CustomerDiscount> _logger;

    public DiscountController(IDiscountService discountService, ILogger<CustomerDiscount> logger)
    {
      _discountService = discountService;
      _logger = logger;
    }
    /// <summary>
    ///  Get all list of Discount
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllDiscount")]
    [ProducesDefaultResponseType(typeof(CustomerDiscountDto))]
    public async Task<IActionResult> GetAllDiscount()
    {
      var response = await _discountService.GetAllDiscount();
      Result res = Result.Combine(response);
      if (res.IsFailure)
      {
        if (response.Equals(null))
          _logger.LogError(res.Error);
          return BadRequest("Invalid request");
      }        
      return Ok(response.Value);
    }
    /// <summary>
    /// Create discount 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("AddDiscount")]
    [ProducesDefaultResponseType(typeof(CustomerDiscountDto))]
    public async Task<IActionResult> AddDiscount(CustomerDiscountDto request)
    {
      var response = await _discountService.CreateDiscountAsync(request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
      {
        if (response.Equals(null))
          _logger.LogError(res.Error);
        return BadRequest("Invalid request");
      }
        
      return Ok(response.Value);
    }
  }
}
