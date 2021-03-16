namespace AskDefinex.Business.Model.AskQuestionModule
{
    public class QuestionSearchModel : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Header { get; set; }
        public string Detail { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
    }
}
