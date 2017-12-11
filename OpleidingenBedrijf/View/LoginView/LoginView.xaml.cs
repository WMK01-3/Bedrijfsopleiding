using System.Windows;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;

namespace BedrijfsOpleiding.View.LoginView
{
    public partial class LoginView
    {

        #region OwnViewModel : BaseViewModel

        private LoginVM _viewModel;
        public LoginVM ViewModel
        {
            get => _viewModel = _viewModel ?? new LoginVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public LoginView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
            ErrorMessage.Visibility = Visibility.Hidden;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new RegistrationView(MainVM);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LoginAsync();
        }
    }
}
