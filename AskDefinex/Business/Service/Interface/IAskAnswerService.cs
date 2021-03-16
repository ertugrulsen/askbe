using AskDefinex.Business.Model.AskAnswerModule;
using DefineXwork.Library.Business;
using System.Collections.Generic;

namespace AskDefinex.Business.Service.Interface
{
    public interface IAskAnswerService : IService
    {
        public int CreateAnswer(AnswerCreateModel answerModel);
        public void UpdateAnswer(AnswerUpdateModel updateModel);
        public void DeleteAnswer(AnswerDeleteModel deleteModel);
        public int AnswerUpdateUpVote(AnswerUpdateModel updateModel);
        public int AnswerUpdateDownVote(AnswerUpdateModel updateModel);
        public AnswerDetailModel GetAnswerById(int id);
        public List<AnswerDetailModel> GetAnswersByQuestionId(int questionId);
    }
}
