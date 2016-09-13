using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace CrarftedFood
{
    public class UserSession
    {
        public static void SetUser(Employee emp)
        {
            HttpContext.Current.Session["user"] = emp;
        }

        public static Employee GetUser()
        {
            if(HttpContext.Current.Session["user"] == null)
                throw new Exception("Nije ulogovan");
            return (Employee) HttpContext.Current.Session["user"];
        }
    }
}