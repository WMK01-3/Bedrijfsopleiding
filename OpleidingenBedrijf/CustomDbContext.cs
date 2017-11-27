using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding
{
   
    class CustomDbContext : DbContext
    {
        public CustomDbContext() : base("CustomDbContext") { }


        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

    }
}
