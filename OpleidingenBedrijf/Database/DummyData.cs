using System;
using System.Collections.Generic;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.Database
{
    public static class DummyData
    {
        #region Dummy Data Lists

        #region Users

        private static List<User> _userList = new List<User>
        {
            new User
            {
                UserName = "Customer",
                Email = "email",
                FirstName = "Klant",
                LastName = "Account",
                PassWord = "pw",
                Role = User.RoleEnum.Customer
            },
            new User
            {
                UserName = "Teacher",
                Email = "email",
                FirstName = "Leraar",
                LastName = "Account",
                PassWord = "pw",
                Role = User.RoleEnum.Teacher
            },
            new User
            {
                UserName = "Admin",
                Email = "email",
                FirstName = "Beheerder",
                LastName = "Account",
                PassWord = "pw",
                Role = User.RoleEnum.Employee
            }
        };

        #endregion

        #region Courses

        private static List<Course> _courseList = new List<Course>
                {
                    new Course
                    {
                        Title = "Tekenen",
                        Duration = 45,
                        Archived = false,
                        Price = 20,
                        Description = "Tekenen voor dummies",
                        UserID = 2,
                        MaxParticipants = 20,
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
                        MaxParticipants = 10,
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
                        MaxParticipants = 9,
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
                        MaxParticipants = 25,
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
                        MaxParticipants = 10,
                        Difficulty = Course.DifficultyEnum.Moderate,
                        LocationID = 1
                    },
                };

        #endregion

        #region Locations

        private static List<Location> _locationList = new List<Location>
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

        #endregion

        #region CourseDates

        private static List<CourseDate> _courseDates = new List<CourseDate>()
        {
            new CourseDate
            {
                CourseID = 1,
                ClassRoom = "T5.01",
                Date = new DateTime(2018, 1, 12, 12, 30, 0)
            },
            new CourseDate
            {
                CourseID = 1,
                ClassRoom = "T5.01",
                Date = new DateTime(2018, 1, 10, 10, 0, 0)
            },
            new CourseDate
            {
                CourseID = 2,
                ClassRoom = "T4.01",
                Date = new DateTime(2018, 1, 10, 10, 0, 0)
            },
            new CourseDate
            {
                CourseID = 2,
                ClassRoom = "T4.01",
                Date = new DateTime(2018, 1, 11, 14, 30, 0)
            },
            new CourseDate
            {
                CourseID = 2,
                ClassRoom = "T4.01",
                Date = new DateTime(2018, 1, 11, 10, 0, 0)
            },
            new CourseDate
            {
                CourseID = 3,
                ClassRoom = "T5.01",
                Date = new DateTime(2018, 1, 12, 9, 0, 0)
            },
            new CourseDate
            {
                CourseID = 4,
                ClassRoom = "T2.01",
                Date = new DateTime(2018, 1, 12, 9, 0, 0)
            },
            new CourseDate
            {
                CourseID = 5,
                ClassRoom = "T2.01",
                Date = new DateTime(2018, 1, 12, 9, 0, 0)
            }
        };

        #endregion

        #endregion

        /// <summary>
        /// Adds a list of different user accounts
        /// </summary>
        public static void AddData()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                foreach (Location loc in _locationList)
                    context.Locations.Add(loc);
                context.SaveChanges();

                foreach (User user in _userList)
                    context.Users.Add(user);
                context.SaveChanges();

                foreach (Course c in _courseList)
                    context.Courses.Add(c);
                context.SaveChanges();

                foreach (CourseDate courseDate in _courseDates)
                    context.CourseDates.Add(courseDate);
                context.SaveChanges();
            }
        }
    }
}
