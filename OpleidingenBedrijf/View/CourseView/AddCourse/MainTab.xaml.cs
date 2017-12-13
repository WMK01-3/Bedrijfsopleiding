using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course.AddCourse;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class MainTab
    {
        public AddCourseView View;

        #region ViewModel : BaseViewModel

        private MainTabVM _viewModel;
        public MainTabVM ViewModel
        {
            get => _viewModel = _viewModel ?? new MainTabVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public MainTab(AddCourseView view, MainWindowVM vm) : base(vm)
        {
            View = view;
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
            View.MainVM.CurrentView = new CourseOverView(View.MainVM);
        }

        private void btnChooseTeacher_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CheckData();
        }

        private void Duration_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.Delete || e.Key == Key.Back) == false)
                e.Handled = true;
        }

        private void Duration_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;

            if (text.IsEmpty()) return;

            if (int.Parse(text) > 1440)
                ((TextBox)sender).Text = "1440";
        }
    }
}
