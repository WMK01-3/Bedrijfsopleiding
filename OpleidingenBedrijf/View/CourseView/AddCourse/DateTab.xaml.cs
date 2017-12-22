using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course.AddCourse;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class DateTab
    {
        private AddCourseView _view;

        #region ViewModel : BaseViewModel

        private DateTabVM _viewModel;
        public DateTabVM ViewModel
        {
            get => _viewModel = _viewModel ?? new DateTabVM(MainVM, this, _view);
            set => _viewModel = value;
        }

        #endregion

        public DateTab(AddCourseView view, MainWindowVM vm) : base(vm)
        {
            _view = view;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex += 1;
        }

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex -= 1;
        }

        private void PreviousWeek_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SetCalendar(-1);
        }

        private void NextWeek_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SetCalendar(1);
        }
        private void ClassRoom_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.CurrentClassRoom = ((TextBox)sender).Text;
        }

        private void MondayGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectDay(0);
        }

        private void TuesdayGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectDay(1);
        }

        private void WednesdayGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectDay(2);
        }

        private void ThursdayGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectDay(3);
        }

        private void FridayGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectDay(4);
        }

        private void SaturdayGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectDay(5);
        }

        private void SundayGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectDay(6);
        }
    }
}
