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
            #region ErrorControllers
            #region ErrorIcons
            RV.ecUsername.Visibility = RV.tbUsername.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecBirthdate.Visibility = RV.dpBirthDate.ToString().Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecFirstName.Visibility = RV.tbFirstName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecEmail.Visibility = RV.tbEmail.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecLastName.Visibility = RV.tbLastName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecCityName.Visibility = RV.tbCityName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecStreetName.Visibility = RV.tbStreetName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecZipCode.Visibility = RV.tbZipCode.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecPassword.Visibility = RV.pbPassword.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.ecRepeatPassword.Visibility = RV.pbPasswordRepeat.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            #endregion
            #region ErrorBorders
            RV.tbUsername.BorderBrush = RV.tbUsername.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.pbPassword.BorderBrush = RV.pbPassword.Password.Length == 0 ? _redBrush : _blueBrush;
            RV.tbFirstName.BorderBrush = RV.tbFirstName.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.tbLastName.BorderBrush = RV.tbLastName.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.dpBirthDate.BorderBrush = RV.dpBirthDate.ToString().Length == 0 ? _redBrush : _blueBrush;
            RV.tbEmail.BorderBrush = RV.tbEmail.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.tbZipCode.BorderBrush = RV.tbZipCode.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.tbStreetName.BorderBrush = RV.tbStreetName.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.tbCityName.BorderBrush = RV.tbCityName.Text.Length == 0 ? _redBrush : _blueBrush;
            RV.pbPasswordRepeat.BorderBrush = RV.pbPasswordRepeat.Password.Length == 0 ? _redBrush : _blueBrush;
            #endregion
            #region ErrorMessages
            RV.elbUsername.Visibility = RV.tbUsername.Text.Length==0?Visibility.Visible:Visibility.Hidden;
            RV.elbPassword.Visibility = RV.pbPassword.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.elbPasswordRepeat.Visibility = RV.pbPasswordRepeat.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.elbEmail.Visibility = RV.tbEmail.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.elbFirstName.Visibility = RV.tbFirstName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.elbLastName.Visibility = RV.tbLastName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.elbZipCode.Visibility = RV.tbZipCode.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.elbCityName.Visibility = RV.tbCityName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.elbStreetName.Visibility = RV.tbStreetName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
            RV.elbBirthDate.Visibility = RV.dpBirthDate.ToString().Length == 0 ? Visibility.Visible : Visibility.Hidden;
            #endregion
            errorCount += RV.tbEmail.Text.Length == 0 ? 1 : 0;
            errorCount += RV.tbStreetName.Text.Length == 0 ? 1 : 0;
            errorCount += RV.tbCityName.Text.Length == 0 ? 1 : 0;
            errorCount += RV.tbZipCode.Text.Length == 0 ? 1 : 0;
            errorCount += RV.tbFirstName.Text.Length == 0 ? 1 : 0;
            errorCount += RV.tbLastName.Text.Length == 0 ? 1 : 0;
            errorCount += RV.tbUsername.Text.Length == 0 ? 1 : 0;
            errorCount += RV.pbPassword.Password.Length == 0 ? 1 : 0;
            errorCount += RV.pbPasswordRepeat.Password.Length == 0 ? 1 : 0;
            errorCount += RV.dpBirthDate.ToString().Length == 0 ? 1 : 0;
            if (RV.pbPassword.Password != RV.pbPasswordRepeat.Password)
            {
                errorCount += 1;
            }
            #endregion
            if (errorCount == 0)
            {
                using (var context = new CustomDbContext())
                {
                    var addedUser = new User()
                    {
                        FirstName = RV.tbFirstName.Text,
                        LastName = RV.tbLastName.Text,
                        UserName = RV.tbUsername.Text,
                        PassWord = RV.pbPassword.Password,
                        Email = RV.tbEmail.Text,
                        Role = User.RoleEnum.Customer,
                        Street = RV.tbStreetName.Text,
                        City = RV.tbCityName.Text,
                        Zipcode = RV.tbZipCode.Text
                    };
                    context.Users.Add(addedUser);
                    context.SaveChanges();
                }
            }
            
            }
    }
}
