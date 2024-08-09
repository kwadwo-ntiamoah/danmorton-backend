using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Application.CustomerMgmt.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jemma.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController(ISender _mediatR) : ApiController
    {
        [HttpGet()]
        public async Task<IActionResult> GetCustomersAsync([FromQuery] GetCustomersQuery query) {
            var results = await _mediatR.Send(query);
            return results.Match(Ok, Problem);
        }
    }
}