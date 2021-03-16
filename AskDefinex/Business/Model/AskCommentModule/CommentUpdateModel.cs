namespace AskDefinex.Business.Model.AskCommentModule
{
    public class CommentUpdateModel : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Question_Answer_Id { get; set; }
        public string Comment { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
    }
}
