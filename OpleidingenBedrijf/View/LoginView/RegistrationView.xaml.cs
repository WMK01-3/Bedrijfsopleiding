using System.Windows;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;

namespace BedrijfsOpleiding.View.LoginView
{
    public partial class RegistrationView
    {
        public RegistrationView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            OwnViewModel = new RegistrationVM(this);
            
            #region hideControls
            ecUsername.Visibility = Visibility.Hidden;
            ecBirthdate.Visibility = Visibility.Hidden;
            ecEmail.Visibility = Visibility.Hidden;
            ecFirstName.Visibility = Visibility.Hidden;
            ecLastName.Visibility = Visibility.Hidden;
            ecPassword.Visibility = Visibility.Hidden;
            ecRepeatPassword.Visibility = Visibility.Hidden;
            #endregion
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ParentViewModel.CurrentView = new LoginView(ParentViewModel);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            ((RegistrationVM)OwnViewModel).RegisterUser();
        }
    }
}
