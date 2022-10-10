using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Motives.WebTest.API.ContextServices;
using Motives.WebTest.API.Interfaces;
using Motives.WebTest.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Motives.WebTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthensController : ControllerBase
    {
        private readonly ILogger<AuthensController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserService _iuserService;

        public AuthensController(ILogger<AuthensController> logger, IConfiguration configuration,
            IUserService userService)
        {
            _logger = logger;
            _configuration = configuration;
            _iuserService = userService;
        }

        [HttpPost]
        [Route("api/user/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLogin)
        {
            try
            {
                var user = await _iuserService.UserLogin(userLogin);
                if (user.Status == EStatusModel.Success)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userLogin.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                return Unauthorized(user.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("exception", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/user/register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel userRegister)
        {
            try
            {
                var response = await _iuserService.UserRegister(userRegister);
                if(response.Status == EStatusModel.Success)
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
    }
}
