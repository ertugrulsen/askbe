namespace AskDefinex.Business.Model
{
    public class QuestionUpdateModel : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Header { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public string Detail { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }


    }
}
