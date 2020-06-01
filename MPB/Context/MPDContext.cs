using MPB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MPB.Context
{
    public class MPDContext: DbContext
    {
        public MPDContext() :base("MPDDatabase")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<MissingPerson> MissingPersons { get; set; }
    }
}