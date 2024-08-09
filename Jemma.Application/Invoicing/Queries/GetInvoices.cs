using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Aggregates;
using Jemma.Domain.Enums;
using MediatR;

namespace Jemma.Application.Invoicing.Queries
{
    public record GetInvoicesQuery(InvoicePaymentStatus? Status, string? CustomerName, Guid? InvoiceId, DateTime? Date) : IRequest<ErrorOr<List<Invoice>>>;

    public class GetInvoicesQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetInvoicesQuery, ErrorOr<List<Invoice>>>
    {
        public async Task<ErrorOr<List<Invoice>>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Invoice, bool>>> predicates = [];

            if (request.CustomerName != null)
            {
                predicates.Add(x => x.BillTo.Name == request.CustomerName);
            }

            if (request.Date != null) {
                predicates.Add(x => x.DateCreated == request.Date);
            }

            if (request.InvoiceId != null) {
                predicates.Add(x => x.Id == request.InvoiceId.Value);
            }

            if (request.Status != null) {
                predicates.Add(x => x.PaymentStatus == request.Status);
            }

            var invoices = await _unitOfWork.Invoice.FilterAsync(predicates, [x => x.InvoiceItems]);
            return invoices.ToList();
        }
    }
}