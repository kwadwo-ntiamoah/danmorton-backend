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
using MediatR;

namespace Jemma.Application.BasketMgmt.Commands
{
    public record UpdateBasketCmd(string BasketNumber, WashStatus Status, List<UpdateBasketItemCmd> BasketItems): IRequest<ErrorOr<Basket>>;
    public record UpdateBasketItemCmd(string Name, string Color, string Print);

    public class UpdateBasketCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateBasketCmd, ErrorOr<Basket>>
    {
        public async Task<ErrorOr<Basket>> Handle(UpdateBasketCmd request, CancellationToken cancellationToken)
        {
            var baskets = await _unitOfWork.Basket.FilterAsync([x => x.BasketNumber == request.BasketNumber], [x => x.BasketItems]);

            if (!baskets.Any()) {
                return new Error[] { Errors.Catalog.NotFound };
            }

            var basket = baskets.First();
            
            // foreach(var item in request.BasketItems) {
            //     basket.AddBasketItems();
            // }

            _unitOfWork.Basket.UpdateAsync(basket);
            await _unitOfWork.CommitAsync();

            return basket;
        }
    }
}