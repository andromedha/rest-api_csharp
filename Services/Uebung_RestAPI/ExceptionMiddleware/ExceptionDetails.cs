using System;
using System.Runtime.Serialization;

namespace Uebung_RestAPI.ExceptionMiddleware
{
    [DataContract]
    public class ExceptionDetails
    {
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public Exception Exception { get; set; }
        [DataMember]
        public Exception InnerException { get; set; }

    }
}
