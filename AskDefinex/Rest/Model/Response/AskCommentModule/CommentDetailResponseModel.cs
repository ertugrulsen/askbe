namespace AskDefinex.Rest.Model.Response.AskCommentModule
{
    public class CommentDetailResponseModel : CommonResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Question_Answer_Id { get; set; }
        public string Comment { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
    }
}
