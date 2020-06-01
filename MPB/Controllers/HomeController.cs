using MPB.App_Start;
using MPB.Context;
using MPB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPB.Controllers
{
    public class HomeController : Auth
    {
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                return RedirectToAction("LoggedIn");
            }
            return View();

        }

        public ActionResult AddNewUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewUser(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.checkLoginIsset())
                {
                    MPDContext db = new MPDContext();
                    user.Role = "user";
                    user.Active = false;
                    db.Users.Add(user);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = user.Login + " został zarejestrowany. Aby wpełni korzystać z systemu, konto musi potwierdzić administrator.";
                    return View();
                }else
                {
                    ViewBag.Message = "Podany login już istnieje w bazie";
                    return View("AddNewUser", user);
                }
            }else
            {
                return View("AddNewUser", user);
            }
        }

        public ActionResult Login()
        {
            if (!checkLogged())
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoggedIn");
            }
            
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user != null)
            {
                MPDContext db = new MPDContext();
                User usr = db.Users.Where(u => u.Login == user.Login && u.Password == user.Password).FirstOrDefault<User>();
                if (usr != null)
                {
                    if (usr.Role != "admin" && !usr.Active)
                    {
                        ViewBag.Message = "Twoje konto nie zostało zatwierdzone przez administratora! Nie możesz się zalogować.";
                    }
                    else
                    {
                        Session["UserID"] = usr.UserId.ToString();
                        checkLogged();
                        return RedirectToAction("LoggedIn");
                    }

                }
                else
                {
                    ViewBag.Message = "Login lub hasło jest nieprawidłowe!";
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if(checkLogged())
            {
                if (checkAdmin())
                {
                    return RedirectToAction("Index", "Admin");
                }
                return View();
            }else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Loggout()
        {
            Session["UserID"] = null;
            Session.Abandon();
            ViewBag.Message = "Wylogowano";
            return RedirectToAction("Login", "Home");
        }

    }
}