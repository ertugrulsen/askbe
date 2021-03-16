namespace AskDefinex.Business.Model
{
    public class QuestionCreateRequestModel
    {
        public string Header { get; set; }
        public string Detail { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }

    }
}
