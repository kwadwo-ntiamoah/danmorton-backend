using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Domain.Entities;
using Jemma.Domain.Enums;
using Jemma.Domain.ValueObjects;

namespace Jemma.Domain.Aggregates
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public Price TotalAmount { get; private set; } = null!;
        public double Discount { get; private set; } = 0.00;
        public string InvoiceNumber { get; set; } = null!;
        public Price AmountPaid { get; private set; } = null!;
        public InvoicePaymentStatus PaymentStatus { get; private set; } = InvoicePaymentStatus.NOT_PAID;
        public BillRecipient BillTo { get; private set; } = null!;
        public DateTime DueDate { get; private set; } = DateTime.Now.AddDays(7);
        private readonly List<InvoiceItem> _invoiceItems = [];
        public IReadOnlyList<InvoiceItem> InvoiceItems => _invoiceItems.AsReadOnly();
        public DateTime DateCreated {get; private set;} = DateTime.UtcNow;

        public static Invoice Create(double discount, string recipientName, string recipientContact, string recipientAddress)
        {
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                Discount = discount,
                InvoiceNumber = GenerateInvoiceNumber(),
                BillTo = BillRecipient.Create(recipientName, recipientContact, recipientAddress),
                AmountPaid = new Price(0)
            };

            return invoice;
        }

        public void AddInvoiceItem(InvoiceItem invoiceItem)
        {
            var invoiceItemToCheck = _invoiceItems.FirstOrDefault(x => x.Name == invoiceItem.Name && x.ServiceType == invoiceItem.ServiceType);

            if (invoiceItemToCheck == null)
            {
                _invoiceItems.Add(invoiceItem);
            }

            CalculatePrice();
        }

        public void MakePayment(Price amountPaid) {
            var newAmount = new Price(AmountPaid.Amount + amountPaid.Amount);
            AmountPaid = newAmount;

            if (TotalAmount.Amount - AmountPaid.Amount == 0) {
                PaymentStatus = InvoicePaymentStatus.FULLY_PAID;
            } 
            else {
                PaymentStatus = InvoicePaymentStatus.PAID_PARTIALLY;
            }
        }

        private void CalculatePrice()
        {
            var price = _invoiceItems.Sum(x => x.GetPrice());

            // get discount
            var discountedPrice = price * (decimal)(Discount / 100);
            var newPrice = price - discountedPrice;

            TotalAmount = new Price(newPrice);
        }

        private static string GenerateInvoiceNumber()
        {
            HashSet<int> existingInvoiceNumbers = [];
            Random random = new();
            int invoiceNumber;

            do
            {
                // Generate a 7-digit number
                invoiceNumber = random.Next(1000000, 10000000); // Range: 1000000 to 9999999
            } while (existingInvoiceNumbers.Contains(invoiceNumber));

            // Add the new invoice number to the set of existing numbers
            existingInvoiceNumbers.Add(invoiceNumber);

            return invoiceNumber.ToString();
        }
    }
}