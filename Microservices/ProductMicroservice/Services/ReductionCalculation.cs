
namespace ProductMicroservice.Services
{
    using DeliVeggieSharedLibrary.Interfaces;
    using Moq;
    using System;
    using System.Threading.Tasks;

    internal sealed class ReductionCalculation : IReductionCalculation
    {
        private readonly IDbService _cosmosDbService;
        public ReductionCalculation(IDbService cosmosDbService)
            => _cosmosDbService = cosmosDbService;

        public async Task<double> GetReducedPrice(double price)
        {
            var reduction = await GetReductionForTodayAsync(DateTime.Now.DayOfWeek);
            var productPriceToday = reduction > 0 ? price * reduction : price;
            return productPriceToday;
        }

        private async Task<double> GetReductionForTodayAsync(DayOfWeek dayOfWeek)
            => await _cosmosDbService.GetReductionforTodayAsync(dayOfWeek);
    }
}
