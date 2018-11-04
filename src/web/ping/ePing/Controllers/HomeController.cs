﻿using System;
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

namespace ePing.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
      

        public HomeController()
        {
            
        }

        DashboardViewModel CreateVM()
        {
            string auth = HttpContext.Session.GetValue<string>("auth");
            var user = JsonConvert.DeserializeObject<UserDto>(auth);

            var token = JsonConvert.DeserializeObject<BearerDto>(auth);


            DashboardViewModel vm = new DashboardViewModel() { User = user.User, Token = token.Jwt };
            return vm;
        }

        public IActionResult Index()
        {

            return View("Index", CreateVM());
        }

        

        public IActionResult Licencies()
        {
            
            return View("Licencies", CreateVM());
        }

        public IActionResult Equipes()
        {
            return View("Equipes", CreateVM());
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
