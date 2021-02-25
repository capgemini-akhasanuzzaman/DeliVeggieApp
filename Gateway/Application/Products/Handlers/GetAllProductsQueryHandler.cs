
namespace ProductService.Handlers
{
    using Application.Common.Services;
    using Application.Products.Queries;
    using DeliVeggieSharedLibrary.Models;
    using EasyNetQ;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    internal sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductsResponse>
    {
        public async Task<ProductsResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productResponse =  await SingleTonRabbitHutch
                    .RabbitHutchObject.Rpc
                    .RequestAsync<ProductsRequest, ProductsResponse>(new ProductsRequest{});

            return productResponse;
        }
    }
}