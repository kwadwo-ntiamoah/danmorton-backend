using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Enums;
using Jemma.Domain.ValueObjects;

namespace Jemma.Domain.Aggregates
{
    public class Payment
    {
        public Guid Id {get; private set;}
        public Price AmountPaid {get; private set;} = null!;
        public string PaidBy {get; private set;} = null!;
        public Guid InvoiceId {get; private set;}
        public PaymentMethod PaymentMethod {get; private set;}
        public DateTime DateCreated {get; private set;} = DateTime.UtcNow;

        public static Payment Create(Guid invoiceId, Price amountPaid, string paidBy, PaymentMethod paymentMethod) {
            var payment = new Payment {
                Id = Guid.NewGuid(),
                InvoiceId = invoiceId,
                AmountPaid = amountPaid,
                PaidBy = paidBy,
                PaymentMethod = paymentMethod
            };

            return payment;
        }
    }
}