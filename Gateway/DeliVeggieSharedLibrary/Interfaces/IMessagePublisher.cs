namespace DeliVeggieSharedLibrary.Interfaces
{
    using System.Threading.Tasks;
    public interface IMessagePublisher
    {
        public Task Publish<T>(T obj);
    }
}
