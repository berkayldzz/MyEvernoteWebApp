using MyEvernoteWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MyEvernoteWebApp.Filter
{
    public class Auth : FilterAttribute , IAuthorizationFilter  
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User == null)  // eğer login yapılmadıysa
            {
                filterContext.Result = new RedirectResult("/Home/Login"); // login sayfasına gönder
            }
        }
    }
}