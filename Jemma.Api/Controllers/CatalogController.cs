using Jemma.Api.Contracts;
using Jemma.Application.CatalogMgmt.Commands;
using Jemma.Application.CatalogMgmt.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jemma.Api.Controllers
{
    [Route("api/[controller]")]
    public class CatalogController(ISender _mediatR) : ApiController
    {
        [HttpGet("items")]
        public async Task<IActionResult> GetItemsAsync() {
            var query = new GetItemsQuery();
            var results = await _mediatR.Send(query);

            return results.Match(Ok, Problem);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProductsAsync() {
            var query = new GetProductsQuery();
            var results = await _mediatR.Send(query);

            return results.Match(Ok, Problem);
        }

        [HttpPost("products")]
        public async Task<IActionResult> AddProductAsync(AddProductCmd request) {
            var results = await _mediatR.Send(request);

            return results.Match(Ok, Problem);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItemAsync(AddItemCmd request) {
            var results = await _mediatR.Send(request);

            return results.Match(Ok, Problem);
        }

        [HttpPost("items/{id:Guid}")]
        public async Task<IActionResult> UpdateItemAsync(Guid id, UpdateItemDto request) {
            var command = new UpdateItemCmd(id, request.Name, request.Image);
            var results = await _mediatR.Send(command);

            return results.Match(Ok, Problem);
        }

        [HttpPost("items/{id:Guid}/delete")]
        public async Task<IActionResult> DeleteItemAsync(Guid id) {
            var command = new DeleteItemCmd(id);
            var results = await _mediatR.Send(command);

            return results.Match(Ok, Problem);
        }
    }
}