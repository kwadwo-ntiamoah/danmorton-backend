using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Aggregates;
using Jemma.Domain.Entities;
using Jemma.Domain.ValueObjects;
using MediatR;

namespace Jemma.Application.Invoicing.Commands
{
    public record GenerateInvoiceCmd(
        double Discount, string RecipientName, string RecipientContact,
        string RecipientAddress, List<InvoiceItemCmd> InvoiceItems) : IRequest<ErrorOr<Invoice>>;

    public record InvoiceItemCmd(string Name, string Description, string ServiceType, decimal ServiceAmount, int Quantity);

    public class GenerateInvoiceCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GenerateInvoiceCmd, ErrorOr<Invoice>>
    {
        public async Task<ErrorOr<Invoice>> Handle(GenerateInvoiceCmd request, CancellationToken cancellationToken)
        {
            // create invoice
            var invoice = Invoice.Create(request.Discount, request.RecipientName, request.RecipientContact, request.RecipientAddress);

            // add invoice items
            var tempInvoiceItems = request.InvoiceItems.Select(x => InvoiceItem.Create(
                invoice.Id,
                x.Name,
                x.Description,
                x.ServiceType,
                new Price(x.ServiceAmount),
                x.Quantity)).ToList();

            foreach (var item in tempInvoiceItems)
            {
                invoice.AddInvoiceItem(item);
            }

            // create customer
            var contact = invoice.BillTo;

            var tempCustomer = await _unitOfWork.Customer.FilterAsync([c => c.Name == contact.Name && c.Contact == contact.Contact]);

            if (!tempCustomer.Any())
            {
                var customer = Customer.Create(contact.Name, contact.Contact, contact.Address);
                await _unitOfWork.Customer.AddAsync(customer);
            }

            await _unitOfWork.InvoiceItem.AddRangeAsync(invoice.InvoiceItems);
            await _unitOfWork.Invoice.AddAsync(invoice);
            await _unitOfWork.CommitAsync();

            return invoice;
        }
    }
}