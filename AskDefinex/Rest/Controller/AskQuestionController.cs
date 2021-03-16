using System;
using AutoMapper;
using DefineXwork.Library.Logging;
using AskDefinex.Business.Model;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Model.Request;
using AskDefinex.Rest.Model.Response;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using AskDefinex.Common.Const;
using AskDefinex.Rest.Model.Response.AskQuestionModule;
using System.Collections.Generic;
using System.Linq;
using AskDefinex.Business.Model.AskQuestionModule;
using AskDefinex.Rest.Model.Request.AskQuestionModule;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nest;

namespace AskDefinex.Rest.Controller
{
    [Route("api/[controller]")]
    public class AskQuestionController : BaseController
    {
        private readonly ILogger<AskQuestionController> _logManager;
        private readonly IAskQuestionService _questionService;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;

        public AskQuestionController(ILogger<AskQuestionController> logManager, IAskQuestionService questionService, IMapper mapper, IElasticClient elasticClient)
        {
            _logManager = logManager;
            _questionService = questionService;
            _mapper = mapper;
            _elasticClient = elasticClient;
        }

        [HttpPost]
        [Route("CreateQuestion")]
        [Authorize]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionCreateRequestModel request)
        {

            _logManager.LogDebug("CreateQuestion service started with request: {@QuestionCreateRequestModel}", request);

            RestResponseContainer<QuestionCreateResponseModel> response = new RestResponseContainer<QuestionCreateResponseModel>();

            QuestionCreateModel model = _mapper.Map<QuestionCreateRequestModel, QuestionCreateModel>(request);   

            BaseResponseModel checkQuestionIsExist = _questionService.CheckIfQuestionExist(request.Header);
            if (checkQuestionIsExist.IsSucceed)
            {
                response.IsSucceed = false;
                response.ErrorCode = checkQuestionIsExist.Code;
                _logManager.LogDebug("CheckIfQuestionService finished with response: {@BaseResponseModel}",checkQuestionIsExist);
                return Ok(response);
            }

            int id = await _questionService.CreateQuestion(model);
            QuestionCreateResponseModel responseModel = new QuestionCreateResponseModel();
            responseModel.Id = id;
            response.IsSucceed = true;
            response.Response = responseModel;

            _logManager.LogDebug("CreateQuestion service finished with response: {@QuestionCreateResponseModel}", responseModel);

            return Ok(response);
        }
        [HttpPost]
        [Route("UpdateQuestion")]
        [Authorize]
        public IActionResult UpdateQuestion([FromBody] QuestionUpdateRequestModel request)
        {
            _logManager.LogDebug("UpdateQuestion service started with request: {@QuestionUpdateRequestModel}", request);

            RestResponseContainer<QuestionUpdateResponseModel> response = new RestResponseContainer<QuestionUpdateResponseModel>();

            QuestionUpdateModel model = _mapper.Map<QuestionUpdateRequestModel, QuestionUpdateModel>(request);
            _questionService.UpdateQuestion(model);
            response.IsSucceed = true;

            _logManager.LogDebug("UpdateQuestion service finished with response: {@QuestionUpdateModel}", model);

            return Ok(response);
        }       
        [HttpPut]
        [Route("DeleteQuestion")]
        [Authorize]
        public IActionResult DeleteQuestion([FromBody] QuestionDeleteRequestModel request)
        {
            _logManager.LogDebug("DeleteQuestion service started with request: {@QuestionDeleteRequestModel}", request);

            RestResponseContainer<QuestionDeleteResponseModel> response = new RestResponseContainer<QuestionDeleteResponseModel>();

            QuestionDeleteModel model = _mapper.Map<QuestionDeleteRequestModel, QuestionDeleteModel>(request);
            _questionService.DeleteQuestion(model);

            response.IsSucceed = true;

            _logManager.LogDebug("DeleteQuestion service finished with response: {@QuestionDeleteModel}", model);

            return Ok(response);
        }
        [HttpGet]
        [Route("GetAllQuestions")]
        [AllowAnonymous]
        public IActionResult GetAllQuestions()
        {
            _logManager.LogDebug("GetAllQuestions service started");

            RestResponseContainer<List<QuestionDetailResponseModel>> response = new RestResponseContainer<List<QuestionDetailResponseModel>>();

            List<QuestionDetailModel> model = _questionService.GetAllQuestions();

            if (model != null)
            {
                response.Response = _mapper.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>>(model);
            }
            response.IsSucceed = true;

            _logManager.LogDebug("GetAllQuestions service finished");

            return Ok(response);
        }
        [HttpGet]
        [Route("GetQuestionById")]
        [AllowAnonymous]
        public IActionResult GetQuestionById([FromQuery] QuestionIdModel request)
        {
            _logManager.LogDebug("GetQuestionById service started with request: {@QuestionIdModel}", request);

            RestResponseContainer<QuestionDetailResponseModel> response = new RestResponseContainer<QuestionDetailResponseModel>();

            QuestionDetailModel model = _questionService.GetQuestionById(request.Id);

            if (model != null)
            {
                response.Response = _mapper.Map<QuestionDetailModel, QuestionDetailResponseModel>(model);
            }
            response.IsSucceed = true;

            _logManager.LogDebug("GetQuestionById service finished with response: {@QuestionDetailModel}", model);

            return Ok(response);
        }

        [HttpPost]
        [Route("QuestionUpdateUpVote")]
        [Authorize]
        public IActionResult QuestionUpdateUpVote([FromBody] QuestionUpdateRequestModel request)
        {
            _logManager.LogDebug("QuestionUpdateUpVote service started with request: {@QuestionUpdateRequestModel}", request);
            RestResponseContainer<QuestionUpdateResponseModel> response = new RestResponseContainer<QuestionUpdateResponseModel>();

            QuestionUpdateModel model = _mapper.Map<QuestionUpdateRequestModel, QuestionUpdateModel>(request);
            _questionService.QuestionUpdateUpVote(model);
            response.IsSucceed = true;

            _logManager.LogDebug("QuestionUpdateUpVote service finished with response: {@QuestionUpdateModel}", model);

            return Ok(model.UpVotes);
        }
        [HttpPost]
        [Route("QuestionUpdateDownVote")]
        [Authorize]
        public IActionResult QuestionUpdateDownVote([FromBody] QuestionUpdateRequestModel request)
        {
            _logManager.LogDebug("QuestionUpdateDownVote service started with request: {@QuestionUpdateRequestModel}", request);

            RestResponseContainer<QuestionUpdateResponseModel> response = new RestResponseContainer<QuestionUpdateResponseModel>();

            QuestionUpdateModel model = _mapper.Map<QuestionUpdateRequestModel, QuestionUpdateModel>(request);
            _questionService.QuestionUpdateDownVote(model);
            response.IsSucceed = true;

            _logManager.LogDebug("QuestionUpdateDownVote service finished with response: {@QuestionUpdateModel}", model);

            return Ok(model.DownVotes);
        }
        [HttpPost]
        [Route("QuestionIsClosed")]
        [Authorize]
        public IActionResult QuestionIsClosedById([FromBody] QuestionUpdateRequestModel request)
        {
            _logManager.LogDebug("QuestionIsClosedById service started with request: {@QuestionUpdateRequestModel}", request);

            RestResponseContainer<QuestionUpdateResponseModel> response = new RestResponseContainer<QuestionUpdateResponseModel>();

            QuestionUpdateModel model = _mapper.Map<QuestionUpdateRequestModel, QuestionUpdateModel>(request);
            _questionService.QuestionIsClosedById(model);
            response.IsSucceed = true;

            _logManager.LogDebug("GetAllQuestionsForMain service finished with response: {@QuestionUpdateModel}", model);

            return Ok(response);
        }
        [HttpGet]
        [Route("GetAllQuestionsForMain")]
        [AllowAnonymous]
        public IActionResult GetAllQuestionsForMain([FromQuery] AllQuestionForMainRequestModel request)
        {
            _logManager.LogDebug("GetAllQuestionsForMain service started with request: {@QuestionDetailModel}", request);
            RestResponseContainer<List<QuestionDetailResponseModel>> response = new RestResponseContainer<List<QuestionDetailResponseModel>>();

            AllQuestionForMainModel requestModel = _mapper.Map<AllQuestionForMainRequestModel, AllQuestionForMainModel>(request);

            List<QuestionDetailModel> model = _questionService.GetAllQuestionsForMain(requestModel);

            if (model != null)
            {
                if (model.Count > 0)
                {
                    int mod = model[0].TotalCount % model[0].Count;
                    int pageCount = model[0].TotalCount / model[0].Count;
                    if (mod > 0)
                    {
                        pageCount = pageCount + 1;
                    }
                    response.TotalCount = model[0].TotalCount;
                    response.TotalPages = pageCount;
                }
                response.Response = _mapper.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>>(model);
            }
            response.IsSucceed = true;
            _logManager.LogDebug("GetAllQuestionsForMain service finished with response: {@QuestionDetailModel}", model);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetQuestionWithAnswers")]
        [AllowAnonymous]
        public IActionResult GetQuestionWithAnswers([FromQuery] QuestionDetailModel request)
        {
            _logManager.LogDebug("GetQuestionWithAnswers service started with request: {@QuestionDetailModel}", request);
            RestResponseContainer<QuestionDetailResponseModel> response = new RestResponseContainer<QuestionDetailResponseModel>();

            QuestionDetailModel model = _questionService.GetQuestionWithAnswers(request);

            if (model != null)
            {
                response.Response = _mapper.Map<QuestionDetailModel, QuestionDetailResponseModel>(model);
            }
            response.IsSucceed = true;
            _logManager.LogDebug("GetUnansweredQuestions service finished with response: {@QuestionDetailModel}", model);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetUnansweredQuestions")]
        [AllowAnonymous]
        public IActionResult GetUnansweredQuestions([FromQuery] QuestionDetailModel request)
        {
            _logManager.LogDebug("GetUnansweredQuestions service started with request: {@QuestionDetailModel}", request);
            RestResponseContainer<List<QuestionDetailResponseModel>> response = new RestResponseContainer<List<QuestionDetailResponseModel>>();

            List<QuestionDetailModel> model = _questionService.GetUnansweredQuestions(request);

            if (model != null)
            {
                if (model.Count > 0)
                {
                    int mod = model[0].TotalCount % model[0].Count;
                    int pageCount = model[0].TotalCount / model[0].Count;
                    if (mod > 0)
                    {
                        pageCount = pageCount + 1;
                    }
                    response.TotalCount = model[0].TotalCount;
                    response.TotalPages = pageCount;
                }
                response.Response = _mapper.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>>(model);
            }
            response.IsSucceed = true;
            _logManager.LogDebug("GetUnansweredQuestions service finished with response: {@QuestionDetailModel}", model);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetMostUpVotedQuestions")]
        [AllowAnonymous]
        public IActionResult GetMostUpVotedQuestions([FromQuery] QuestionDetailModel request)
        {
            _logManager.LogDebug("GetMostUpVotedQuestions service started with request: {@QuestionDetailModel}", request);
            RestResponseContainer<List<QuestionDetailResponseModel>> response = new RestResponseContainer<List<QuestionDetailResponseModel>>();

            List<QuestionDetailModel> model = _questionService.GetMostUpVotedQuestions(request);

            if (model != null)
            {
                if (model.Count > 0)
                {
                    int mod = model[0].TotalCount % model[0].Count;
                    int pageCount = model[0].TotalCount / model[0].Count;
                    if (mod > 0)
                    {
                        pageCount = pageCount + 1;
                    }
                    response.TotalCount = model[0].TotalCount;
                    response.TotalPages = pageCount;
                }
                response.Response = _mapper.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>>(model);
            }
            response.IsSucceed = true;
            _logManager.LogDebug("GetMostUpVotedQuestions service finished with response: {@QuestionDetailModel}", model);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetQuestionsByUserId")]
        [Authorize]
        public IActionResult GetQuestionsByUserId([FromQuery] QuestionDetailModel request)
        {
            _logManager.LogDebug("GetQuestionsByUserId service started with request: {@QuestionDetailModel}", request);
            RestResponseContainer<List<QuestionDetailResponseModel>> response = new RestResponseContainer<List<QuestionDetailResponseModel>>();

            List<QuestionDetailModel> model = _questionService.GetQuestionsByUserId(request);

            if (model != null)
            {
                response.Response = _mapper.Map<List<QuestionDetailModel>, List<QuestionDetailResponseModel>>(model);
            }
            response.IsSucceed = true;
            _logManager.LogDebug("GetQuestionsByUserId service finished with response: {@QuestionDetailModel}", model);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetQuestionCountByUserId")]
        [AllowAnonymous]
        public IActionResult GetQuestionCountByUserId([FromQuery] QuestionListByUserIdRequestModel request)
        {
            _logManager.LogDebug("GetQuestionCountByUserId service started with request: {@QuestionListByUserIdRequestModel}", request);
            RestResponseContainer<QuestionCountResponseModel> response = new RestResponseContainer<QuestionCountResponseModel>();

            QuestionCountModel model = _questionService.GetQuestionCountByUserId(request.UserId);

            if (model != null)
            {
                response.Response = _mapper.Map<QuestionCountModel, QuestionCountResponseModel>(model);            
            }
            response.IsSucceed = true;
            _logManager.LogDebug("GetQuestionCountByUserId service finished with response: {@QuestionCountModel}", model);
            return Ok(response);
        }

        [HttpGet("reindex")]
        public async Task<IActionResult> ReIndex()
        {
            _logManager.LogDebug("Elasticsearch reIndex service started");
            await _elasticClient.DeleteByQueryAsync<QuestionSearchModel>(q => q.MatchAll());

            var allPosts =  _questionService.GetAllQuestions().ToArray();

            foreach (var post in allPosts)
            {
                await _elasticClient.IndexDocumentAsync(post);
            }

            String returnMessage = $"{allPosts.Length} post(s) reindexed";
            _logManager.LogDebug("Elasticsearch reIndex service finished with this message: {returnMessage}",returnMessage);
            return Ok(returnMessage);
        }

        [HttpGet("SearchWithElastic")]
        public async Task<IActionResult> Find(string query, int page = 1, int pageSize = 5)
        {
            RestResponseContainer<QuestionSearchResponseModel> response =
                new RestResponseContainer<QuestionSearchResponseModel>();

            QuestionSearchResponseModel responseModel = new QuestionSearchResponseModel();
            _logManager.LogDebug("Search Service started with this query:{query}",query);
            var elasticResponse = await _elasticClient.SearchAsync<QuestionSearchModel>(
                s => s
                    .From((page - 1) * pageSize)
                    .Size(pageSize)
                    .Query(q => q
                                    .Match(c => c
                                        .Field(p => p.Header)
                                        .Query(query)
                                    )
                                || q.Match(c => c
                                    .Field(p => p.Detail)
                                    .Query(query)
                                )
                    )
            );

            if (!elasticResponse.IsValid)
            {
                // We could handle errors here by checking response.OriginalException or response.ServerError properties
                _logManager.LogError("Elastic client has server error {ServerError}", elasticResponse.ServerError);
                return Ok(elasticResponse.ServerError);
            }

            responseModel.SearchModelList = elasticResponse.Documents.ToList();
            responseModel.TotalCount = elasticResponse.Total;

            response.Response = responseModel;
            _logManager.LogDebug("Search service finished successfully with this request query {query} and response is {@ResponseModel}",query,responseModel);
            return Ok(response);
        }


    }
}
