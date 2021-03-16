namespace AskDefinex.Rest.Model.Request.AskAnswerModule
{
    public class AnswerCreateRequestModel
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
