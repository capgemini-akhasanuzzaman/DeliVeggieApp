
namespace Application.Products.Handlers
{
    using Application.Common.Services;
    using Application.Products.Queries;
    using DeliVeggieSharedLibrary.Models;
    using EasyNetQ;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class GetAllProductByIdQueryHandler : IRequestHandler<GetAllProductByIdQuery, ProductDetailsResponse>
    {
        public async Task<ProductDetailsResponse> Handle(GetAllProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productResponse = await SingleTonRabbitHutch.RabbitHutchObject.Rpc
                .RequestAsync<ProductDetailsRequest, ProductDetailsResponse>(new ProductDetailsRequest
                {
                    ProductId = request.ProductId
                });

            return productResponse;
        }
    }
}
