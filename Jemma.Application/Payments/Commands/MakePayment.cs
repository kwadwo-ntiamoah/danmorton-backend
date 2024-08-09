using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Application.Common.Errors;
using Jemma.Domain.Aggregates;
using Jemma.Domain.Entities;
using Jemma.Domain.Enums;
using Jemma.Domain.ValueObjects;
using MediatR;

namespace Jemma.Application.Payments.Commands
{
    public record MakePaymentCmd(Guid InvoiceId, decimal Amount, string PaidBy, PaymentMethod PaymentMethod) : IRequest<ErrorOr<Payment>>;

    public class MakePaymentCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<MakePaymentCmd, ErrorOr<Payment>>
    {
        public async Task<ErrorOr<Payment>> Handle(MakePaymentCmd request, CancellationToken cancellationToken)
        {
            // get invoice
            var invoices = await _unitOfWork.Invoice.FilterAsync([invoice => invoice.Id == request.InvoiceId], [x => x.InvoiceItems]);
            var invoice = invoices.FirstOrDefault();

            if (invoice == null)
            {
                return new Error[] { Errors.Invoice.NotFound };
            }

            // get amount left to be paid
            var amountLeft = invoice.TotalAmount.Amount - invoice.AmountPaid.Amount;

            // if incoming amount is greater, don't accept payment.. already paid
            if (request.Amount > amountLeft)
            {
                return new Error[] { Errors.Invoice.OverPosting(amountLeft) };
            }

            // make payment
            var payment = Payment.Create(invoice.Id, new Price(request.Amount), request.PaidBy, request.PaymentMethod);
            invoice.MakePayment(payment.AmountPaid);

            var tempBaskets = await _unitOfWork.Basket.FilterAsync([x => x.BasketNumber == invoice.InvoiceNumber]);

            if (!tempBaskets.Any())
            {
                var basket = Basket.Create(invoice.InvoiceNumber);

                foreach (var item in invoice.InvoiceItems)
                {
                    foreach (var i in Enumerable.Range(1, item.Quantity))
                    {
                        var basketItem = BasketItem.Create(basket.Id, item.Name, item.Description, "", "");
                        basket.AddBasketItems(basketItem);
                    }
                }
                await _unitOfWork.Basket.AddAsync(basket);
                await _unitOfWork.BasketItem.AddRangeAsync(basket.BasketItems);
            }

            await _unitOfWork.Payment.AddAsync(payment);
            _unitOfWork.Invoice.UpdateAsync(invoice);
            await _unitOfWork.CommitAsync();

            return payment;
        }
    }
}