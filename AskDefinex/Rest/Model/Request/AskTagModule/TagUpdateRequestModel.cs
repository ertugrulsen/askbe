namespace AskDefinex.Business.Model
{
    public class TagUpdateRequestModel
    {
        public string Id { get; set; }
        public string QuestionId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}
