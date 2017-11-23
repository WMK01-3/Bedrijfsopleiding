﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View;

namespace BedrijfsOpleiding
{
    public partial class App
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            // Application is running 
            // Process command line args 
            var startMinimized = false;

            for (var i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "/StartMinimized")
                    startMinimized = true;
            }

            // Create main application window, starting minimized if specified
            MainWindow mainWindow = new MainWindow();
            if (startMinimized)
            {
                mainWindow.WindowState = WindowState.Minimized;
            }
            mainWindow.Show();

            // EF stuff
            Debug.WriteLine("Testing db");

            using (var context = new CustomDbContext())
            {
                Debug.WriteLine("Adding address");

                //var testUser = new User()
                //{
                //    FirstName = "Dirk",
                //    LastName = "Van RuyterHoffe",
                //    Gender = User.GenderEnum.Male,
                //    Email = "DikkeDirk@gmail.com",
                //    Role = User.RoleEnum.Customer,
                //    Street = "Bierweg 69",
                //    City = "middleOfNowhereTown",
                //    Zipcode = "1337 EZ"
                //};

                //var testTeacher = new User()
                //{
                //    FirstName = "Robin",
                //    LastName = "Schult",
                //    Gender = User.GenderEnum.Male,
                //    Email = "RS@gmail.com",
                //    Role = User.RoleEnum.Teacher,
                //    Street = "Bierweg 69",
                //    City = "middleOfNowhereTown",
                //    Zipcode = "1337 EZ"
                //};

                //var testTeacher2 = new User()
                //{
                //    FirstName = "Lola",
                //    LastName = "Trola",
                //    Gender = User.GenderEnum.Male,
                //    Email = "LT@gmail.com",
                //    Role = User.RoleEnum.Teacher,
                //    Street = "Bierweg 69",
                //    City = "middleOfNowhereTown",
                //    Zipcode = "1337 EZ"
                //};

                //var testTeacher3 = new User()
                //{
                //    FirstName = "Mike",
                //    LastName = "Edinger",
                //    Gender = User.GenderEnum.Male,
                //    Email = "MGM@gmail.com",
                //    Role = User.RoleEnum.Teacher,
                //    Street = "Snepg 69",
                //    City = "middleOfNowhereTown",
                //    Zipcode = "1337 EZ"
                //};

                //var location1 = new Location()
                //{
                //    Classroom = "X4.01"
                //};

                //var location2 = new Location()
                //{
                //    Classroom = "C2.65"
                //};

                //context.Locations.Add(location1);
                //context.Locations.Add(location2);
                //context.Users.Add(testUser);
                //context.Users.Add(testTeacher);
                //context.Users.Add(testTeacher2);
                //context.Users.Add(testTeacher3);

                //context.SaveChanges();

                Debug.WriteLine("Done");

                var users = (from s in context.Users
                    orderby s.Street
                    select s).ToList<User>();

                Debug.WriteLine("Fetching users from db");


                foreach (var usr in users)
                {
                    string name = usr.FirstName + " " + usr.LastName;
                    Console.WriteLine(@"ID: {0}, Name: {1}", usr.UserID, name);
                }

                Debug.WriteLine("Done");

                /*
                    var user = new User
                    {
                        FirstName = "Dirk",
                        LastName = "Bietsma",
                        Gender = User.GenderEnum.Male,
                        Email = "DikkeDirk@gmail.com",
                        Role = User.RoleEnum.Customer,
                    
                        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                    };

                    context.Students.Add(student);
                */
            }
        }
    }
}
