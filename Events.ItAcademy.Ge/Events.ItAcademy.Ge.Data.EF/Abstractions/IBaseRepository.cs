using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.Data.EF.Abstractions
{
    public interface IBaseRepository <T> where T : class
    {
        IQueryable<T> Table { get; }

        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(params object[] key);

        Task AddAsync(T entity);

        Task UpdateAsync(T item);

        Task RemoveAsync(T item);

        Task RemoveAsync(params object[] key);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    }
}
