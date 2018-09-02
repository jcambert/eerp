using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.HealthChecks;
using System.Diagnostics;
using System.Threading.Tasks;
using WebSupervisor.Models;

namespace WebSupervisor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHealthCheckService _healthCheckSvc;
        public HomeController(IHealthCheckService checkSvc)
        {
            _healthCheckSvc = checkSvc;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _healthCheckSvc.CheckHealthAsync();

            var data = new HealthStatusViewModel(result.CheckStatus);

            foreach (var checkResult in result.Results)
            {
                data.AddResult(checkResult.Key, checkResult.Value);
            }

            ViewBag.RefreshSeconds = 60;

            return View(data);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
