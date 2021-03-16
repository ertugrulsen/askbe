using AskDefinex.Business.Model.AskCommentModule;
using DefineXwork.Library.Business;
using System.Collections.Generic;

namespace AskDefinex.Business.Service.Interface
{
    public interface IAskCommentService : IService
    {
        public int CreateComment(CommentCreateModel commentModel);
        public void UpdateComment(CommentUpdateModel updateModel);
        public void DeleteComment(CommentDeleteModel deleteModel);
        public void DeleteCommentByQuestionId(CommentDeleteModel deleteModel);
        public List<CommentDetailModel> GetCommentsByQuestionId(int questionid);
        public List<CommentDetailModel> GetCommentsByAnswerId(int answerid);
    }
}
