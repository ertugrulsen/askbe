namespace AskDefinex.Business.Model
{
    public class TagUpdateModel : BaseModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
