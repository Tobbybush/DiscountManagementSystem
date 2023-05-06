using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountSystem.Data.IRepository
{
  public interface IUnitOfWork
  {
    ICustomerRepository CustomerRepository { get; }
    IDiscountRepository DiscountRepository { get; }
    Task SaveAsync();
  }
}
