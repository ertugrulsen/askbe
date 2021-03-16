using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Model.AskCommentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskDefinex.Rest.Model.Response.AskQuestionModule
{
    public class QuestionDetailResponseModel : CommonResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Header { get; set; }
        public string Detail { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsActive { get; set; }
        public List<AnswerDetailModel> AnswerList { get; set; }
        public List<CommentDetailModel> CommentList { get; set; }
    }
}
