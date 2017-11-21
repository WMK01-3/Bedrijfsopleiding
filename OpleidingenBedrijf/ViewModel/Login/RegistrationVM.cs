using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BedrijfsOpleiding.View.LoginView;
using System.Diagnostics;
using System.Reflection;
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

            RV.tbGebruikersnaam.BorderBrush = RV.tbGebruikersnaam.Text.Length == 0 ? _redBrush : _blueBrush;

            RV.pbWachtwoord.BorderBrush = RV.pbWachtwoord.Password.Length == 0 ? _redBrush : _blueBrush;

            RV.tbVoornaam.BorderBrush = RV.tbVoornaam.Text.Length == 0 ? _redBrush : _blueBrush;

            RV.tbAchternaam.BorderBrush = RV.tbAchternaam.Text.Length == 0 ? _redBrush : _blueBrush;

            RV.dpGeboortedatum.BorderBrush = RV.dpGeboortedatum.ToString().Length == 0 ? _redBrush : _blueBrush;

            RV.tbEmail.BorderBrush = RV.tbEmail.Text.Length == 0 ? _redBrush : _blueBrush;

            RV.tbZipCode.BorderBrush = RV.tbZipCode.Text.Length == 0 ? _redBrush : _blueBrush;

            RV.tbStreet.BorderBrush = RV.tbStreet.Text.Length == 0 ? _redBrush : _blueBrush;

            RV.tbPlace.BorderBrush = RV.tbPlace.Text.Length == 0 ? _redBrush : _blueBrush;

            RV.pbWachtwoordHerhalen.BorderBrush = RV.pbWachtwoordHerhalen.Password.Length == 0 ? _redBrush : _blueBrush;
        }
    }
}
