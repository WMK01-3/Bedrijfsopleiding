using System.Diagnostics;
using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseOverView
    {
        public CourseOverView(BaseViewModel parent) : base(parent)
        {
            OwnViewModel = new CourseOverViewVM(this);
            InitializeComponent();

            courses.ItemsSource = ((CourseOverViewVM)OwnViewModel).CourseList;
            if (((MainWindowVM)ParentViewModel).IsEmployee == false)
            {
                btnAddCourse.Height = 0;
                btnAddCourse.Visibility = Visibility.Hidden;
                Thickness thickness = btnAddCourse.Margin;
                thickness.Top = 0;
            }
        }

        private void ToCourseInfo(object sender, RoutedEventArgs e)
        {
            if (courses.SelectedItem == null) return;

            if (courses.SelectedItem is Course == false) return;

            ParentViewModel.CurrentView =
                new CourseInfoView(((Course)courses.SelectedItem).CourseID, ParentViewModel);
        }

        private void BtnAddCourse_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}