using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course.AddCourse;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class TeacherTab 
    {
        private AddCourseView _view;
        private int _index;

        #region OwnViewModel : BaseViewModel

        private TeacherTabVM _viewModel;
        public TeacherTabVM ViewModel
        {
            get => _viewModel = _viewModel ?? new TeacherTabVM(MainVM);
            set => _viewModel = value;
        }

        #endregion

        public TeacherTab(AddCourseView view, MainWindowVM vm) : base(vm)
        {
            _view = view;
            InitializeComponent();
        }

        private void ListBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Debug.WriteLine("Index = " + _index);

            if (_index >= _viewModel.TeacherInfo.ToArray().Length) return;

            Teacher teachers = _viewModel.TeacherInfo.ToArray()[_index];
            _index++;
            ((ListBox)sender).ItemsSource = teachers.Professions;
        }

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex -= 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex += 2;
        }
    }
}
