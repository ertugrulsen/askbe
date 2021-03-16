namespace AskDefinex.Business.Model
{
    public class QuestionUpdateRequestModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Detail { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }

    }
}
