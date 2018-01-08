using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            triangle.Visibility = Visibility.Hidden;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.CheckData();
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
            ViewModel.UpdateCalendar();
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

        private void AddDate_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.AddDate();
        }

        //For deleting an item
        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            string tag = ((FontAwesome.WPF.FontAwesome)sender).Tag.ToString();

            IEnumerable<SelectedInfoClass> item = from i in ViewModel.DateItemList
                                                  where i.ElementIndex == int.Parse(tag)
                                                  select i;

            IEnumerable<SelectedInfoClass> selectedInfoClasses = item as IList<SelectedInfoClass> ?? item.ToList();

            if (selectedInfoClasses.Any())
                ViewModel.DateItemList.Remove(selectedInfoClasses.First());
        }
    }
}
