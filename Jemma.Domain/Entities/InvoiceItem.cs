using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.ValueObjects;

namespace Jemma.Domain.Entities
{
    public class InvoiceItem
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId {get;private set;}
        public string ServiceType { get; private set; } = null!;
        public Price ServiceAmount {get; private set;} = null!;
        public int Quantity {get; private set;}
        public string Name {get; private set;} =  null!;
        public string Description {get; private set;} = null!;

        public static InvoiceItem Create(Guid invoiceId, string name, string description, string serviceType, Price serviceAmount, int quantity) {
            var invoiceItem = new InvoiceItem {
                Id = Guid.NewGuid(),
                InvoiceId = invoiceId,
                ServiceType = serviceType,
                ServiceAmount = serviceAmount,
                Quantity = quantity,
                Name = name,
                Description = description,
            };

            return invoiceItem;
        }

        public decimal GetPrice() {
            return ServiceAmount.Amount * Quantity;
        }
    }
}