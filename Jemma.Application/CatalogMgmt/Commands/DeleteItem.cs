using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Application.Common.Errors;
using Jemma.Domain.ValueObjects;
using MediatR;

namespace Jemma.Application.CatalogMgmt.Commands
{
    public record DeleteItemCmd(Guid ItemId): IRequest<ErrorOr<NoValue>>;

    public class DeleteItemCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteItemCmd, ErrorOr<NoValue>>
    {
        public async Task<ErrorOr<NoValue>> Handle(DeleteItemCmd request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.Item.GetByIdAsync(request.ItemId);

            if (item == null) {
                return new Error[] { Errors.Catalog.NotFound };
            }

            _unitOfWork.Item.DeleteAsync(item);
            await _unitOfWork.CommitAsync();

            return NoValue.New();
        }
    }
}