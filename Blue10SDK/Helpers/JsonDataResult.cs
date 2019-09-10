using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDK
{
    public abstract class JsonDataResult<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
