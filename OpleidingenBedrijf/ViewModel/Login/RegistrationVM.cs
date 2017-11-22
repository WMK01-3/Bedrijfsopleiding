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

namespace BedrijfsOpleiding.ViewModel.Login
{
     class RegistrationVM : BaseViewModel
    {
        private readonly SolidColorBrush _redBrush = new SolidColorBrush(Colors.Tomato);
        private readonly SolidColorBrush _blueBrush = new SolidColorBrush(Colors.CornflowerBlue);
        public RegistrationVM(UserControl boundView) : base(boundView)
        {

        }
        public void RegisterUser()
        {
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

            RV.elbUsername.Content = "kwo";
            if (RV.pbPassword.Password != RV.pbPasswordRepeat.Password)
            {
                
            }






            #endregion







        }
    }
}
