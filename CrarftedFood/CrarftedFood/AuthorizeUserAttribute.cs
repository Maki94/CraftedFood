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
        public int[] Permission { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool p = false;
            Data.DTOs.LoginDto user = UserSession.GetUser();
            foreach (int permission in this.Permission)
            {
                if (user != null && user.Permissions.Contains(permission))
                {
                    p = true;
                }
            }
            return p;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Login",
                                action = "Unauthorized",

                            })
                        );
        }
    }
}
