using System.Linq.Expressions;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Aggregates;
using Jemma.Domain.Enums;
using MediatR;

namespace Jemma.Application.BasketMgmt.Queries
{
    public record GetBasketsQuery(WashStatus? Status, string? BasketNumber): IRequest<ErrorOr<List<Basket>>>;

    public class GetBasketsQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetBasketsQuery, ErrorOr<List<Basket>>> {
        public async Task<ErrorOr<List<Basket>>> Handle(GetBasketsQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Basket, bool>>> predicates = [];
            Expression<Func<Basket, object>>[] includes = [x => x.BasketItems];

            if (request.Status != null) {
                predicates.Add(x => x.Status == request.Status);
            }

            if (request.BasketNumber != null) {
                predicates.Add(x => x.BasketNumber == request.BasketNumber);
            }

            var baskets = await _unitOfWork.Basket.FilterAsync(predicates, includes);
            return baskets.ToList();
        }
    }
}