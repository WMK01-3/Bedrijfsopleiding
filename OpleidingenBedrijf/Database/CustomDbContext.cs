﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.Database
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext() : base("CustomDbContext")
        {
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Profession> Professions { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<CourseDate> CourseDates { get; set; }

    }
}
