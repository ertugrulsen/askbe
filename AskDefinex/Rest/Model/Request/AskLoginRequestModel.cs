namespace AskDefinex.Rest.Model.Request
{
    public class AskLoginRequestModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RecaptchaToken { get; set; }
    }
}
