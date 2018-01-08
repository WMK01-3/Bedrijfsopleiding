using System;
using System.Data.Entity;
using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.Tools;
using BedrijfsOpleiding.View;
using System.Linq;
using BedrijfsOpleiding.Database;

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

            using (CustomDbContext context = new CustomDbContext())
            {
                context.SaveChanges();
                
                if (context.Database.Exists())
                    System.Data.Entity.Database.SetInitializer<CustomDbContext>(null);

                if (context.Users.Any() == false && context.Courses.Any() == false)
                    DummyData.AddData();
            }
        }
    }
}
