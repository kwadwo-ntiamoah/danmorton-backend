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
    public record DeleteUserCmd(string Email): IRequest<ErrorOr<NoValue>>;

    public class DeleteUserCmdHandler(IIdentityService _identityService) : IRequestHandler<DeleteUserCmd, ErrorOr<NoValue>>
    {
        public async Task<ErrorOr<NoValue>> Handle(DeleteUserCmd request, CancellationToken cancellationToken)
        {
            await _identityService.DeleteUserAsync(request.Email);
            return NoValue.New();
        }
    }
}