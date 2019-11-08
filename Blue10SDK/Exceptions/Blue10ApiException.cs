using System;

namespace Blue10SDK.Exceptions
{
    public class Blue10ApiException : Exception
    {
        public Blue10ApiException(){}
        public Blue10ApiException(string pMessage) : base(pMessage){}
        public Blue10ApiException(string message, Exception innerException) : base(message, innerException){}
    }
}