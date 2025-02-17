using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Aggregates;

namespace Jemma.Infrastructure.Persistence.Repositories
{
    public class InvoiceRepository(AppDbContext _context): GenericRepository<Invoice>(_context), IInvoiceRepository
    {
        
    }
}