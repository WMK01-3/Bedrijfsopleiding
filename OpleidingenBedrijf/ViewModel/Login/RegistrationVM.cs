using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BedrijfsOpleiding.View.LoginView;
using System.Diagnostics;

namespace BedrijfsOpleiding.ViewModel.Login
{
     class RegistrationVM : BaseViewModel
    {
        public RegistrationVM(UserControl boundView) : base(boundView)
        {

        }

        public void RegisterUser()
        {
            RegistrationView RV = (RegistrationView)CurrentView;
            string o = RV.dpGeboortedatum.ToString().Length > 0 ? "oi" : "w";
        }
    }
}
