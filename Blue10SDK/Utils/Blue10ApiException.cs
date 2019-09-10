using System;

namespace Blue10SDK
{
    public class Blue10ApiException : Exception
    {
        public Blue10ApiException(string pMessage) : base(pMessage)
        {
            
        }
    }
}