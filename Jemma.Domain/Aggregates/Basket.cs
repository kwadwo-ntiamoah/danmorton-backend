using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Entities;
using Jemma.Domain.Enums;

namespace Jemma.Domain.Aggregates
{
    public class Basket
    {
        public Guid Id { get; set; }
        public string BasketNumber { get; set; } = string.Empty;
        public WashStatus Status { get; set; } = WashStatus.NOT_STARTED;
        private readonly List<BasketItem> _basketItems = [];
        public IReadOnlyList<BasketItem> BasketItems => _basketItems.AsReadOnly();

        public static Basket Create(string basketNumber)
        {
            var basket = new Basket
            {
                Id = Guid.NewGuid(),
               BasketNumber = basketNumber
            };

            return basket;
        }

        public void AddBasketItems(BasketItem basketItem)
        {
            _basketItems.Add(basketItem);
        }
    }
}