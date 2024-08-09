using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Services;
using Jemma.Domain.Aggregates;
using MediatR;

namespace Jemma.Application.Authentication.Queries
{
    public record GetUsersQuery(): IRequest<ErrorOr<List<User>>>;

    public class GetUsersQueryHandler(IIdentityService _identityService) : IRequestHandler<GetUsersQuery, ErrorOr<List<User>>>
    {
        public async Task<ErrorOr<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetUsersAsync();
        }
    }
}