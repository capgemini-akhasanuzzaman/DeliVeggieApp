using Domain.Model;

namespace Application.Shared.Model
{
    public class ProductDetailsResponse
    {
        public Product Product { get; set; } = new Product();
    }
}
