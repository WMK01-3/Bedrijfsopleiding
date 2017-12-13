using System.Data.Entity;
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
        
        public DbSet<Profession> Professions { get; set; }

        public DbSet<CourseDate> CourseDates { get; set; }

    }
}
