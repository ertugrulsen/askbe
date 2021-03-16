using AskDefinex.Rest.Filter;
using Microsoft.AspNetCore.Mvc;

namespace AskDefinex.Rest.Controller
{

    [ApiController]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public abstract class BaseController : ControllerBase
    {
    }
}
