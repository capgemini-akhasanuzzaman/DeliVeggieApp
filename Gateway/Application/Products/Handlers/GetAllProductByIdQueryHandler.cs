
namespace Application.Products.Handlers
{
    using Application.Products.Queries;
    using DeliVeggieSharedLibrary.Models;
    using EasyNetQ;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetAllProductByIdQueryHandler : IRequestHandler<GetAllProductByIdQuery, ProductDetailsResponse>
    {
        private readonly IBus _rabbitMq;
        public GetAllProductByIdQueryHandler(IBus rabbitMq)
        {
            _rabbitMq = rabbitMq;
        }
        public async Task<ProductDetailsResponse> Handle(GetAllProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productResponse = await _rabbitMq.Rpc
                .RequestAsync<ProductDetailsRequest, ProductDetailsResponse>(new ProductDetailsRequest
                {
                    ProductId = request.ProductId
                });

            return productResponse;
        }
    }
}
