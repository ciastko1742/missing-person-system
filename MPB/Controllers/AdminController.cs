using MPB.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPB.Context;
using MPB.Models;

namespace MPB.Controllers
{
    public class AdminController : Auth
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (checkLogged() && checkAdmin() )
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult UserList()
        {
            if(checkLogged() && checkAdmin())
            {
                List<User> users = Models.User.FindAll();
                ViewBag.Users = users;

                return View();
            }else
            {
                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult UserDetails(int id)
        {
            if (checkLogged() && checkAdmin())
            {
                if (id != 0)
                {
                    User user = Models.User.findById(id);
                    ViewBag.user = user;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ChangeRole(int id)
        {
            if (checkLogged() && checkAdmin() && id != 0)
            {
                User user = Models.User.findById(id);
                user.changeRole();
                ViewBag.user = user;
                return RedirectToAction("UserDetails", new { id = user.UserId });
            }else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Activate(int id)
        {
            if (checkLogged() && checkAdmin() && id != 0)
            {
                User user = Models.User.findById(id);
                user.changeAccountActive();
                ViewBag.user = user;
                return RedirectToAction("UserDetails", new { id = user.UserId });
            }else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}