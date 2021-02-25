namespace ProductMicroservice.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DeliVeggieSharedLibrary.Interfaces;
    using Domain.Model;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Options;
    using ProductMicroservice.Models;

    internal sealed class CosmosDbService : IDbService
    {
        private Container _productContainer;
        private Container _productReductionContainer;

        IOptions<CosmosDBConfig> _cosmosDBConfig;
        CosmosClient _cosmosClient;

        public CosmosDbService(IOptions<CosmosDBConfig> cosmosDBConfig, CosmosClient cosmosClient)
        {
            _cosmosDBConfig = cosmosDBConfig;
            ConnectToContainer(cosmosClient);
        }

        private void ConnectToContainer(CosmosClient cosmosClient)
        {
            try
            {
                var databaseName = _cosmosDBConfig.Value.DatabaseName;
                var productContainer = _cosmosDBConfig.Value.ProductContainer;
                var productReductionContainer = _cosmosDBConfig.Value.ProductReductionContainer;

                _cosmosClient = cosmosClient;
                var database = _cosmosClient
                                    .CreateDatabaseIfNotExistsAsync(databaseName)
                                        .GetAwaiter()
                                            .GetResult();
                database.Database
                    .CreateContainerIfNotExistsAsync(productContainer, "/productid")
                        .GetAwaiter()
                         .GetResult();

                _productContainer = _cosmosClient.GetContainer(databaseName, productContainer);

                database.Database
                    .CreateContainerIfNotExistsAsync(productReductionContainer, "/dayofweek")
                        .GetAwaiter()
                         .GetResult();

                _productReductionContainer = _cosmosClient.GetContainer(databaseName, productReductionContainer);
            }
            catch (Exception ex)
            {
                //Log exception
            }
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            try
            {
                var queryAllProductById = $"select * from c where c.productid  = '{productId}' ";
                var query = _productContainer.GetItemQueryIterator<Product>(new QueryDefinition(queryAllProductById));

                var results = new List<Product>();

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();

                    results.AddRange(response.ToList());
                }
                return results.FirstOrDefault();
            }
            catch (CosmosException ex)
                    when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                var queryString = "select * from c";
                var query = _productContainer.GetItemQueryIterator<Product>(new QueryDefinition(queryString));

                var results = new List<Product>();
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();

                    results.AddRange(response.ToList());
                }
                return results;
            }
            catch (Exception ex)
            {
                //Log exception
                return new List<Product>();
            }
        }
        public async Task<double> GetReductionforTodayAsync(DayOfWeek dayOfWeek)
        {
            try
            {
                var queryReduction = new QueryDefinition($"select * from c where c.dayofweek = '{dayOfWeek}' ");

                var query = _productReductionContainer
                                .GetItemQueryIterator<ProductReduction>();

                var results = new List<ProductReduction>();

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();

                    results.AddRange(response.ToList());
                }
                return results?.FirstOrDefault() == null ? 0 : results.FirstOrDefault().Reduction;

            }
            catch (Exception ex)
            {
                //Log exception

                return 0;
            }
        }
    }

}
