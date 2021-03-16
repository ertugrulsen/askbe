namespace AskDefinex.Business.Model
{
    public class ResponseContainerBusinessModel<T>
    {
        public T Response { get; set; }
        public bool IsSucceed { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
