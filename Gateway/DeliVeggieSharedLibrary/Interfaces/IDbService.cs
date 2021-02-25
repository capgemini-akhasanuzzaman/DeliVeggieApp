namespace DeliVeggieSharedLibrary.Interfaces
{
    using Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IDbService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<double> GetReductionforTodayAsync(DayOfWeek dayofWeek);
    }
}
