using System.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
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

            var str = new List<string> {""};
            Array enums = Enum.GetValues(typeof(Course.DifficultyEnum));

            foreach (object enumItem in enums)
                str.Add(enumItem.ToString());

            CbxDifficulty.ItemsSource = str;
        }

        private void ToCourseInfo(object sender, RoutedEventArgs e)
        {
            if (Courses.SelectedItem == null) return;

            if (Courses.SelectedItem is Course == false) return;


            MainVM.CurrentView =
                new CourseInfoView(((Course)Courses.SelectedItem).CourseID, MainVM);
        }

        private void BtnAddCourse_OnClick(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new AddCourse.AddCourseView(MainVM);
            MainVM.CurrentView = new AddCourse.AddCourseView(MainVM);
        }

        private void TxtCourseName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.FilterText();
        }

        private void TxtLocation_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.FilterText();
        }

        private void CbxDifficulty_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.FilterText();
        }
    }
}