using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Media;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;
using FontAwesome.WPF;

namespace BedrijfsOpleiding.View.LoginView
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : BaseView
    {
        public RegistrationView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            OwnViewModel = new RegistrationVM(this);

            #region hideControls
            ecUsername.Visibility = Visibility.Hidden;
            ecBirthdate.Visibility = Visibility.Hidden;
            ecCityName.Visibility = Visibility.Hidden;
            ecEmail.Visibility = Visibility.Hidden;
            ecFirstName.Visibility = Visibility.Hidden;
            ecLastName.Visibility = Visibility.Hidden;
            ecZipCode.Visibility = Visibility.Hidden;
            ecStreetName.Visibility = Visibility.Hidden;
            ecCityName.Visibility = Visibility.Hidden;
            ecPassword.Visibility = Visibility.Hidden;
            ecRepeatPassword.Visibility = Visibility.Hidden;
            #endregion
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ParentViewModel.CurrentView = new LoginView(ParentViewModel);
        }

        private void btnRegister_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           ((RegistrationVM) OwnViewModel).RegisterUser();
        }
    }
}
