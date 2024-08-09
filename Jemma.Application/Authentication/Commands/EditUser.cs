using ErrorOr;
using Jemma.Application.Common.Abstractions.Services;
using Jemma.Application.Common.Errors;
using Jemma.Domain.Aggregates;
using MediatR;

namespace Jemma.Application.Authentication.Commands
{
    public record EditUserCmd(string Email, string FullName, string Role): IRequest<ErrorOr<User>>;

    public class EditUserCmdHandler(IIdentityService _identityService) : IRequestHandler<EditUserCmd, ErrorOr<User>>
    {
        public async Task<ErrorOr<User>> Handle(EditUserCmd request, CancellationToken cancellationToken)
        {
            var user = await _identityService.UpdateUserAsync(request.Email, request.FullName, request.Role);

            if (user == null) {
                return new Error[] { Errors.User.UpdateError };
            }

            return user;
        }
    }
}