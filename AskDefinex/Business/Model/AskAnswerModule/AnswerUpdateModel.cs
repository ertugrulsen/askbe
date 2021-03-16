namespace AskDefinex.Business.Model.AskAnswerModule
{
    public class AnswerUpdateModel : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
    }
}
