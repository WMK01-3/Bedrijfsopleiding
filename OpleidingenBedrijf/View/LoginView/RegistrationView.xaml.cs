using System.Windows;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;

namespace BedrijfsOpleiding.View.LoginView
{
    public partial class RegistrationView
    {
        #region OwnViewModel : BaseViewModel

        private RegistrationVM _viewModel;
        public RegistrationVM ViewModel
        {
            get => _viewModel = _viewModel ?? new RegistrationVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public RegistrationView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();

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
            MainVM.CurrentView = new LoginView(MainVM);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RegisterUser();
        }
    }
}
