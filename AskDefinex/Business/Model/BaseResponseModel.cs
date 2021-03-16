namespace AskDefinex.Business.Model
{
    public class BaseResponseModel
    {
        public bool IsSucceed { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
