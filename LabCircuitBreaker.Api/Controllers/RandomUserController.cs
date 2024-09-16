using LabCircuitBreaker.Api.Services.Interface;
using LabCircuitBreaker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LabCircuitBreaker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomUserController : ControllerBase
    {

        private readonly IRandomUserService randomUserService;
        public RandomUserController(IServiceProvider serviceProvider)
        {
            randomUserService = serviceProvider.GetService<IRandomUserService>();
        }

        [HttpGet("GetRandom")]
        public async Task<IActionResult> GetRandom()
        {
            if (FailingController.IsError)
                return new StatusCodeResult(500);

            return new JsonResult(await randomUserService.GetRandomUserAsync());
        }
    }
}
