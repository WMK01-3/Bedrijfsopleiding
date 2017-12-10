using System;
using System.Diagnostics;
using System.Windows;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Profile;

namespace BedrijfsOpleiding.View.Profile
{
    public partial class ProfileProfessionTab
    {
        #region ViewModel : BaseViewModel

        private ProfileProfessionTabVM _viewModel;
        public ProfileProfessionTabVM ViewModel
        {
            get => _viewModel = _viewModel ?? new ProfileProfessionTabVM(MainVM);
            set => _viewModel = value;
        }

        #endregion


        public ProfileProfessionTab(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
        }

        private void RemoveCategory_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.AddProfession(txtBoxNewCategory.Text);
        }
    }
}
