using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jemma.Api.Contracts;
using Jemma.Application.Invoicing.Commands;
using Jemma.Application.Invoicing.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jemma.Api.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceController(ISender _mediatR) : ApiController
    {
        [HttpGet()]
        public async Task<IActionResult> GetInvoicesAsync([FromQuery] GetInvoicesDto request) {
            var query = new GetInvoicesQuery(request.Status, request.CustomerName, request.InvoiceId, request.Date);
            var results = await _mediatR.Send(query);

            return results.Match(Ok, Problem);
        }

        [HttpPost()]
        public async Task<IActionResult> GenerateInvoiceAsync(GenerateInvoiceCmd request) {
            var results = await _mediatR.Send(request);

            return results.Match(Ok, Problem);
        }
    }
}