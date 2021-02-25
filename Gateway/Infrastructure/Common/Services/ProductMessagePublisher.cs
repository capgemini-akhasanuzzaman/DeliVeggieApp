using Application.Shared.Interfaces;
using EasyNetQ;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
    public class ProductMessagePublisher : IMessagePublisher
    {
        public async Task Publish<Product>(Product product)
        {
            await SingleTonRabbitHutch.RabbitHutchObject.PubSub.PublishAsync(product);
        }
    }
}
