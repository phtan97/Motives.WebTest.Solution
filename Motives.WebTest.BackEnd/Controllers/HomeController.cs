using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Motives.WebTest.BackEnd.Interfaces;
using Motives.WebTest.BackEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _IuserService;
        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _IuserService = userService;
        }

        public ActionResult Index()
        {
            if(_User != null)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string userName, string passWord)
        {
            var response = await _IuserService.Login(new UserLoginModel { Username = userName, Password = passWord });
            if(response != null)
            {
                SetSessionUser(new TableUser
                {
                    Username = response.UserName,
                    Name = response.Name,
                    SubName = response.SubName,
                    Phone = response.Phone,
                    Email = response.Email
                });
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> Register(string Username, string Password, string Name, string Phone, string Email, string SubName)
        {
            var response = await _IuserService.Register(new UserRegisterModel { Username = Username, Password = Password,
            Email = Email, Name = Name, SubName = SubName, PhoneNumber = Phone});
            return Json(response);
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
