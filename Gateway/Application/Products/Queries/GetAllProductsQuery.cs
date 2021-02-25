namespace Application.Products.Queries
{
    using DeliVeggieSharedLibrary.Models;
    using MediatR;
    public class GetAllProductsQuery : IRequest<ProductsResponse> { }
}
