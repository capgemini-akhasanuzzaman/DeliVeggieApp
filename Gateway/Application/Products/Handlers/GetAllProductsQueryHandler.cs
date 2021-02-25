
namespace ProductService.Handlers
{
    using Application.Products.Queries;
    using DeliVeggieSharedLibrary.Models;
    using EasyNetQ;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    internal sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductsResponse>
    {
        private readonly IBus _rabbitMq;
        public GetAllProductsQueryHandler(IBus rabbitMq)
        {
            _rabbitMq = rabbitMq;
        }
        public async Task<ProductsResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productResponse =  await _rabbitMq.Rpc
                    .RequestAsync<ProductsRequest, ProductsResponse>(new ProductsRequest{});

            return productResponse;
        }
    }
}