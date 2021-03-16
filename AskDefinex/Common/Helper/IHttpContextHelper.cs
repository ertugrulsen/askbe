using AskDefinex.Common.Models;

namespace AskDefinex.Common.Utility
{
    public interface IHttpContextHelper
    {
        HttpContextModel GetHttpContext();
    }
}
