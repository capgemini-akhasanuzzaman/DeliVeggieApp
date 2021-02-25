namespace ProductMicroservice.Services
{
    using DeliVeggieSharedLibrary.Interfaces;
    using DeliVeggieSharedLibrary.Models;
    using Domain.Model;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class ProductService
    {
        private readonly IDbService _cosmosDbService;
        private readonly IReductionCalculation _reductionCalculation;
        public ProductService(IDbService cosmosDbService, IReductionCalculation reductionCalculation)
        {
            _cosmosDbService = cosmosDbService;
            _reductionCalculation = reductionCalculation;
        }
        public async Task<ProductDetailsResponse> GetProductResponseByIdAsync(int productId)
        {
            var product = await _cosmosDbService.GetProductByIdAsync(productId);

            DateTime entryDate;
            DateTime.TryParse(product.EntryDate, out entryDate);

            var reducedPriceForToday = await GetReducedPriceForToday(product.Price);

            var productResponse = new ProductDetailsResponse()
            {
                Product = new Product()
                {
                    Name = product.Name,
                    ProductId = product.ProductId,
                    EntryDate = product.EntryDate == null ? null : entryDate.ToLongDateString(),
                    Price = reducedPriceForToday
                }
            };

            return productResponse;
        }

        private async Task<double> GetReducedPriceForToday(double price)
            => await _reductionCalculation.GetReducedPrice(price);

        public async Task<ProductsResponse> GetAllProductResponseAsync()
        {
            var products = await _cosmosDbService.GetProductsAsync();

            var listOfProduct = products
                            .ToList()
                            .Select(p => new Product()
                            {
                                EntryDate =DateTime.Parse(p?.EntryDate).ToLongDateString(),
                                Name = p.Name,
                                Price = p.Price,
                                ProductId = p.ProductId
                            });

            var productResponse = new ProductsResponse()
            {
                ProductList = listOfProduct?.ToList()
            };

            return productResponse;
        }
    }
}
