namespace AskDefinex.Rest.Model.Request.AskCommentModule
{
    public class CommentUpdateRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Question_Answer_Id { get; set; }
        public string Comment { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
    }
}
