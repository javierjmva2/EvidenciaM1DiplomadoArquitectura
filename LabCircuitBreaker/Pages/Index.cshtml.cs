using LabCircuitBreaker.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace LabCircuitBreaker.Pages
{
    public class IndexModel : PageModel
    {
        public static string StateCircuitBreaker = "Is Closed";

        private IRandomUserService randomUserService;
        private readonly IServiceProvider _serviceProvider;
        public IndexModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnGet()
        {

        }

        public JsonResult OnGetStatusCircuit()
        {
            return new JsonResult(StateCircuitBreaker);
        }

        public async Task<JsonResult> OnGetRandomUserAsync()
        {
            randomUserService = _serviceProvider.GetService<IRandomUserService>();
            return new JsonResult(await randomUserService.GetRandomUserAsync());
        }
    }
}
