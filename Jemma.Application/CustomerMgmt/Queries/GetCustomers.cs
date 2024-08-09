using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Entities;
using MediatR;

namespace Jemma.Application.CustomerMgmt.Queries
{
    public record GetCustomersQuery(DateTime? Date = null): IRequest<ErrorOr<List<Customer>>>;

    public class GetCustomersQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetCustomersQuery, ErrorOr<List<Customer>>>
    {
        public async Task<ErrorOr<List<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Customer, bool>>> predicates = [];

            if (request.Date != null) {
                predicates.Add(x => x.DateCreated == request.Date);
            }

            var customers = await _unitOfWork.Customer.FilterAsync(predicates);
            return customers.ToList();
        }
    }
}