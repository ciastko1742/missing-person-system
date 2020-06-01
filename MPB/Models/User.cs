using MPB.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MPB.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Podaj Login")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Podaj hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }

        public bool checkLoginIsset() {

            MPDContext db = new MPDContext();
            var usr = db.Users.Where(u => u.Login == this.Login).FirstOrDefault<User>();

            if(usr != null)
                return false;
            return true;
        }

        public string nameActive()
        {
            if (this.Active)
            {
                return "<span style='color: green'>Aktywne</span>";
            }else
            {
                return "<span style='color: red'>Nieaktywne</span>";
            }
        }

        public void changeRole()
        {
                MPDContext db = new MPDContext();
                User user = db.Users.Single(u => u.UserId == this.UserId);
                if(user.Role == "admin")
            {
                user.Role = "user";
            }else
            {
                user.Role = "admin";
            }
                db.SaveChanges();
        }


        public void changeAccountActive()
        {
            MPDContext db = new MPDContext();
            User user = db.Users.Single(u => u.UserId == this.UserId);
            if (user.Active)
            {
                user.Active = false;
            }
            else
            {
                user.Active = true;
            }
            db.SaveChanges();
        }

        public static List<User> FindAll()
        {
            MPDContext db = new MPDContext();
            List<User> users = db.Users.ToList();

            return users;
        }

        public static User findById(int Id)
        {
            MPDContext db = new MPDContext();
            User user = db.Users.Single(u => u.UserId == Id);

            return user;
        }
    }
}