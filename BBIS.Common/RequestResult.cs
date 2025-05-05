namespace BBIS.Common
{
    public class RequestResult : RequestResult<object>
    {
        public RequestResult() { }
        public RequestResult(RequestResult<object> result)
        {
            Success = result.Success;
            Message = result.Message;
            Data = result.Data;
            StatusCode = result.StatusCode;
        }
    }

    public class RequestResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
