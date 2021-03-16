namespace AskDefinex.Rest.Model.Response
{
    public class RestResponseContainer<T>
    {
        public T Response { get; set; }
        public bool IsSucceed { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
    }
}
