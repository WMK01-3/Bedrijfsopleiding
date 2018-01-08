using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
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
            int profID = int.Parse(((Button)sender).Tag.ToString());

            using (CustomDbContext context = new CustomDbContext())
            {
                Profession profession = (from p in context.Professions
                                         where p.ProfessionID == profID
                                         select p).First();

                context.Professions.Remove(profession);
                context.SaveChanges();
            }

            _viewModel.UpdateProfessionsList();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.AddProfession(txtBoxNewCategory.Text);
        }
    }
}
