using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Services;
using Jemma.Application.Common.Errors;
using MediatR;

namespace Jemma.Application.Authentication.Queries
{
    public record LoginQuery(string Username, string Password): IRequest<ErrorOr<AuthResult>>;

    public record LoginQueryHandler(IIdentityService _identityService) : IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
    {
        public async Task<ErrorOr<AuthResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var res = await _identityService.LoginAsync(request.Username, request.Password);

            if (res == null) {
                return new Error[] { Errors.User.InvalidLogin };
            }

            return res;
        }
    }
}