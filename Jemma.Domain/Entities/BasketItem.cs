using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Enums;

namespace Jemma.Domain.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Print { get; set; } = null!;
        public Guid BasketId { get; set; }

        public static BasketItem Create(Guid basketId, string name, string description, string color, string print) {
            return new BasketItem {
                Id = Guid.NewGuid(),
                BasketId = basketId,
                Name = name,
                Description = description,
                Color = color,
                Print = print
            };
        }
    }
}