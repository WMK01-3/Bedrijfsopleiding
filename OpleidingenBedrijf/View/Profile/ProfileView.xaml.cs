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

            if (!MainVM.IsTeacher || !MainVM.IsEmployee)
            {
                TabProfession.Visibility = Visibility.Hidden;
            }
            // if (vm.CurUser.Role != User.RoleEnum.Teacher)
            //    profileTab.SelectedIndex += 1;
        }



    }
}
