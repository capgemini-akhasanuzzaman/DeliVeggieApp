namespace ProductMicroservice.MessageBroker
{
    using DeliVeggieSharedLibrary.Models;
    using EasyNetQ;
    using System.Threading.Tasks;
    internal static class RabbitHutchSubscription
    {
        public static async Task Subscription(Services.ProductService productService,IBus rabbitMq)
        {
            await rabbitMq.Rpc.RespondAsync<ProductsRequest, ProductsResponse>(
                async (request) =>
                {
                    var r = await productService.GetAllProductResponseAsync();
                    return r;
                });

            await rabbitMq.Rpc.RespondAsync<ProductDetailsRequest, ProductDetailsResponse>(
                async (request) =>
                {
                    return request.ProductId > 0 ?
                    await productService.GetProductResponseByIdAsync(request.ProductId) : null;
                });
        }
    }
}