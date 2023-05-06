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
  public class DiscountRepository : RepositoryGeneric<CustomerDiscount>, IDiscountRepository
  {
    private readonly DataContext _context;

    public DiscountRepository(DataContext context) : base(context)
    {
      _context = context;
    }
  }
}
