using AskDefinex.Business.Model;
using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Model.AskBadgeModule;
using AskDefinex.Business.Model.AskCommentModule;
using AskDefinex.Business.Model.AskQuestionModule;
using AskDefinex.Business.Model.AskUserModule;
using AskDefinex.Rest.Model.Request;
using AskDefinex.Rest.Model.Request.AskAnswerModule;
using AskDefinex.Rest.Model.Request.AskBadgeModule;
using AskDefinex.Rest.Model.Request.AskCommentModule;
using AskDefinex.Rest.Model.Request.AskQuestionModule;
using AskDefinex.Rest.Model.Request.AskUserModule;
using AskDefinex.Rest.Model.Response;
using AskDefinex.Rest.Model.Response.AskAnswerModule;
using AskDefinex.Rest.Model.Response.AskBadgeModule;
using AskDefinex.Rest.Model.Response.AskCommentModule;
using AskDefinex.Rest.Model.Response.AskQuestionModule;
using AutoMapper;
using System.Collections.Generic;

namespace AskDefinex.Rest.Common.Mapper
{
    public class RestMapper : Profile
    {
        public RestMapper()
        {
            //askAuthentication
            CreateMap<AskLoginModel, AskLoginResponseModel>();
            CreateMap<AskRegistrationRequestModel, AskRegistrationModel>();
            //askUser
            CreateMap<AskUserDetailModel, AskUserDetailResponseModel>();
            CreateMap<AskUserCreateRequestModel, AskUserCreateModel>();



            //askBadge
            CreateMap<BadgeDetailRequestModel, BadgeDetailModel>();
            CreateMap<BadgeDetailModel, BadgeDetailResponseModel>();
            CreateMap<BadgeCreateRequestModel, BadgeCreateModel>();
            CreateMap<BadgeCreateModel, BadgeCreateResponseModel>();
            CreateMap<BadgeUpdateRequestModel, BadgeUpdateModel>();
            CreateMap<BadgeUpdateModel, BadgeUpdateResponseModel>();
            CreateMap<BadgeDeleteRequestModel, BadgeDeleteModel>();
            CreateMap<BadgeDeleteModel, BadgeDeleteResponseModel>();
            //AskQuestion
            CreateMap<QuestionDetailModel, QuestionDetailResponseModel>();
            CreateMap<QuestionCreateRequestModel, QuestionCreateModel>();
            CreateMap<QuestionCreateModel, QuestionCreateResponseModel>();
            CreateMap<QuestionUpdateRequestModel, QuestionUpdateModel>();
            CreateMap<QuestionUpdateModel, QuestionUpdateResponseModel>();
            CreateMap<QuestionDeleteRequestModel, QuestionDeleteModel>();
            CreateMap<QuestionDeleteModel, QuestionDeleteResponseModel>();
            CreateMap<QuestionDetailRequestModel, QuestionDetailModel>();
            CreateMap<QuestionDetailModel, List<QuestionDetailResponseModel>>();
            CreateMap<QuestionDetailModel, QuestionDetailResponseModel>();
            CreateMap<QuestionCountModel, QuestionCountResponseModel>();
            CreateMap<AllQuestionForMainRequestModel, AllQuestionForMainModel>();

            //AskTag
            CreateMap<TagCreateRequestModel, TagCreateModel>();
            CreateMap<TagCreateModel, TagCreateResponseModel>();
            CreateMap<TagUpdateRequestModel, TagUpdateModel>();
            CreateMap<TagDeleteRequestModel, TagUpdateModel>();

            //AskAnswer
            CreateMap<AnswerCreateRequestModel, AnswerCreateModel>();
            CreateMap<AnswerCreateModel, AnswerCreateResponseModel>();
            CreateMap<AnswerUpdateRequestModel, AnswerUpdateModel>();
            CreateMap<AnswerUpdateModel, AnswerUpdateResponseModel>();
            CreateMap<AnswerDeleteRequestModel, AnswerDeleteModel>();
            CreateMap<AnswerDeleteModel, AnswerDeleteResponseModel>();
            CreateMap<AnswerDetailRequestModel, AnswerDetailModel>();
            CreateMap<AnswerDetailModel, AnswerDetailResponseModel>();
            //AskComment
            CreateMap<CommentCreateRequestModel, CommentCreateModel>();
            CreateMap<CommentCreateModel, CommentCreateResponseModel>();
            CreateMap<CommentUpdateRequestModel, CommentUpdateModel>();
            CreateMap<CommentUpdateModel, CommentUpdateResponseModel>();
            CreateMap<CommentDeleteRequestModel, CommentDeleteModel>();
            CreateMap<CommentDeleteModel, CommentDeleteResponseModel>();
            CreateMap<CommentDetailModel, List<CommentDetailResponseModel>>();


            CreateMap(typeof(ResponseContainerBusinessModel<>), typeof(RestResponseContainer<>));
        }
    }
}
