using AutoMapper;
using DefineXwork.Library.Security.Common;
using AskDefinex.Business.Model;
using AskDefinex.DataAccess.Model.Data;
using AskDefinex.Business.Model.AskUserModule;
using AskDefinex.Business.Model.AskAnswerModule;
using AskDefinex.Business.Model.AskCommentModule;
using AskDefinex.Business.Model.AskBadgeModule;
using AskDefinex.Business.Model.AskQuestionModule;

namespace AskDefinex.Business.Common
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<UserContextUserDAOModel, UserContextModel>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
            CreateMap<UserContextUserDAOModel, UserContextUserDetailModel>();
            CreateMap<AskLoginDAOModel, AskLoginModel>();

            //askBadge
            CreateMap<AskBadgeDAOModel, BadgeDetailModel>();
            CreateMap<BadgeCreateModel, AskBadgeDAOModel>();
            CreateMap<BadgeUpdateModel, AskBadgeDAOModel>();
            CreateMap<BadgeDeleteModel, AskBadgeDAOModel>();
            //AskQuestion
            CreateMap<AskQuestionDAOModel, QuestionDetailModel>();
            CreateMap<QuestionCreateModel, AskQuestionDAOModel>();
            CreateMap<QuestionUpdateModel, AskQuestionDAOModel>();
            CreateMap<QuestionDeleteModel, AskQuestionDAOModel>();
            CreateMap<QuestionCreateModel, QuestionSearchModel>();
            CreateMap<QuestionUpdateModel, QuestionSearchModel>();
            CreateMap<QuestionDetailModel, QuestionSearchModel>();
            //AskAnswer
            CreateMap<AnswerCreateModel, AskAnswerDAOModel>();
            CreateMap<AnswerUpdateModel, AskAnswerDAOModel>();
            CreateMap<AnswerDeleteModel, AskAnswerDAOModel>();
            CreateMap<AskAnswerDAOModel, AnswerDetailModel>();
            //AskComment
            CreateMap<CommentCreateModel, AskCommentDAOModel>();
            CreateMap<CommentUpdateModel, AskCommentDAOModel>();
            CreateMap<CommentDeleteModel, AskCommentDAOModel>();
            CreateMap<AskCommentDAOModel, CommentDetailModel>();
            //askUser
            CreateMap<AskUserCreateModel, AskUserDAOModel>();
            CreateMap<AskUserDAOModel, AskUserDetailModel>();
            //askTag
            CreateMap<TagCreateModel, AskTagDAOModel>();
            CreateMap<TagUpdateModel, AskTagDAOModel>();




        }
    }
}