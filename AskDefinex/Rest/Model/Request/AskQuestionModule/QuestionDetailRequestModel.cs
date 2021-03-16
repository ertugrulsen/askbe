namespace AskDefinex.Business.Model
{
    public class QuestionDetailRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Header { get; set; }
        public string Detail { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public int StartOffSet { get; set; }
        public int Count { get; set; }


    }
}
