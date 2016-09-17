using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CrarftedFood.Tests
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public int Permission { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Data.DTOs.LoginDto user = UserSession.GetUser();

            if (user != null && user.Permissions.Contains(this.Permission))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Login",
                                action = "Index",

                            })
                        );
        }
    }
}
