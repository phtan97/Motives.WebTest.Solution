using Motives.WebTest.API.ContextServices;
using Motives.WebTest.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.API.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> UserLogin(UserLoginModel userLogin);
        Task<ResponseModel> UserRegister(UserRegisterModel userRegister);
        Task<ResponseModel> UserUpdate(UserRegisterModel userUpdate);
        Task<ResponseModel> UserDelete(int userID);
        Task<TableUser[]> GetUsers();
    }
}
