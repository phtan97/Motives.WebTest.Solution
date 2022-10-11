using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Motives.WebTest.BackEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.BackEnd.Models
{
    public class AuthenFilter : ActionFilterAttribute
    {
        private IContextService _IContextService = EngineContext.Resolve<IContextService>();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = _IContextService.CurrentUser;
            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                            { "controller", "Home" },
                            { "action", "Index" }
                    });

            }
        }
    }
}
