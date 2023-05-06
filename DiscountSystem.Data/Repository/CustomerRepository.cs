using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountSystem.Data.Context;
using DiscountSystem.Data.IRepository;
using DiscountSystem.Data.Model;

namespace DiscountSystem.Data.Repository
{
  public class CustomerRepository : RepositoryGeneric<Customer>, ICustomerRepository
  {
    private readonly DataContext _db;

    public CustomerRepository(DataContext context) : base(context)
    {
       _db = context;
    }
  }
}
