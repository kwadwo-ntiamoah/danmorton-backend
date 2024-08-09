using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jemma.Application.Common.Abstractions.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T?> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> FilterAsync(List<Expression<Func<T, bool>>> predicate, Expression<Func<T, object>>[]? includes = null);
        public Task AddAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);
        public void UpdateAsync(T entity);
        public void DeleteAsync(T entity);
    }
}