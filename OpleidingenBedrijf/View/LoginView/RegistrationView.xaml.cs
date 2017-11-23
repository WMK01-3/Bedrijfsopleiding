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

        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ParentViewModel.CurrentView = new LoginView(ParentViewModel);
        }

        private void btnRegister_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ((RegistrationVM)OwnViewModel).RegisterUser();
        }
    }
}
