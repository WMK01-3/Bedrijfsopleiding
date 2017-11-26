using System.Data.Entity;
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
                mainWindow.WindowState = WindowState.Minimized;

            mainWindow.Show();


            Database.SetInitializer<CustomDbContext>(null);

            using (CustomDbContext context = new CustomDbContext())
            {
                Location loc = new Location("T5", "Shitstreet", "Shitcity", "1234SH");

                Course course = new Course
                {
                    Price = 230,
                    Title = "How to be a professional shit",
                    UserID = 1,
                    LocationID = 1
                };

                context.Locations.Add(loc);
                context.SaveChanges();
                context.Courses.Add(course);
                context.SaveChanges();
            }
        }
    }
}
