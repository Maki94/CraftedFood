using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace CrarftedFood
{
    public class UserSession
    {
        public static void SetUser(Data.DTOs.LoginDto emp)
        {
            HttpContext.Current.Session["user"] = emp;
        }

        public static Data.DTOs.LoginDto GetUser()
        {
            if(HttpContext.Current.Session["user"] == null)
                throw new Exception("Nije ulogovan");
            return (Data.DTOs.LoginDto) HttpContext.Current.Session["user"];
        }

        public static bool CheckUserID(int id)
        {
            return UserSession.GetUser().EmployeeId == id;
        }

        public static bool IfAdmin()
        {
            return UserSession.GetUser().RoleId == (int) Data.Enums.Roles.Admin;
        }

        public static bool IfClient()
        {
            return UserSession.GetUser().RoleId == (int)Data.Enums.Roles.Client;
        }
    }
}