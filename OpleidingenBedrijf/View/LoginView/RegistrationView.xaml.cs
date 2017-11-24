using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;
﻿using System.Diagnostics;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View.LoginView
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : BaseView
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
            ((RegistrationVM) OwnViewModel).RegisterUser();
        }
    }
}
