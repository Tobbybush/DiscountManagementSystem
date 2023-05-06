using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountSystem.Data.ResponseModel
{
  public static class DiscountResponseModel
  {
    public static class Messages
    {
      public const string DiscountCreatedSuccessfull = "Discount created successfull";

    }
    public static class ErrorMessages
    {
      public const string DiscountCreationFailed = "Discount failed to create";
      public const string InvalidRequest = "Invalid request";
    }
  }
}
