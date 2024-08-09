using LinqKit;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Jemma.Application.Common.Abstractions.Persistence;

namespace Jemma.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : class
    {
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> FilterAsync(List<Expression<Func<T, bool>>> tempPredicates, Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();

            var predicate = PredicateBuilder.New<T>(true);

            foreach (var pred in tempPredicates)
            {
                predicate = predicate.And(pred);
            }

            if (!includes.IsNullOrEmpty())
            {
                foreach (var include in includes!)
                {
                    query = query.Include(include);
                }
            }

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}