namespace DeliVeggieSharedLibrary.Interfaces
{
    using System.Threading.Tasks;
    public interface IReductionCalculation
    {
        Task<double> GetReducedPrice(double price);
    }
}
