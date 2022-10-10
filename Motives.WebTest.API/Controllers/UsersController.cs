using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Motives.WebTest.API.Interfaces;
using Motives.WebTest.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Motives.WebTest.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _iuserService;

        public UsersController(ILogger<UsersController> logger, IUserService userService) 
        {
            _logger = logger;
            _iuserService = userService;
        }

        [HttpPut]
        [Route("api/users/update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserRegisterModel userUpdate)
        {
            try
            {
                var response = await _iuserService.UserUpdate(userUpdate);
                if(response.Status == EStatusModel.Success)
                {
                    return Ok(response.Message);
                }
                return BadRequest(response.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError("exception", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
