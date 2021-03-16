namespace AskDefinex.DataAccess.Model.Data
{
    public class AskTagDAOModel : BaseDAOModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
