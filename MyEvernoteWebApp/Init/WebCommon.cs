using MyEvernote.Common;
using MyEvernote.Entites;
using MyEvernoteWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernoteWebApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            EvernoteUser user = CurrentSession.User;

            if (user != null)
                return user.Username;
            else
                return "system";
        }
    }


}



