
namespace Application.Common.Services
{
    using EasyNetQ;
    internal sealed class SingleTonRabbitHutch
    {
        private SingleTonRabbitHutch() { }

        private static readonly object padLock = new object();
        private static IBus singleTonRabbitHutch = null;

        public static IBus RabbitHutchObject
        {
            get
            {
                lock (padLock)
                {
                    if (singleTonRabbitHutch == null)
                    {
                        singleTonRabbitHutch = RabbitHutch.CreateBus("host=localhost");
                    }
                    return singleTonRabbitHutch;
                }
            }
        }
    }
}
