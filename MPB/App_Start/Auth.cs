using MPB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPB.App_Start
{
    public abstract class Auth : Controller
    {
        public User loggedUser;
        // GET: Auth
        public bool checkLogged()
        {
            if (Session["UserID"] != null)
            {
                int id = Int16.Parse(Session["UserID"].ToString());
                loggedUser = MPB.Models.User.findById(id);
                ViewBag.LoggedUser = loggedUser;
                return true;
            }
            return false;
        }

        public bool checkAdmin()
        {
            if (loggedUser != null)
            {
                if (loggedUser.Role == "admin")
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}