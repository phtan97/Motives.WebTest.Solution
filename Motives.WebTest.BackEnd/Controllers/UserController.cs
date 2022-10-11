using Microsoft.AspNetCore.Mvc;
using Motives.WebTest.BackEnd.Interfaces;
using Motives.WebTest.BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Controllers
{
    [AuthenFilter]
    public class UserController : BaseController
    {
        private readonly IUserService _IuserService;
        public UserController(IUserService userService)
        {
            _IuserService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _IuserService.GetAllUsers();
            return View(response);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int idUser)
        {
            try
            {
                var response = await _IuserService.DeleteUser(idUser);
                return Json(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
