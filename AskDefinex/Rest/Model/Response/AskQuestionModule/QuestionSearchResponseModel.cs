using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskQuestionModule;

namespace AskDefinex.Rest.Model.Response.AskQuestionModule
{
    public class QuestionSearchResponseModel : BaseResponseModel
    {
        public List<QuestionSearchModel> SearchModelList { get; set; }
        public long TotalCount { get; set; }
    }
}
