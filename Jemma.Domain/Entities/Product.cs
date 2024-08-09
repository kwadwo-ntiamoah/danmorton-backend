using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jemma.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set;} = null!;
        public string Description { get; private set; } = null!;

        public static Product Create(string name, string description) {
            var product = new Product {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };  

            return product;
        }
    }
}