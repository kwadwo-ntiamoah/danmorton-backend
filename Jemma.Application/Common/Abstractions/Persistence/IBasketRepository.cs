using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Aggregates;

namespace Jemma.Application.Common.Abstractions.Persistence
{
    public interface IBasketRepository: IGenericRepository<Basket>
    {
        
    }
}