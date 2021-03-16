namespace AskDefinex.DataAccess.Model.Data
{
    public class AskCommentDAOModel : BaseDAOModel
    {
        public int Id { get; set; }
        public int Question_Answer_Id { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
    }
}
