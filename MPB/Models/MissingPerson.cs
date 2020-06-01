using MPB.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPB.Models
{
    public class MissingPerson
    {
        [Key]
        public int PersonId { get; set; }
        [DisplayName("Imię")]
        public string Firstname { get; set; }
        [DisplayName("Nazwisko")]
        public string Lastname { get; set; }
        [DisplayName("Wiek")]
        public int Age { get; set; }
        [DisplayName("Widziany ostatnio")]
        public string Location { get; set; }
        [DisplayName("Zdjęcie")]
        public string Image { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        [NotMapped]
       public HttpPostedFileBase ImageFile { get; set; }

        public static List<MissingPerson> FindAll()
        {
                MPDContext db = new MPDContext();
                List<MissingPerson> persons= db.MissingPersons.ToList();

                return persons;
        }

        public User getUser()
        {
            User user = User.findById(this.UserId);
            return user;
        }

        public static MissingPerson findById(int Id)
        {
            MPDContext db = new MPDContext();
            MissingPerson person = db.MissingPersons.Single(u => u.PersonId == Id);

            return person;
        }

        public void addImage(HttpPostedFileBase file)
        {
            string filename = Path.GetFileNameWithoutExtension(file.FileName).ToString();
            string ext = Path.GetExtension(this.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + ext;
            this.Image = "~/images/" + filename;
            filename = Path.Combine(HttpContext.Current.Server.MapPath("~/images/"), filename);
            this.ImageFile.SaveAs(filename);
        }

        public void deleteImage()
        {
            var filePath = HttpContext.Current.Server.MapPath(this.Image);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void delete() {
            deleteImage();
            MPDContext db = new MPDContext();
            MissingPerson person = db.MissingPersons.Single(u => u.PersonId == this.PersonId);
            db.MissingPersons.Remove(person);
            db.SaveChanges();
        }
    }
}