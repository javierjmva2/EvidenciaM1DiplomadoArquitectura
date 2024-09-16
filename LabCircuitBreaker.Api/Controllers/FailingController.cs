using LabCircuitBreaker.Api.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LabCircuitBreaker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FailingController : ControllerBase
    {
        public static bool IsError = false;
        public FailingController(IServiceProvider serviceProvider)
        {
        }

        [HttpGet(Name = "SetFailing")]
        public string SetFailing(bool isError)
        {
            IsError = isError;
            return "Now is error:" + isError;
        }
    }
}
