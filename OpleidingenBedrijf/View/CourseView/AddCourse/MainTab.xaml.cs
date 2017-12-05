using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class MainTab
    {
        private AddCourseView _view;

        public MainTab(AddCourseView view)
        {
            _view = view;
            InitializeComponent();
        }

        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsDigit(c) || c.Equals(',') && Price.Text.Contains(",") == false) continue;

                e.Handled = true;
                break;
            }
        }

        private void Difficulty_Loaded(object sender, RoutedEventArgs e)
        {
            List<Course.DifficultyEnum> data = Enum.GetValues(typeof(Course.DifficultyEnum)).Cast<Course.DifficultyEnum>().ToList();

            Difficulty.ItemsSource = data;
            Difficulty.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _view.MainVM.CurrentView = new CourseOverView(_view.MainVM);
        }

        private void btnChooseTeacher_Click(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex += 1;
        }
    }
}
