using System.Windows;
using System;
using System.Diagnostics;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseOverView
    {
        #region OwnViewModel : BaseViewModel

        private CourseOverViewVM _viewModel;
        public CourseOverViewVM ViewModel
        {
            get => _viewModel = _viewModel ?? new CourseOverViewVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public CourseOverView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
            //courses.ItemsSource = _viewModel.CourseList;
            cbxDifficulty.ItemsSource = Enum.GetValues(typeof(Course.DifficultyEnum));
        }

        private void ToCourseInfo(object sender, RoutedEventArgs e)
        {
            if (courses.SelectedItem == null) return;

            if (courses.SelectedItem is Course == false) return;


            MainVM.CurrentView =
                new CourseInfoView(((Course)courses.SelectedItem).CourseID, MainVM);
        }

        private void BtnAddCourse_OnClick(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new AddCourse.AddCourseView(MainVM);
            MainVM.CurrentView = new AddCourse.AddCourseView(MainVM);
        }

        private void SearchCourses(object sender, RoutedEventArgs e)
        {
            _viewModel.FilterText(TxtCourseName.Text, cbxDifficulty.Text, TxtLocation.Text);
            cbxDifficulty.SelectedIndex = -1;
        }
    }
}