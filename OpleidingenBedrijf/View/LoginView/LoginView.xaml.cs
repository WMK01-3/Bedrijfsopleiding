using System.Windows;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;

namespace BedrijfsOpleiding.View.LoginView
{
    public partial class LoginView
    {
        public LoginView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            OwnViewModel = new LoginVM(this);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // ParentViewModel.CurrentView = new RegistrationView(_parent);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Visibility = Visibility.Collapsed;
            ((LoginVM)OwnViewModel).Login();
        }
    }
}
