using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BedrijfsOpleiding.View.LoginView;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using BedrijfsOpleiding.Models;


namespace BedrijfsOpleiding.ViewModel.Login
{
     class RegistrationVM : BaseViewModel
     {
        private int errorCount = 0;
        private readonly SolidColorBrush _redBrush = new SolidColorBrush(Colors.Tomato);
        private readonly SolidColorBrush _blueBrush = new SolidColorBrush(Colors.CornflowerBlue);
        public RegistrationVM(UserControl boundView) : base(boundView)
        {

        }
        public void RegisterUser()
        {
            errorCount = 0;
            RegistrationView RV = (RegistrationView)CurrentView;
            using (var context = new CustomDbContext())
            {
                var userNameCount = (from u in context.Users
                    where u.UserName == RV.tbUsername.Text
                              select u.UserName).Count();

                


                #region ErrorControllers
                #region ErrorIcons

                if (RV.tbUsername.Text.Length == 0)
                {
                    RV.ecUsername.Visibility = Visibility.Visible;
                }
                else
                {
                    RV.ecUsername.Visibility = userNameCount == 0 ? Visibility.Hidden: Visibility.Visible;
                }
                
            
            RV.ecBirthdate.Visibility = RV.dpBirthDate.ToString().Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecFirstName.Visibility = RV.tbFirstName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecEmail.Visibility = RV.tbEmail.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecLastName.Visibility = RV.tbLastName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecPassword.Visibility = RV.pbPassword.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecRepeatPassword.Visibility = RV.pbPasswordRepeat.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecLastName.Visibility = RV.tbLastName.Text.IsName() ? Visibility.Hidden : Visibility.Visible;
            RV.ecEmail.Visibility = RV.tbEmail.Text.IsEmail() ? Visibility.Hidden : Visibility.Visible;
            RV.ecFirstName.Visibility = RV.tbFirstName.Text.IsName() ? Visibility.Hidden : Visibility.Visible;
            RV.ecPassword.Visibility = RV.pbPassword.Password.IsPassword() ? Visibility.Hidden : Visibility.Visible;
            RV.ecRepeatPassword.Visibility = RV.pbPasswordRepeat.Password.IsPassword() ? Visibility.Hidden : Visibility.Visible;
            
            
                
            #endregion
            #region ErrorBorders
            
            RV.pbPassword.BorderBrush = RV.pbPassword.Password.Length == 0 ? _redBrush : _blueBrush;
            RV.tbFirstName.BorderBrush = RV.tbFirstName.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.tbLastName.BorderBrush = RV.tbLastName.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.dpBirthDate.BorderBrush = RV.dpBirthDate.ToString().Length == 0 ? _redBrush : _blueBrush;
            RV.tbEmail.BorderBrush = RV.tbEmail.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.pbPasswordRepeat.BorderBrush = RV.pbPasswordRepeat.Password.Length == 0 ? _redBrush : _blueBrush;
            RV.tbFirstName.BorderBrush = RV.tbFirstName.Text.IsName() ? _blueBrush : _redBrush;
            RV.tbLastName.BorderBrush = RV.tbLastName.Text.IsName() ? _blueBrush : _redBrush;
            RV.tbEmail.BorderBrush = RV.tbEmail.Text.IsEmail() ? _blueBrush : _redBrush;
            RV.pbPassword.BorderBrush = RV.pbPassword.Password.IsPassword() ? _blueBrush : _redBrush;
            RV.pbPasswordRepeat.BorderBrush = RV.pbPasswordRepeat.Password.IsPassword() ? _blueBrush : _redBrush;
            RV.tbUsername.BorderBrush = userNameCount == 0 ?  _blueBrush : _redBrush;
                if (RV.tbUsername.Text.Length == 0)
                {
                    RV.tbUsername.BorderBrush = _redBrush;
                }
                else
                {
                    RV.tbUsername.BorderBrush = userNameCount == 0 ? _blueBrush : _redBrush;
                }
            #endregion
            #region ErrorMessages
            RV.elbPassword.Content = RV.pbPassword.Password.Length == 0 ? "Dit veld is verplicht. " : "";
            RV.elbBirthDate.Content = RV.dpBirthDate.ToString().Length == 0 ? "Dit veld is verplicht. " : "";
            RV.elbLastName.Content = RV.tbLastName.Text.Length == 0 ? "Dit veld is verplicht. " : "";
            RV.elbPasswordRepeat.Content = RV.pbPasswordRepeat.Password.Length == 0 ? "Dit veld is verplicht. " : "";
           

                if (RV.tbUsername.Text.Length == 0)
                {
                    RV.elbUsername.Content = "Dit veld is verplicht";
                }
                else
                {
                    if (userNameCount == 0)
                    {
                        RV.elbUsername.Content = "";
                    }
                    else
                    {
                        RV.elbUsername.Content = "Deze gebruikersnaam is al in gebruik.";
                    }
                }
            if (RV.tbEmail.Text.Length == 0)
            {
                RV.elbEmail.Content = "Dit veld is verplicht.";
            }
            else
            {
                RV.elbEmail.Content = !RV.tbEmail.Text.IsEmail() ? "Email adres voldoet niet aan de eisen." : "";
            }
            if (RV.tbFirstName.Text.Length == 0)
            {
                RV.elbFirstName.Content = "Dit veld is verplicht.";
            }
            else
            {
                RV.elbFirstName.Content = !RV.tbFirstName.Text.IsName() ? "Voornaam voldoet niet aan de eisen." : "";
            }
            if (RV.tbLastName.Text.Length == 0)
            {
                RV.elbLastName.Content = "Dit veld is verplicht.";
            }
            else
            {
                RV.elbLastName.Content = !RV.tbLastName.Text.IsName() ? "Achternaam voldoet niet aan de eisen." : "";
            }
            if (RV.pbPassword.Password.Length == 0)
            {
                RV.elbPassword.Content = "Dit veld is verplicht.";
            }
            else
            {
                RV.elbPassword.Content = !RV.pbPassword.Password.IsPassword() ? "Wachtwoord veriest een 1 number, 1 letter, 1 special character, between 8 and 24 characters" : "";
                if (!Equals(RV.pbPassword.Password, RV.pbPasswordRepeat.Password))
                {
                    RV.elbPassword.Content = "Wachtwoorden komen niet overeen.";
                }
            }
            if (RV.pbPasswordRepeat.Password.Length == 0)
            {
                RV.elbPasswordRepeat.Content = "Dit veld is verplicht.";
            }
            else
            {
                RV.elbPasswordRepeat.Content = !RV.pbPasswordRepeat.Password.IsPassword() ? "Wachtwoord veriest een 1 number, 1 letter, 1 special character, between 8 and 24 characters" : "";
            }
            #endregion
            #endregion


            errorCount += RV.tbEmail.Text.Length == 0 ? 1 : 0;

            errorCount += userNameCount == 0 ? 0 : 1;
   
            errorCount += RV.tbFirstName.Text.Length == 0 ? 1 : 0;
         
            errorCount += RV.tbLastName.Text.Length == 0 ? 1 : 0;
           
            errorCount += RV.tbUsername.Text.Length == 0 ? 1 : 0;
            
            errorCount += RV.pbPassword.Password.Length == 0 ? 1 : 0;
            errorCount += RV.pbPasswordRepeat.Password.Length == 0 ? 1 : 0;
            errorCount += RV.dpBirthDate.ToString().Length == 0 ? 1 : 0;
            errorCount += Equals(RV.pbPassword.Password, RV.pbPasswordRepeat.Password) ? 0 : 1;
            errorCount += RV.tbEmail.Text.IsEmail() ? 0 : 1;
            errorCount += RV.pbPassword.Password.IsPassword() ? 0 : 1;
            errorCount += RV.tbFirstName.Text.IsName() ? 0 : 1;
            errorCount += RV.tbLastName.Text.IsName() ? 0 : 1;





                if (errorCount == 0)
                {

                    var addedUser = new User()
                    {
                        FirstName = RV.tbFirstName.Text,
                        LastName = RV.tbLastName.Text,
                        UserName = RV.tbUsername.Text,
                        PassWord = RV.pbPassword.Password,
                        Email = RV.tbEmail.Text,
                        Role = User.RoleEnum.Customer,
                        Street = "yet to be implemented street",
                        City = "Yet to be implemented city",
                        Zipcode = "Yet to be implemented zip"
                    };
                    context.Users.Add(addedUser);
                    context.SaveChanges();
                    RV.ParentViewModel.CurrentView = new LoginView(RV.ParentViewModel);
                }
            }
            
            }
    }
}
