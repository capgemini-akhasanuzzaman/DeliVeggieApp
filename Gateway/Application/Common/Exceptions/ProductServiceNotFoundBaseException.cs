namespace Application.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class ProductServiceNotFoundBaseException : ProductServiceBaseException
    {
        protected ProductServiceNotFoundBaseException() { }
        protected ProductServiceNotFoundBaseException(string message) : base(message) { }
        protected ProductServiceNotFoundBaseException(string message, Exception inner) : base(message, inner) { }
        protected ProductServiceNotFoundBaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
