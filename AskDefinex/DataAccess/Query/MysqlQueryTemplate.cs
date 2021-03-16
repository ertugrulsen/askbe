using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;

namespace AskDefinex.DataAccess.Query
{
    public class MysqlQueryTemplate : IQueryTemplate
    {
        readonly Dictionary<string, string> _queries = new Dictionary<string, string>();
        public MysqlQueryTemplate()
        {
            //AskUser
            _queries.Add("AskUserDAO.CreateUser", @"Insert Into ask_user(Id,Name,Surname,UserName,Email,Password,IsActive,CreateDate,CreateUser,LastUpdateDate,LastUpdateUser) Values (@Id,@Name,@Surname,@UserName,@Email,@Password,@IsActive,@CreateDate,@CreateUser,@LastUpdateDate,@LastUpdateUser); Select LAST_INSERT_ID();");
            _queries.Add("AskUserDAO.GetUserWithUserNameAndEmail", @"Select * from askdefinex.ask_user where Username=@UserName or Email=@Email and IsActive=1");
            _queries.Add("AskUserDAO.GetUser", @"Select * from ask_user where Username=@UserName and IsActive=1");
            _queries.Add("AskUserDAO.GetUserById", @"Select * from ask_user where Id=@Id and IsActive=1");
            _queries.Add("AskUserDAO.GetUserByEmail", @"Select * from ask_user where Email=@Email and IsActive=1");
            _queries.Add("AskUserDAO.DeleteUserById", @"Update ask_user set isactive=0 where id=@Id");

            //AskAuthentication
            _queries.Add("AskAuthenticationDAO.GetLoginUserByRefreshToken", @"Select id,username,refreshtoken,refreshtokencreatedate,isactive from ask_user where refreshtoken=@RefreshToken and username=@UserName and IsActive=1");
            _queries.Add("AskAuthenticationDAO.SetRefreshToken", @"Update ask_user set refreshtoken=@refreshtoken,refreshtokencreatedate=@refreshtokencreatedate where username=@UserName");
            _queries.Add("AskAuthenticationDAO.UpdateRefreshToken", @"Update ask_user set refreshtoken=@refreshtoken where username=@UserName");
            _queries.Add("AskAuthenticationDAO.RemoveRefreshToken", @"Update ask_user set refreshtoken='' where username=@UserName");
            _queries.Add("AskAuthenticationDAO.GetAskLoginUser", @"Select id,username,name,email,surname,refreshtoken,refreshtokencreatedate,isactive from ask_user where (email=@Email or username=@Username) and password=@Password and IsActive=1");
            //AskBadge
            _queries.Add("AskBadgeDAO.GetBadgeByUserId", @"Select * from ask_badge where userid=@UserId");
            _queries.Add("AskBadgeDAO.CreateBadge", @"Insert Into ask_badge(userid,name,type,isactive,createdate,createuser) Values (@UserId,@Name,@Type,@IsActive,@CreateDate,@CreateUser); Select LAST_INSERT_ID();");
            _queries.Add("AskBadgeDAO.UpdateBadge", @"Update ask_badge set userid=@UserId, name=@Name, type=@Type, isactive=@IsActive, lastUpdateDate=@LastUpdateDate, lastUpdateUser=@LastUpdateUser where id=@Id");
            _queries.Add("AskBadgeDAO.DeleteBadge", @"Update ask_badge set isactive=@IsActive where id=@Id");
            //AskQUestion
            _queries.Add("AskQuestionDAO.CreateQuestion", @"Insert Into ask_question(userid,header,detail,upvotes,downvotes,isaccepted,isactive,isclose,createdate,createuser) Values (@UserId,@Header,@Detail,@UpVotes,@DownVotes,@IsAccepted,@IsActive,@IsClose,@CreateDate,@CreateUser); Select LAST_INSERT_ID();");
            _queries.Add("AskQuestionDAO.GetQuestionByHeader", @"Select * from ask_question where header=@Header and isActive=1");
            _queries.Add("AskQuestionDAO.UpdateQuestion", @"Update ask_question set userid=@UserId, header=@Header, detail=@Detail, isaccepted=@IsAccepted, isactive=@IsActive, lastUpdateDate=@LastUpdateDate, lastUpdateUser=@LastUpdateUser where id=@Id");
            _queries.Add("AskQuestionDAO.DeleteQuestion", @"Update ask_question set isactive=@IsActive where id=@Id");
            _queries.Add("AskQuestionDAO.GetAllQuestions", @"Select * from ask_question where isactive=true");
            _queries.Add("AskQuestionDAO.GetAllQuestionsCount", @"Select Count(*) from ask_question where isactive=true");
            _queries.Add("AskQuestionDAO.GetQuestionById", @"Select * from ask_question where id=@Id");
            _queries.Add("AskQuestionDAO.QuestionUpdateUpVote", @"Update ask_question set upvotes=@UpVotes where id=@Id");
            _queries.Add("AskQuestionDAO.QuestionUpdateDownVote", @"Update ask_question set downvotes=@DownVotes where id=@Id");
            _queries.Add("AskQuestionDAO.QuestionIsClosedById", @"Update ask_question set isclose=true where id=@Id");
            _queries.Add("AskQuestionDAO.GetAllQuestionsForMain", @"Select * from ask_question where isactive=true Order By CreateDate Desc limit @Limit offset @Offset");
            _queries.Add("AskQuestionDAO.GetUnansweredQuestions", @"Select * from askdefinex.ask_question Where Id NOT IN(Select QuestionId From askdefinex.ask_answer) limit @Limit offset @Offset");
            _queries.Add("AskQuestionDAO.GetUnansweredQuestionsCount", @"Select Count(*) from askdefinex.ask_question Where Id NOT IN(Select QuestionId From askdefinex.ask_answer)");
            _queries.Add("AskQuestionDAO.GetMostUpVotedQuestions", @"Select * from ask_question where isactive=true Order By upvotes Desc limit @Limit offset @Offset");
            _queries.Add("AskQuestionDAO.GetMostUpVotedQuestionsCount", @"Select Count(*) from ask_question where isactive=true Order By upvotes Desc");
            _queries.Add("AskQuestionDAO.GetQuestionsByUserId", @"Select * from ask_question where userid=@UserId and isactive=true Order By CreateDate Desc limit @Limit offset @Offset");
            _queries.Add("AskQuestionDAO.GetQuestionCountByUserId", @"Select count(*) from ask_question where userid=@UserId and isactive=true");

            //AskAnswer
            _queries.Add("AskAnswerDAO.CreateAnswer", @"Insert Into ask_answer(userid,questionid,answer,upvotes,downvotes,isaccepted,isactive,createdate,createuser) Values (@UserId,@QuestionId,@Answer,@UpVotes,@DownVotes,@IsAccepted,@IsActive,@CreateDate,@CreateUser); Select LAST_INSERT_ID();");
            _queries.Add("AskAnswerDAO.UpdateAnswer", @"Update ask_answer set userid=@UserId, questionid=@QuestionId, answer=@Answer, isaccepted=@IsAccepted, isactive=@IsActive, lastUpdateDate=@LastUpdateDate, lastUpdateUser=@LastUpdateUser where id=@Id");
            _queries.Add("AskAnswerDAO.DeleteAnswer", @"Update ask_answer set isactive=@IsActive where id=@Id");
            _queries.Add("AskAnswerDAO.AnswerUpdateUpVote", @"Update ask_answer set upvotes=@UpVotes where id=@Id");
            _queries.Add("AskAnswerDAO.AnswerUpdateDownVote", @"Update ask_answer set downvotes=@DownVotes where id=@Id");
            _queries.Add("AskAnswerDAO.GetAnswerById", @"Select * from ask_answer where id=@Id");
            _queries.Add("AskAnswerDAO.GetAnswersByQuestionId", @"Select * from ask_answer where questionid=@QuestionId  and isactive=true");
            //AskComment
            _queries.Add("AskCommentDAO.CreateComment", @"Insert Into ask_comment(userid,question_answer_id,comment,type,isactive,createdate,createuser) Values (@UserId,@Question_Answer_Id,@Comment,@Type,@IsActive,@CreateDate,@CreateUser); Select LAST_INSERT_ID();");
            _queries.Add("AskCommentDAO.UpdateComment", @"Update ask_comment set userid=@UserId, question_answer_id=@Question_Answer_Id,comment=@Comment, type=@Type, isactive=@IsActive, lastUpdateDate=@LastUpdateDate, lastUpdateUser=@LastUpdateUser where id=@Id");
            _queries.Add("AskCommentDAO.DeleteComment", @"Update ask_comment set isactive=@IsActive where id=@Id");
            _queries.Add("AskCommentDAO.DeleteCommentByQuestionId", @"Update ask_comment set isactive=@IsActive where question_answer_id=@Question_Answer_Id");
            _queries.Add("AskCommentDAO.GetCommentsByQuestionId", @"Select * from ask_comment where question_answer_id=@QuestionId And Type=@Type  and isactive=true");
            _queries.Add("AskCommentDAO.GetCommentsByAnswerId", @"Select * from ask_comment where question_answer_id=@AnswerId And Type=@Type  and isactive=true");

             //AskTag
            _queries.Add("AskTagDAO.CreateTag", @"Insert Into ask_tag(questionid,name,type,isactive,createdate,createuser) Values (@QuestionId,@Name,@Type,@IsActive,@CreateDate,@CreateUser); Select LAST_INSERT_ID();");
            _queries.Add("AskTagDAO.UpdateTag", @"Update ask_tag set questionid=@QuestionId, name=@Name, type=@Type where Id=@Id");
            _queries.Add("AskTagDAO.DeleteTag", @"Update ask_tag set isactive=0 where Id=@Id");

            //GetUser
            _queries.Add("UserContextDAO.GetUser", @"Select * from ask_user where username=@UserName and IsActive=1");
            _queries.Add("UserContextDAO.GetUserRoles", @"Select roleid,name as role from userrole ur inner join role r on r.id=ur.roleid where userid=@UserId");

            //Authentication
            _queries.Add("AuthenticationDAO.SetRefreshToken", @"Update ask_user set refreshtoken=@refreshtoken,refreshtokencreatedate=@refreshtokencreatedate where username=@UserName");
            _queries.Add("AuthenticationDAO.UpdateRefreshToken", @"Update ask_user set refreshtoken=@refreshtoken where username=@UserName");
            _queries.Add("AuthenticationDAO.RemoveRefreshToken", @"Update ask_user set refreshtoken='' where username=@UserName");
            _queries.Add("AuthenticationDAO.GetLoginUserByRefreshToken", @"Select id,username,refreshtoken,refreshtokencreatedate,isactive from ask_user where refreshtoken=@RefreshToken and username=@UserName and IsActive=1 and IsDeleted=0");

        }
        public string GetQuery(string key)
        {
            if (!_queries.TryGetValue(key, out string value))
                throw new ArgumentNullException(key);
            return value;
        }
    }
}
