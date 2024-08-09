using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Aggregates;

namespace Jemma.Infrastructure.Persistence.Repositories
{
    public class ItemRepository(AppDbContext _context): GenericRepository<Item>(_context), IItemRepository
    {
        
    }
}