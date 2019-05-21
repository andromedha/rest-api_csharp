using System;
using System.Runtime.Serialization;

namespace PersonManagement.Contracts
{
    [Serializable]
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException()
        {
        }

        public DataNotFoundException(string message) : base(message)
        {
        }

        public DataNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}