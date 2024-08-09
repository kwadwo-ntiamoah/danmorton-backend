using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Entities;

namespace Jemma.Application.Common.Abstractions.Persistence
{
    public interface IInvoiceItemRepository: IGenericRepository<InvoiceItem>
    {
        
    }
}