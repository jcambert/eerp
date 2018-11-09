using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ePing.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ePing.Controllers
{
    public class ClubQueryParameters
    {
        
        public string Club { get; set; } = string.Empty;
    }

    [Authorize]
    public class HomeController : Controller
    {
      

        public HomeController()
        {
            
        }

        DashboardViewModel CreateVM(string viewingClub="")
        {
            string auth = HttpContext.Session.GetValue<string>("auth");
            var user = JsonConvert.DeserializeObject<UserDto>(auth);

            var token = JsonConvert.DeserializeObject<BearerDto>(auth);

            
            DashboardViewModel vm = new DashboardViewModel() { User = user.User, Token = token.Jwt, ViewingClub=viewingClub };
            return vm;
        }

        public IActionResult Index([FromQuery]ClubQueryParameters parameters)
        {

            return View("Index", CreateVM(parameters.Club));
        }

        

        public IActionResult Licencies([FromQuery]ClubQueryParameters parameters)
        {
            
            return View("Licencies", CreateVM(parameters.Club));
        }

        public IActionResult Equipes([FromQuery]ClubQueryParameters parameters)
        {
            return View("Equipes", CreateVM(parameters.Club));
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
