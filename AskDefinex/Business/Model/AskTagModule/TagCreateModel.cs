namespace AskDefinex.Business.Model
{
    public class TagCreateModel : BaseModel
    {
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }

    }
}
