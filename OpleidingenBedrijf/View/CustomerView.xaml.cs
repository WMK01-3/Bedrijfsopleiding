using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.Annotations;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class CustomerView
    {
        #region ViewModel : BaseViewModel

        private CustomerVM _viewModel;
        public CustomerVM ViewModel
        {
            get => _viewModel = _viewModel ?? new CustomerVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public CustomerView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();

            //courses.ItemsSource = _viewModel.CourseList;

            var str = new List<string> { "" };
            Array enums = Enum.GetValues(typeof(User.RoleEnum));

            foreach (object enumItem in enums)
                str.Add(enumItem.ToString());

            CbxRole.ItemsSource = str;
            
            //Set the Datagrid for the first time
            ViewModel.UpdateDataGrid();
        }

        private void BtnSaveEdits_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveEdits();
        }

        private void RoleComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.InfoChanged = ViewModel.IsInfoDifferent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CustomerGrid.SelectedItem == null) return;
            if (CustomerGrid.SelectedItem is CustomerDataGridItem == false) return;
            _viewModel.Block(((CustomerDataGridItem)CustomerGrid.SelectedItem).UserID);
            MainVM.CurrentView = new CustomerView(MainVM);
        }

        private void TxtUserFullName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.FilterText();
        }

        private void CbxRole_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.FilterText();
        }
    }

    public class CustomerDataGridItem : INotifyPropertyChanged
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public User.RoleEnum OriginalRole { get; set; }
        public string Blocked { get; set; }

        private User.RoleEnum _currentRole;
        public User.RoleEnum CurrentRole
        {
            get => _currentRole;
            set
            {
                _currentRole = value;
                OnPropertyChanged(nameof(CurrentRole));
            }
        }

        public List<User.RoleEnum> Roles => Enum.GetValues(typeof(User.RoleEnum)).Cast<User.RoleEnum>().ToList();

        public CustomerDataGridItem(int userID)
        {
            UserID = userID;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
