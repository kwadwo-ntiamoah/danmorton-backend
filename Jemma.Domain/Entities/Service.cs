using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.ValueObjects;

namespace Jemma.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public Price Price {get; private set; } = null!;
        public Guid ItemId {get; private set;}

        public static Service Create(Guid itemId, string Name, Price price) {
            var service = new Service {
                Id = Guid.NewGuid(),
                ItemId = itemId,
                Name = Name,
                Price = price,
            };

            return service;
        }

        public void UpdatePrice(Price newPrice) {
            Price = newPrice;
        }
    }
}