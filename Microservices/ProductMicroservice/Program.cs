
namespace ProductMicroservice
{
    using EasyNetQ;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProductMicroservice.Models;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProductMicroservice.Services;
    using ProductMicroservice.MessageBroker;
    using ProductMicroservice.Enums;
    using DeliVeggieSharedLibrary.Interfaces;
    using ProductMicroservice.Model;

    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var services = ConfigureServices();
                var serviceProvider = services.BuildServiceProvider();
                var productService = serviceProvider.GetRequiredService<ProductService>();
                var rabbitMq = serviceProvider.GetRequiredService<IBus>();

                await RabbitHutchSubscription.Subscription(productService, rabbitMq);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();

        }

        static IConfiguration Configuration;
        private static IServiceCollection ConfigureServices()
        {
            Configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            var cosmosDBConfig = new CosmosDBConfig();
            Configuration.GetSection(nameof(DBName.CosmosDb)).Bind(cosmosDBConfig);

            var rabbitMqConfig = new RabbitMqConfig();
            Configuration.GetSection(nameof(ConfigSection.RabbitMq)).Bind(rabbitMqConfig);

            var services = new ServiceCollection();

            services.AddScoped<IDbService, CosmosDbService>();
            services.AddTransient<IReductionCalculation, ReductionCalculation>();
            services.AddScoped<ProductService>();

            services.AddSingleton(RabbitHutch.CreateBus(rabbitMqConfig.ConnectionString));

            services.AddSingleton(new CosmosClient(cosmosDBConfig.Account, cosmosDBConfig.Key));
            services.Configure<CosmosDBConfig>(Configuration.GetSection(nameof(DBName.CosmosDb)));

            return services;
        }
    }
}

