using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Entities;

namespace Jemma.Infrastructure.Persistence.Repositories
{
    public class InvoiceItemRepository(AppDbContext _context): GenericRepository<InvoiceItem>(_context), IInvoiceItemRepository
    {
        
    }
}