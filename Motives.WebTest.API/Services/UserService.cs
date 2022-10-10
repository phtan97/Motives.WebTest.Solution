using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Motives.WebTest.API.ContextServices;
using Motives.WebTest.API.Interfaces;
using Motives.WebTest.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.API.Services
{
    public class UserService : IUserService
    {
        public Task<ResponseModel> UserDelete(int userID)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    if (!db.TableUsers.Any(x => x.Id == userID))
                    {
                        return Task.FromResult(new ResponseModel
                        {
                            Status = EStatusModel.Failed,
                            Message = "User is not existed in system"
                        });
                    }
                    var user = db.TableUsers.FirstOrDefault(x => x.Id == userID);
                    db.TableUsers.Remove(user);
                    return db.SaveChanges() > 0 ? Task.FromResult(new ResponseModel
                    {
                        Status = EStatusModel.Success,
                        Message = "Remove user success"
                    }) : Task.FromResult(new ResponseModel
                    {
                        Status = EStatusModel.Failed,
                        Message = "Remove user failed"
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<ResponseModel> UserLogin(UserLoginModel userLogin)
        {
            try
            {
                byte[] salt = new byte[128 / 8];
                using (var db = new DatabaseContext())
                {
                    var user = db.TableUsers.FirstOrDefault(x => x.Username.Equals(userLogin.Username));
                    if (user == null) return Task.FromResult(new ResponseModel()
                    {
                        Status = EStatusModel.Failed,
                        Message = "Account is not existed in system"
                    });
                    var password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                    password: userLogin.Password,
                                    salt: salt,
                                    prf: KeyDerivationPrf.HMACSHA256,
                                    iterationCount: 100000,
                                    numBytesRequested: 256 / 8));
                    if (!password.Equals(user.Password)) return Task.FromResult(new ResponseModel
                    {
                        Status = EStatusModel.Failed,
                        Message = "Password is incorrect"
                    });
                    return Task.FromResult(new ResponseModel
                    {
                        Status = EStatusModel.Success,
                        Message = "Log in success"
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<ResponseModel> UserRegister(UserRegisterModel userRegister)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    if (db.TableUsers.Any(x => x.Username.Equals(userRegister.Username)))
                    {
                        return Task.FromResult(new ResponseModel
                        {
                            Status = EStatusModel.Failed,
                            Message = "Account was existed in system"
                        });
                    }
                    byte[] salt = new byte[128 / 8];
                    var password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                        password: userRegister.Password,
                                        salt: salt,
                                        prf: KeyDerivationPrf.HMACSHA256,
                                        iterationCount: 100000,
                                        numBytesRequested: 256 / 8));
                    db.TableUsers.Add(new TableUser
                    {
                        Username = userRegister.Username,
                        Password = password,
                        Email = userRegister.Email,
                        Name = userRegister.Name,
                        SubName = userRegister.SubName,
                        Phone = userRegister.PhoneNumber
                    });
                    return db.SaveChanges() > 0 ? Task.FromResult(new ResponseModel
                    {
                        Status = EStatusModel.Success,
                        Message = "Register success"
                    }) : Task.FromResult(new ResponseModel
                    {
                        Status = EStatusModel.Failed,
                        Message = "Register failed"
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<ResponseModel> UserUpdate(UserRegisterModel userUpdate)
        {
            try
            {
                using(var db = new DatabaseContext())
                {
                    if(!db.TableUsers.Any(x => x.Username.Equals(userUpdate.Username)))
                    {
                        return Task.FromResult(new ResponseModel
                        {
                            Status = EStatusModel.Failed,
                            Message = "User is not existed in system"
                        });
                    }
                    byte[] salt = new byte[128 / 8];
                    userUpdate.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                        password: userUpdate.Password,
                                        salt: salt,
                                        prf: KeyDerivationPrf.HMACSHA256,
                                        iterationCount: 100000,
                                        numBytesRequested: 256 / 8));
                    var user = db.TableUsers.FirstOrDefault(x => x.Username.Equals(userUpdate.Username));
                    user.Password = userUpdate.Password;
                    user.Name = userUpdate.Name;
                    user.SubName = userUpdate.SubName;
                    user.Phone = userUpdate.PhoneNumber;
                    user.Email = userUpdate.Email;
                    return db.SaveChanges() > 0 ? Task.FromResult(new ResponseModel
                    {
                        Status = EStatusModel.Success,
                        Message = "Update user success"
                    }) : Task.FromResult(new ResponseModel
                    {
                        Status = EStatusModel.Failed,
                        Message = "Update user failed"
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
