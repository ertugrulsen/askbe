namespace AskDefinex.Rest.Model.Request.AskAnswerModule
{
    public class AnswerUpdateRequestModel
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
    }
}
