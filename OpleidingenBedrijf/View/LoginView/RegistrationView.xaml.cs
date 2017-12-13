using System.Windows;
using System.Windows.Input;
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

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new LoginView(MainVM);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RegisterUser();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.RegisterUser();
            }
        }
    }
}
