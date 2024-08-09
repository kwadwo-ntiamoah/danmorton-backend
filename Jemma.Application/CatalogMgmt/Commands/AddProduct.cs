using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Application.Common.Errors;
using Jemma.Domain.Entities;
using Jemma.Domain.ValueObjects;
using MediatR;

namespace Jemma.Application.CatalogMgmt.Commands
{
    public record AddProductCmd(string Name, string Description): IRequest<ErrorOr<Product>>;

    public class AddProductCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<AddProductCmd, ErrorOr<Product>>
    {
        public async Task<ErrorOr<Product>> Handle(AddProductCmd request, CancellationToken cancellationToken)
        {
            var Products = await _unitOfWork.Product.FilterAsync([x => x.Name.Equals(request.Name)]);

            if (Products.Any()) {
                return new Error[] { Errors.Catalog.Conflict };
            }

            var product = Product.Create(request.Name, request.Description);
            
            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.CommitAsync();

            return product;
        }
    }
}