using Motives.WebTest.BackEnd.Interfaces;
using Motives.WebTest.BackEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Services
{
    public class UserService : IUserService
    {
        private AuthenToken _AuthenToken;
        public UserService()
        {
        }

        public async Task<UserResponse> Login(UserLoginModel userLoginModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string para = JsonConvert.SerializeObject(userLoginModel);
                    var content = new StringContent(para, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44327/api/Authens/user/login", content);
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var tokenValid = JsonConvert.DeserializeObject<UserResponse>(result);
                        _AuthenToken = new AuthenToken
                        {
                            Token = tokenValid.AuthenToken.Token,
                            Expiration = tokenValid.AuthenToken.Expiration.AddHours(7)
                        };
                        return tokenValid;
                    }
                    _AuthenToken = null;
                    return null;
                }
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<ResponseModel> Register(UserRegisterModel userRegister)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(userRegister), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44327/api/Authens/user/register", content);
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                    return new ResponseModel
                    {
                        Message = "failed"
                    };
                }
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<ResponseModel> InsertUser(UserRegisterModel userRegister)
        {
            try
            {
                if(_AuthenToken != null)
                {
                    using(var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _AuthenToken.Token);
                        var content = new StringContent(JsonConvert.SerializeObject(userRegister), Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("https://localhost:44327/api/User/insert", content);
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                }
                return new ResponseModel
                {
                    Message = "Unauthorized"
                };
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<TableUser[]> GetAllUsers()
        {
            try
            {
                if(_AuthenToken != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _AuthenToken.Token);
                        var response = await client.GetAsync("https://localhost:44327/api/Users/get");
                        return JsonConvert.DeserializeObject<TableUser[]>(await response.Content.ReadAsStringAsync());
                    }
                }
                return null;
            }
            catch(Exception ex) { throw ex; }
        }

        public async Task<ResponseModel> DeleteUser(int id)
        {
            try
            {
                if(_AuthenToken != null)
                {
                    using(var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _AuthenToken.Token);
                        var response = await client.DeleteAsync($"https://localhost:44327/api/Users/delete/{id}");
                        if(response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                        }
                    }
                }
                return new ResponseModel
                {
                    Message = "Failed"
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<ResponseModel> UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}
