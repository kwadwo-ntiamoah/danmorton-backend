using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Aggregates;
using MediatR;

namespace Jemma.Application.Payments.Queries
{
    public record GetPaymentsQuery(string? PaidBy, DateTime? Date): IRequest<ErrorOr<List<Payment>>>;

    public record GetPaymentsQueryHandler(IUnitOfWork UnitOfWork) : IRequestHandler<GetPaymentsQuery, ErrorOr<List<Payment>>>
    {
        public async Task<ErrorOr<List<Payment>>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Payment, bool>>> predicates = [];

            if (request.PaidBy != null)
            {
                predicates.Add(x => x.PaidBy == request.PaidBy);
            }

            if (request.Date != null) {
                predicates.Add(x => x.DateCreated == request.Date);
            }

            var payments = await UnitOfWork.Payment.FilterAsync(predicates);
            return payments.ToList();
        }
    }
}