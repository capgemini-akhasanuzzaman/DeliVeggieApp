
namespace ProductService.Controllers
{
    using Application.Products.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
            => Ok(await _mediator.Send(new GetAllProductsQuery()));


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _mediator.Send(new GetAllProductByIdQuery(productId));

            return product == null ? NotFound() : (IActionResult)Ok(product);
        }
    }
}
