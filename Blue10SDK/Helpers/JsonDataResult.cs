namespace Blue10SDK
{
    public class JsonDataResult<T>
    {
        public string status { get; set; }

        public T data { get; set; }

        public string message { get; set; }

        public int code { get; set; }
        public string next { get; set; }
    }
}
