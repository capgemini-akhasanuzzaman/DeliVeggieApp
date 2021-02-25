
namespace Application.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class ProductWithSpecifiedIdNotFoundException : ProductServiceNotFoundBaseException
    {
        public ProductWithSpecifiedIdNotFoundException() { }
        public ProductWithSpecifiedIdNotFoundException(string message) : base(message) { }
        public ProductWithSpecifiedIdNotFoundException(string message, Exception inner) : base(message, inner) { }
        private ProductWithSpecifiedIdNotFoundException(
          SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override string Reason => "Product with Specified Id Not Found";
    }
}
