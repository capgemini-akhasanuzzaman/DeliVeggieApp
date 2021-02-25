
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ProductMicroservice
{
    internal  static class CosmosDBConnectionInitializer
    {
        public static async Task<CosmosClient> InitializeCosmosClientInstanceAsync(IOptions<CosmosDBConfig> cosmosDBConfig)
        {
            //var c = Configuration<CosmosDBConfig>(configuration.GetSection("CosmosDb"));
            //var databaseName = configurationSection.GetSection(nameof(CosmosEnum.DatabaseName)).Value;
            //var containerName = configurationSection.GetSection(nameof(CosmosEnum.ContainerName)).Value;
            //var account = configurationSection.GetSection(nameof(CosmosEnum.Account)).Value;
            //var key = configurationSection.GetSection(nameof(CosmosEnum.Key)).Value;

            //var account = configuration["CosmosDb:Account"];
            //var key = configuration["CosmosDb:Key"];
            //var databaseName = configuration["CosmosDb:DatabaseName"];
            //var containerName = configuration["CosmosDb:ContainerName"];

            //var account = cosmosDBConfig.Value.Account;
            //var key = cosmosDBConfig.Value.Key;
            //var databaseName = cosmosDBConfig.Value.DatabaseName;
            //var containerName = cosmosDBConfig.Value.ContainerName;

            //var client = new CosmosClient(account, key);
            //var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            //await database.Database.CreateContainerIfNotExistsAsync(containerName, "/productid");

            //return client;
            return null;
        }
    }
}
