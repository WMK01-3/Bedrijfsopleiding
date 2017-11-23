using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View;
using BedrijfsOpleiding.Tools;

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

            // PDF TEST
            /*
                Debug.WriteLine("Testing PDF");
                Invoice testInvoice = generateTestData();

            

                generateInvoice.Init(testInvoice);
            */
        }

        private Invoice generateTestData()
        {
            // Generating test users
            var testUser = new User()
            {
                FirstName = "Dirk",
                LastName = "Van RuyterHoffe",
                UserName = "DikkeDirk123",
                PassWord = "Welkom01",
                Email = "DikkeDirk@gmail.com",
                Role = User.RoleEnum.Customer,
                Street = "Bierweg 69",
                City = "middleOfNowhereTown",
                Zipcode = "1337 EZ"
            };


            // Generating test courses
            var testCourse_I = new Course()
            {
                Difficulty = Course.DifficultyEnum.Expert,
                MaxParticipants = 12,
                Price = 45,
                Title = "Bier drinken",
                Description = "Leuke cursus man!",
                Dates = new List<DateTime> { DateTime.Now, DateTime.Now, DateTime.Now },
                Duration = 45,
                Location = new Location("C.112", "Campus 5", "Zwollywood", "8080 ZZ"),
                Teacher = new User("Harold", "Brood zonder boter", "harold.BZB@windesheim.nl")
            };
            var testCourse_II = new Course()
            {
                Difficulty = Course.DifficultyEnum.Moderate,
                MaxParticipants = 4,
                Price = 60,
                Title = "Angular for dummies",
                Description = "O ja pittig leerzaam dit!",
                Dates = new List<DateTime> { DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now },
                Duration = 25,
                Location = new Location("C.142", "Campus 5", "Zwollywood", "8080 ZZ"),
                Teacher = new User("Harold", "Brood zonder boter", "harold.BZB@windesheim.nl")
            };
            var testCourse_III = new Course()
            {
                Difficulty = Course.DifficultyEnum.Expert,
                MaxParticipants = 42,
                Price = 120,
                Title = "CS:GO for n00bs en scrubz",
                Description = "Official mlg pro course, learn to eat doritos, drink mountain dew and pop some 360 no scopes liek a bozz",
                Dates = new List<DateTime> { DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now },
                Duration = 60,
                Location = new Location("C.132", "Campus 5", "Zwollywood", "8080 ZZ"),
                Teacher = new User("Harold", "Brood zonder boter", "harold.BZB@windesheim.nl")
            };


            // inschrijvingen
            var testInschrijving_I = new Enrollment() { Timestamp = DateTime.Now, Payed = false, Course = testCourse_I, User = testUser };
            var testInschrijving_II = new Enrollment() { Timestamp = DateTime.Now, Payed = false, Course = testCourse_II, User = testUser };
            var testInschrijving_III = new Enrollment() { Timestamp = DateTime.Now, Payed = false, Course = testCourse_III, User = testUser };

            // Factuur
            var testFactuur = new Invoice(DateTime.Now, testUser);
            testFactuur.Add(testInschrijving_I);
            testFactuur.Add(testInschrijving_II);
            testFactuur.Add(testInschrijving_III);

            return testFactuur;
        }
    }


}
