using Microsoft.AspNetCore.Http;
using Motives.WebTest.BackEnd.Interfaces;
using Motives.WebTest.BackEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Services
{
    public class ContextService : IContextService
    {
        private const string UserSession = "Session.User";
        private const string UserCookie = "Cookie.User";
        private readonly IHttpContextAccessor _IhttpContextAccessor;

        public ContextService(IHttpContextAccessor httpContextAccessor)
        {
            _IhttpContextAccessor = httpContextAccessor;
        }
        public void SetCookie(string key, string value)
        {
            var option = new CookieOptions
            {
                Expires = string.IsNullOrEmpty(value) ? DateTime.Now.AddMonths(-1) : DateTime.Now.AddHours(24 * 365)
            };
            _IhttpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            _IhttpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }
        public string GetCookie(string key)
        {
            return _IhttpContextAccessor.HttpContext.Request.Cookies[key];
        }

        public void RemoveCookie(string Name)
        {
            if (_IhttpContextAccessor.HttpContext == null) return;
            SetCookie(Name, string.Empty);
        }
        public TableUser CurrentUser
        {
            get => _IhttpContextAccessor.HttpContext.Session.GetObject<TableUser>(UserSession);
            set
            {
                if (value != null)
                {
                    _IhttpContextAccessor.HttpContext.Session.SetObject(UserSession, value);
                }
                else
                {
                    _IhttpContextAccessor.HttpContext.Session.Remove(UserCookie);
                    RemoveCookie(UserCookie);
                    RemoveCookie(UserCookie);
                }
            }
        }
    }
}
