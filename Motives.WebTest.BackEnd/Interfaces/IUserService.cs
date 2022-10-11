using Motives.WebTest.BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> Login(UserLoginModel userLoginModel);
        Task<ResponseModel> Register(UserRegisterModel userRegister);
        Task<TableUser[]> GetAllUsers();
        Task<ResponseModel> DeleteUser(int id);
        Task<ResponseModel> UpdateUser();
    }
}
