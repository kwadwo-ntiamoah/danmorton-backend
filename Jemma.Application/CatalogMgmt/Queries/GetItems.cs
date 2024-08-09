using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Aggregates;
using MediatR;

namespace Jemma.Application.CatalogMgmt.Queries
{
    public record GetItemsQuery(): IRequest<ErrorOr<List<Item>>>;

    public class AddItemCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetItemsQuery, ErrorOr<List<Item>>>
    {
        public async Task<ErrorOr<List<Item>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.Item.FilterAsync([], [x => x.Services]);
            return items.ToList();
        }
    }
}