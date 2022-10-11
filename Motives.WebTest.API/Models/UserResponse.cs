using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.API.Models
{
    public class UserResponse
    {
        public ResponseModel ResponseModel { get; set; }
        public AuthenToken AuthenToken { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
    public class AuthenToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
