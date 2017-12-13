using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;
using BedrijfsOpleiding.ViewModel.Course.AddCourse;
using BedrijfsOpleiding.API.GoogleMaps;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    /// <summary>
    /// Interaction logic for LocationTab.xaml
    /// </summary>
    public partial class LocationTab
    {
        private AddCourseView _view;
        #region OwnViewModel : BaseViewModel


        
        private LocationTabVM _viewModel;
        public LocationTabVM ViewModel
        {
            get => _viewModel = _viewModel ?? new LocationTabVM(MainVM);
            set => _viewModel = value;
        }


        #endregion
        public LocationTab(AddCourseView view, MainWindowVM vm) : base(vm)
        {
            _view = view;
            InitializeComponent();
            
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex -= 2;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int locID = (cboChooseLocation.SelectedValue.ToString() == "Nieuwe locatie toevoegen") ? ViewModel.AddLocation(tbCity.Text, tbClassroom.Text) : ViewModel.GetLocation(cboChooseLocation.SelectedValue.ToString()); ;
            if (locID > 0)
            {
                _view.ViewModel.AddCourse(locID);
            } 
            
        }
    }
}
