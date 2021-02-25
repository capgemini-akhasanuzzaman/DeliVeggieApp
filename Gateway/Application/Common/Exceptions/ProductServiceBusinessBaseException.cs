
namespace Application.Common.Exceptions
{
    using System;

    public abstract class ProductServiceBusinessBaseException : ProductServiceBaseException
    { 
        protected ProductServiceBusinessBaseException() { }
        protected ProductServiceBusinessBaseException(string message) : base(message) { }
        protected ProductServiceBusinessBaseException(string message, Exception inner) : base(message, inner) { }
        protected ProductServiceBusinessBaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
