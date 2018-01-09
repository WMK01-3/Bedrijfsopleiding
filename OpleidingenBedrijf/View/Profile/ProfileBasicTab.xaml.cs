using System.Windows;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Profile;

namespace BedrijfsOpleiding.View.Profile
{
    /// <summary>
    /// Interaction logic for ProfileBasicTab.xaml
    /// </summary>
    public partial class ProfileBasicTab : BaseView
    {
        #region OwnViewModel : BaseViewModel


        private ProfileBasicTab _view;

        private ProfileBasicTabVM _viewModel;
        public ProfileBasicTabVM ViewModel
        {
            get => _viewModel = _viewModel ?? new ProfileBasicTabVM(MainVM, this);
            set => _viewModel = value;
        }


        #endregion
        public ProfileBasicTab(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
        }

        private void BtnUpdateName_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateName();
        }

        private void BtnUpdateEmail_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateEmail();
        }

        private void BtnUpdatePassword_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdatePassword();
        }
    }

}
