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
    [Route("api/[controller]")]
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
        [Route("update")]
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
        
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await _iuserService.GetUsers());
            }
            catch(Exception ex)
            {
                _logger.LogError("exception", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertUser([FromBody] UserRegisterModel userRegister)
        {
            try
            {
                var response = await _iuserService.UserRegister(userRegister);
                if (response.Status == EStatusModel.Success)
                {
                    return Ok(response.Message);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("exception", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var response = await _iuserService.UserDelete(id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError("exception", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
