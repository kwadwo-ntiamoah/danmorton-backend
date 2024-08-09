using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Application.Payments.Commands;
using Jemma.Application.Payments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jemma.Api.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController(ISender _mediatR) : ApiController
    {
        [HttpGet()]
        public async Task<IActionResult> GetAsync([FromQuery] GetPaymentsQuery request) {
            var results = await _mediatR.Send(request);
            return results.Match(Ok, Problem);
        }

        [HttpPost()]
        public async Task<IActionResult> PostAsync(MakePaymentCmd request) {
            var result = await _mediatR.Send(request);

            return result.Match(Ok, Problem);
        }
    }
}