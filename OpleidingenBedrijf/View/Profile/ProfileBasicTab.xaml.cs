using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
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

        private void BtnUpdateAcc_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateAccount();
        }
    }

}
