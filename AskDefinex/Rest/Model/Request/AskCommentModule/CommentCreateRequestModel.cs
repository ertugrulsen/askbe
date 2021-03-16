namespace AskDefinex.Rest.Model.Request.AskCommentModule
{
    public class CommentCreateRequestModel
    {
        public int UserId { get; set; }
        public int Question_Answer_Id { get; set; }
        public string Comment { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
    }
}
