using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Persistence;
using Jemma.Domain.Entities;
using MediatR;

namespace Jemma.Application.CatalogMgmt.Queries
{
    public record GetProductsQuery(): IRequest<ErrorOr<List<Product>>>;

    public class AddProductCmdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetProductsQuery, ErrorOr<List<Product>>>
    {
        public async Task<ErrorOr<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Product.GetAllAsync();
            return products.ToList();
        }
    }
}