namespace AskDefinex.Business.Service.Interface
{
    public interface IRecaptchaValidatorService
    {
        bool IsRecaptchaValid(string token);
    }
}
