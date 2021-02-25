namespace ProductMicroservice.Models
{
    internal sealed class CosmosDBConfig
    {
        public string Account { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }
        public string ProductContainer { get; set; }
        public string ProductReductionContainer { get; set; }
    }
}
