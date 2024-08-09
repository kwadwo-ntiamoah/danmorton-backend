using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Entities;
using Jemma.Infrastructure.Persistence.Repositories;

namespace Jemma.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository(AppDbContext _context): GenericRepository<Customer>(_context), ICustomerRepository
    {
        
    }
}