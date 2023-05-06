using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountSystem.Data.ResponseModel
{
  public static class CustomerResponseModel
  {
    public static class Messages
    {
      public const string CustomerCreatedSuccessfull = "Customer created successfull";

    }
    public static class ErrorMessages
    {
      public const string CustomerCreationFailed = "Failed to create Customer";
      public const string CustomerNotExist = "Customer already existed";
    }
  }
}
