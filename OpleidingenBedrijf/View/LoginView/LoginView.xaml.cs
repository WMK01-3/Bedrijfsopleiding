using System.Windows;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;

namespace BedrijfsOpleiding.View.LoginView
{
    public partial class LoginView
    {
        private BaseViewModel _parent;
        private BaseViewModel _ownViewModel;

        public LoginView(BaseViewModel parent)
        {
            InitializeComponent();

            _parent = parent;
            _ownViewModel = new LoginVM();

            DataContext = _ownViewModel;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // _parent.CurrentView = new RegistrationView(_parent);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //_ownViewModel.Login();
        }
    }
}
