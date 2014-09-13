using FinancialCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinancialCenter.Utils
{
    public class AuthenticationFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            Account account = filterContext.HttpContext.Session["account"] as Account;
            if (account == null && filterContext.ActionDescriptor.ActionName.ToUpper() != "LOGIN")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"controller" , "Account"},
                        {"action" , "Login"}
                    }
                    );
            }
        }
    }
}