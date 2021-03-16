using AskDefinex.Business.Service.Interface;
using AskDefinex.Rest.Model.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace AskDefinex.Business.Service
{
    public class RecaptchaValidatorService : IRecaptchaValidatorService
    {
        private readonly ILogger<RecaptchaValidatorService> _logManager;
        public readonly IConfiguration Configuration;

        public RecaptchaValidatorService(ILogger<RecaptchaValidatorService> logManager, IConfiguration configuration)
        {
            _logManager = logManager;
            Configuration = configuration;
        }
        public bool IsRecaptchaValid(string token)
        {
            try
            {
                using var client = new HttpClient();
                var response = client.GetStringAsync($@"{Configuration["Google:GoogleVerifyUrl"]}?secret={Configuration["Google:RecaptchaV3SecretKey"]}&response={token}").Result;
                var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(response);
                var score = Convert.ToDecimal(Configuration["Google:RecaptchaMinScore"]);
                decimal genScore = recaptchaResponse.Score;
                if (!recaptchaResponse.Success || genScore<score)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _logManager.LogError("Exception is occurred at Recaptcha Validator exception message: {Exception}, exception: {Exception}", e.Message, e);
                throw;
            }
            return true;
        }
    }
}
