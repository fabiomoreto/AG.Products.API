using AG.Products.API.Application.Commands;
using AG.Products.API.Application.Queries;
using AG.Products.API.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AG.Products.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : MainController
    {
        private readonly IProductQueries _productQueries;
        private readonly ISender _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductQueries productQueries,
            ISender mediator,
            ILogger<ProductsController> logger
            )
        {
            _productQueries = productQueries;
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/<Products>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken, [FromQuery] int page = 1, int pageSize = 10, string? searchTerm = null, bool activeOnly = true)
        {
            _logger.LogInformation("Serilog: Getting all products");

            var productsDto = await _productQueries.GetAllProducts(cancellationToken, page, pageSize, searchTerm, activeOnly);
            return Ok(productsDto);
        }

        // GET api/<Products>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productQueries.GetProductById(id, cancellationToken);

            if (product is null) return NotFound();

            return Ok(product);
        }

        // POST api/<Products>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
        {
            var requestResult = await _mediator.Send(request, cancellationToken);
            return CustomResponse(requestResult);
        }

        // PUT api/<Products>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var requestResult = await _mediator.Send(request, cancellationToken);
            return CustomResponse(requestResult);
        }

        // PUT api/<Products>/undelete/5
        [HttpPut("undelete/{id}")]
        public async Task<IActionResult> Undelete(Guid id, CancellationToken cancellationToken)
        {
            var requestResult = await _mediator.Send(new UndeleteProductCommand(id), cancellationToken);
            return CustomResponse(requestResult);
        }

        // DELETE api/<Products>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var requestResult = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return CustomResponse(requestResult);
        }
    }
}
