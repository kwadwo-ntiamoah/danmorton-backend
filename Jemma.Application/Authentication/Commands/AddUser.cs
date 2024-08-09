using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Services;
using Jemma.Application.Common.Errors;
using Jemma.Domain.Aggregates;
using MediatR;

namespace Jemma.Application.Authentication.Commands
{
    public record AddUserCmd(string Email, string FullName, string Role): IRequest<ErrorOr<User>>;

    public class AddUserCmdHandler(IIdentityService _identityService) : IRequestHandler<AddUserCmd, ErrorOr<User>>
    {
        public async Task<ErrorOr<User>> Handle(AddUserCmd request, CancellationToken cancellationToken)
        {
            var user = await _identityService.AddUserAsync(request.Email, request.FullName, request.Role);

            if (user == null) {
                return new Error[] { Errors.User.CreateError };
            }

            return user;
        }
    }
}