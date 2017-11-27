using System;
using System.Data.Entity;
using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.Tools;
using BedrijfsOpleiding.View;
using System.Linq;

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
                if (context.Database.Exists())
                    Database.SetInitializer<CustomDbContext>(null);

                User user = (from u in context.Users
                             select u).First();

                Invoice invoice = new Invoice(DateTime.Now, user);
                GenerateInvoice.NewPdf(invoice);
            }
        }
    }
}
