namespace Application.Common.Exceptions
{
    using System;

    [Serializable]
    public abstract class ProductServiceBaseException : Exception
    {
        public abstract string Reason { get; }
        protected ProductServiceBaseException() { }
        protected ProductServiceBaseException(string message) : base(message) { }
        protected ProductServiceBaseException(string message, Exception inner) : base(message, inner) { }
        protected ProductServiceBaseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
