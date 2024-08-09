using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Application.Authentication.Commands;
using Jemma.Application.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jemma.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController(ISender _mediatR) : ApiController
    {
        [HttpPost("token")]
        public async Task<IActionResult> TokenAsync(LoginQuery request) {
            var res = await _mediatR.Send(request);
            return res.Match(Ok, Problem);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync() {
            var res = await _mediatR.Send(new GetUsersQuery());
            return res.Match(Ok, Problem);
        }

        [HttpPost("users")]
        public async Task<IActionResult> AddUserAsync(AddUserCmd request) {
            var res = await _mediatR.Send(request);
            return res.Match(Ok, Problem);
        }

        [HttpPost("users/update")]
        public async Task<IActionResult> EditUserAsync(EditUserCmd request) {
            var res = await _mediatR.Send(request);
            return res.Match(Ok, Problem);
        }

        [HttpPost("users/delete")]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] string username) {
            var res = await _mediatR.Send(new DeleteUserCmd(username));
            return res.Match(Ok, Problem);
        }

        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCmd request) {
            var res = await _mediatR.Send(request);
            return res.Match(Ok, Problem);
        }
    }
}