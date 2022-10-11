using Microsoft.AspNetCore.Mvc;
using Motives.WebTest.BackEnd.Interfaces;
using Motives.WebTest.BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Controllers
{
    public class BaseController : Controller
    {
        protected IContextService _WorkContext = EngineContext.Resolve<IContextService>();

        public TableUser _User 
        {
            get
            {
                return _WorkContext.CurrentUser;
            }
            set
            {
                _WorkContext.CurrentUser = value;
            }
        }
        public void SetSessionUser(TableUser user)
        {
            try
            {
                _WorkContext.CurrentUser = user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
