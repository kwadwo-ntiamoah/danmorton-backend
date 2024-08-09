using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Application.Common.Errors;
using Jemma.Domain.Aggregates;
using MediatR;

namespace Jemma.Application.CatalogMgmt.Commands
{
    public record UpdateItemCmd(Guid Id, string Name, string Image): IRequest<ErrorOr<Item>>;

    public class UpdateItemCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateItemCmd, ErrorOr<Item>>
    {
        public async Task<ErrorOr<Item>> Handle(UpdateItemCmd request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.Item.GetByIdAsync(request.Id);

            if (item == null) {
                return new Error[] { Errors.Catalog.NotFound };
            }
            
            item.UpdateItem(request.Name, request.Image);

             _unitOfWork.Item.UpdateAsync(item);
            await _unitOfWork.CommitAsync();

            return item;
        }
    }
}