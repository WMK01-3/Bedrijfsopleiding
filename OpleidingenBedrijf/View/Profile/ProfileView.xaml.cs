using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Profile;

namespace BedrijfsOpleiding.View.Profile
{
    public partial class ProfileView
    {
        #region ViewModel : BaseViewModel

        private ProfileVM _viewModel;
        public ProfileVM ViewModel
        {
            get => _viewModel = _viewModel ?? new ProfileVM(MainVM);
            set => _viewModel = value;
        }

        #endregion
        
        public ProfileView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();


            if (MainVM.IsTeacher == false || MainVM.IsEmployee == false)
                TabProfession.Visibility = Visibility.Hidden;
        }

        public ProfileView(MainWindowVM mainVM, int tabIndex) : this(mainVM)
        {
            profileTab.SelectedIndex = tabIndex;
        }
    }
}
