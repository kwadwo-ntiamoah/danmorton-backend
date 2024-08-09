using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Application.BasketMgmt.Commands;
using Jemma.Application.BasketMgmt.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jemma.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Basket(ISender _mediatR) : ApiController
    {
        [HttpGet("")]
        public async Task<IActionResult> GetBasketsAsync([FromQuery] GetBasketsQuery request) {
            var res = await _mediatR.Send(request);
            return res.Match(Ok, Problem);
        }

        [HttpPost("")]
        public async Task<IActionResult> UpdateBasketAsync(UpdateBasketCmd request) {
            var res = await _mediatR.Send(request);
            return res.Match(Ok, Problem);
        }
    }
}