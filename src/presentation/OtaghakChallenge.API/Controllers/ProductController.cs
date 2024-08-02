using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtaghakChallenge.Application.Features.Products.Command;
using OtaghakChallenge.Application.Features.Products.Query;
using System.Net;

namespace OtaghakChallenge.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("ActiveProductList")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GerActiveProductListAsync()
        {
            var Product = await _mediator.Send(new GetProductQuery());

            return Ok();
        }

        [HttpPost("AddNewProduct")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddNewProductAsync([FromBody] ProductCommand productCommand)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var productId = await _mediator.Send(productCommand);

            return Ok(0);
        }
    }
}
