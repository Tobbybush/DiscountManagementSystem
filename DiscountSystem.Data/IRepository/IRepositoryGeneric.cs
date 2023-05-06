using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiscountSystem.Data.IRepository
{
  /// <summary>
  ///  Interface that defines base of generic Repository class
  ///  for more details, see: https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#implement-a-generic-repository-and-a-unit-of-work-class
  /// </summary>
  /// <typeparam name="T">class</typeparam>
  public interface IRepositoryGeneric<T>
      where T : class
  {
    Task<T> Get(int id);

    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
        IOrderedQueryable<T>> orderBy = null, string includeProperties = null);

    Task<T> FirstOrDefault(
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null);    

    Task<T> Add(T entity);

    Task AddRange(IEnumerable<T> entity);
    Task<bool> Exists(Expression<Func<T, bool>> filter = null);
    void Update(T entity);

    void Remove(int id);

    void Remove(T entity);



    void RemoveRange(IEnumerable<T> entity);
  }
}
