using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Jemma.Application.Common.Abstractions.Services;
using Jemma.Domain.Aggregates;
using Jemma.Domain.ValueObjects;
using MediatR;

namespace Jemma.Application.Authentication.Commands
{
    public record ChangePasswordCmd(string Username, string CurrentPassword, string NewPassword): IRequest<ErrorOr<NoValue>>;

    public class ChangePasswordCmdHandler(IIdentityService _identityService) : IRequestHandler<ChangePasswordCmd, ErrorOr<NoValue>>
    {
        public async Task<ErrorOr<NoValue>> Handle(ChangePasswordCmd request, CancellationToken cancellationToken)
        {
            await _identityService.ChangePasswordAsync(request.Username, request.CurrentPassword, request.NewPassword);
            return NoValue.New();
        }
    }
}