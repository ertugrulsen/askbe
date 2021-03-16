
using AskDefinex.Rest.Model.Response;
using DefineXwork.Library.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.ServiceModel.Security;
using Microsoft.Extensions.Logging;

namespace AskDefinex.Rest.Filter
{
    public class CustomExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger _logManager;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logManager)
        {
            _logManager = logManager;
        }
        public override void OnException(ExceptionContext context)
        {

            ControllerActionDescriptor controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            int statusCode;

           
            if (context.Exception is ArgumentNullException) statusCode = (int)HttpStatusCode.BadRequest;
            else if (context.Exception is ArgumentException) statusCode = (int)HttpStatusCode.BadRequest;
            else if (context.Exception is UnauthorizedAccessException) statusCode = (int)HttpStatusCode.Unauthorized;
            else if (context.Exception is SecurityAccessDeniedException) statusCode = (int)HttpStatusCode.Forbidden;
            else // özel hatalarda
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }



            string methodDescriptor = $"{controllerActionDescriptor?.MethodInfo.ReflectedType?.Namespace}.{controllerActionDescriptor?.MethodInfo.ReflectedType?.Name}.{controllerActionDescriptor?.MethodInfo.Name}";

            RestResponseContainer<object> responseModel = new RestResponseContainer<object>
            {
                Response = null,
                IsSucceed = false,
                ErrorCode = statusCode.ToString(),
                ErrorMessage = context.Exception.Message.ToString()
            };

            ObjectResult result = new ObjectResult(responseModel)
            {
                StatusCode = statusCode
            };

            context.Result = result;

            _logManager.LogError($"Exception Message : {context.Exception.Message} || methodDescriptor : {methodDescriptor} {context.Exception.StackTrace}");

        }
    }
}
