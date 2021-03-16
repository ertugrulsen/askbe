namespace AskDefinex.Business.Model
{
    public class TagCreateRequestModel
    {
        public string QuestionId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }

    }
}
