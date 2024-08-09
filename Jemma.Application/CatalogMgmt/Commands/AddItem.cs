using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Application.Common.Errors;
using Jemma.Domain.Aggregates;
using Jemma.Domain.Entities;
using Jemma.Domain.ValueObjects;
using MediatR;

namespace Jemma.Application.CatalogMgmt.Commands
{
    public record AddItemCmd(string Name, string Image, List<ItemServices> Services): IRequest<ErrorOr<Item>>;
    public record ItemServices(string Name, Price Price);

    public class AddItemCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<AddItemCmd, ErrorOr<Item>>
    {
        public async Task<ErrorOr<Item>> Handle(AddItemCmd request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.Item.FilterAsync([x => x.Name.Equals(request.Name)]);

            if (items.Any()) {
                return new Error[] { Errors.Catalog.Conflict };
            }

            var item = Item.Create(request.Name, request.Image);

            foreach (var service in request.Services.Select(x => Service.Create(item.Id, x.Name, x.Price))) {
                item.AddService(service);
            }

            await _unitOfWork.Item.AddAsync(item);
            await _unitOfWork.CommitAsync();

            return item;
        }
    }
}