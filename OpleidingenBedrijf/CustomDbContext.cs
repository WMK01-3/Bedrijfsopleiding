using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding
{

    class CustomDbContext : DbContext
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

        public void AddDummyData()
        {
            IQueryable<Models.Location> resultsLocation = from location in this.Locations
                                                          select location;
            IQueryable<Models.User> resultsUser = from user in this.Users
                                                  select user;
            if (!resultsLocation.Any() && !resultsUser.Any())
            {
                List<Location> locationList = new List<Location>
                {
                    new Location
                    {
                        Country = "Nederland",
                        City = "Zwolle",
                        Street = "Assendorperstraat"
                    },
                    new Location
                    {
                        Country = "Nederland",
                        City = "Amersfoort",
                        Street = "Trompetstraat"
                    },
                    new Location
                    {
                        Country = "Nederland",
                        City = "Heemskerk",
                        Street = "De Trompet"
                    },
                    new Location
                    {
                        Country = "Nederland",
                        City = "Den Haag",
                        Street = "Klimopstraat"
                    },
                    new Location
                    {
                        Country = "Nederland",
                        City = "Maastricht",
                        Street = "Aamruwe"
                    },
                };
                foreach (Location loc in locationList)
                {
                    this.Locations.Add(loc);
                }
                this.SaveChanges();
                List<User> userList = new List<User>
                {
                    new User
                    {
                        UserName = "Customer",
                        Email = "email",
                        FirstName = "klant",
                        LastName = "account",
                        PassWord = "pw",
                        Role = User.RoleEnum.Customer
                    },
                    new User
                    {
                        UserName = "Teacher",
                        Email = "email",
                        FirstName = "leraar",
                        LastName = "account",
                        PassWord = "pw",
                        Role = User.RoleEnum.Teacher
                    },
                    new User
                    {
                        UserName = "Admin",
                        Email = "email",
                           FirstName = "beheerder",
                        LastName = "account",
                        PassWord = "pw",
                        Role = User.RoleEnum.Employee
                    },
                };
                foreach (User user in userList)
                {
                    this.Users.Add(user);
                }
                this.SaveChanges();
                List<Course> courseList = new List<Course>
                {
                    new Course
                    {
                        Title = "Tekenen",
                        Duration = 45,
                        Archived = false,
                        Price = 20,
                        Description = "Tekenen voor dummies",
                        UserID = 2,
                        MaxParticipants = 0,
                        Difficulty = Course.DifficultyEnum.Moderate,
                        LocationID = 1
                    },
                    new Course
                    {
                        Title = "Breien",
                        Duration = 25,
                        Archived = false,
                        Price = 15,
                        Description = "Breien voor dummies",
                        UserID = 2,
                        MaxParticipants = 0,
                        Difficulty = Course.DifficultyEnum.Beginner,
                        LocationID = 2
                    },
                    new Course
                    {
                        Title = "Photoshop",
                        Duration = 50,
                        Archived = false,
                        Price = 24,
                        Description = "Photoshop voor dummies",
                        UserID = 2,
                        MaxParticipants = 0,
                        Difficulty = Course.DifficultyEnum.Expert,
                        LocationID = 4
                    },
                    new Course
                    {
                        Title = "Java",
                        Duration = 65,
                        Archived = false,
                        Price = 30,
                        Description = "Java voor dummies",
                        UserID = 2,
                        MaxParticipants = 0,
                        Difficulty = Course.DifficultyEnum.Expert,
                        LocationID = 3
                    },
                    new Course
                    {
                        Title = "archived",
                        Duration = 21,
                        Archived = true,
                        Price = 30,
                        Description = "Archived voor dummies",
                        UserID = 2,
                        MaxParticipants = 0,
                        Difficulty = Course.DifficultyEnum.Moderate,
                        LocationID = 1
                    },
                };
                foreach (Course c in courseList)
                {
                    this.Courses.Add(c);
                }
                this.SaveChanges();
            }
        }
    }
}
