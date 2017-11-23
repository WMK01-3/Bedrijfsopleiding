using System.Diagnostics;
using System.Windows;
using AnimatedContentControlSample.ViewModels;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;

namespace BedrijfsOpleiding.View.LoginView
{
    public partial class LoginView
    {

        public LoginView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            ErrorMessage.Visibility = Visibility.Hidden;
            OwnViewModel = new LoginVM(this);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            ParentViewModel.CurrentView = new RegistrationView(ParentViewModel);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindowVM) ParentViewModel).MenuView = new MenuBar(ParentViewModel);
            // ((LoginVM)OwnViewModel).Login();
        }
    }
}
