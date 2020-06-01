using MPB.App_Start;
using MPB.Context;
using MPB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPB.Controllers
{
    public class MissingPeopleController : Auth
    {

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult AddNew()
        {
            if (checkLogged())
            {
                return View();
            }else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddNew(MissingPerson person)
        {
            if (checkLogged())
            {
                person.addImage(person.ImageFile);
                person.UserId = loggedUser.UserId;
                MPDContext db = new MPDContext();
                db.MissingPersons.Add(person);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Dodano zaginionego!";
                return View();
            }
            else
            {
                ViewBag.Message = "Nie możesz dodać użytkownika, musisz się zalogować!";
                return View(person);
            }
        }

        public ActionResult PeopleList(string value)
        {
            checkLogged();
            List<MissingPerson> list = null;
            if (value == null)
            {
                list = MissingPerson.FindAll();
            }
            else
            {
                MPDContext db = new MPDContext();
                list = db.MissingPersons.Where(u => (u.Firstname.Contains(value)) || u.Lastname.Contains(value)).ToList();
            }
            ViewBag.MissingPeople = list;
            return View();
        }

        public ActionResult PersonDetails(int id)
        {
            checkLogged();
            if (id != 0)
            {
                MissingPerson person = MissingPerson.findById(id);
                ViewBag.person = person;
                return View();
            }
            else
            {
                return RedirectToAction("PeopleList");
            }
        }

        public ActionResult deletePerson(int id)
        {
            if(checkLogged() && checkAdmin())
            {
                MissingPerson person = MissingPerson.findById(id);
                person.delete();
            }
            return RedirectToAction("PeopleList");
        }

        [HttpPost]
        public ActionResult EditPerson(MissingPerson newData)
        {
            checkLogged();
            MPDContext db = new MPDContext();
            MissingPerson person = MissingPerson.findById(newData.PersonId);
            person.Firstname = newData.Firstname;
            person.Lastname = newData.Lastname;
            person.Age = newData.Age;
            person.Location = newData.Location;
            person.Description = newData.Description;

            db.MissingPersons.AddOrUpdate(person);
            db.SaveChanges();
            ViewBag.formPerson = person;
            
            return RedirectToAction("PersonDetails", new { id=person.PersonId});
        }
    }
}