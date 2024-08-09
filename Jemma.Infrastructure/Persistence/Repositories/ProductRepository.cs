using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Entities;

namespace Jemma.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(AppDbContext _context): GenericRepository<Product>(_context), IProductRepository
    {
        
    }
}