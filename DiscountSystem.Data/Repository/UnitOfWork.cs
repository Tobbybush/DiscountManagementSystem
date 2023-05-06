using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountSystem.Data.Context;
using DiscountSystem.Data.IRepository;

namespace DiscountSystem.Data.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DataContext _context;
    private ICustomerRepository _customerRepository;
    private IDiscountRepository _discountRepository;
    public UnitOfWork(DataContext context, ICustomerRepository customerRepository)
    {
      _context = context;
      _customerRepository = customerRepository;
    }
    public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_context);

    public IDiscountRepository DiscountRepository => _discountRepository ??= new DiscountRepository(_context);

    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
      _context.Dispose();
      GC.SuppressFinalize(this);
    }
  }
}
