using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Model.AskCommentModule;
using System.Collections.Generic;

namespace AskDefinex.Business.Model
{
    public class QuestionDetailModel : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Header { get; set; }
        public string Detail { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public int StartOffSet { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public List<AnswerDetailModel> AnswerList { get; set; }
        public List<CommentDetailModel> CommentList { get; set; }
    }
}
