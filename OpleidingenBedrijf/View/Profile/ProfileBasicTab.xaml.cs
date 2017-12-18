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

        public ProfileBasicTab(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
        }

        private void BtnUpdateAcc_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

}
