using System.Diagnostics;
using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseInfoView
    {
        public CourseInfoView(int courseId, BaseViewModel parent) : base(parent)
        {
            CourseInfoVM viewModel = new CourseInfoVM(courseId, this);

            DataContext = viewModel;
            OwnViewModel = viewModel;
            InitializeComponent();
        }

        private void BtnEditCourse_OnClick(object sender, RoutedEventArgs e)
        {
            ((MainWindowVM) ParentViewModel).CurrentView = new AddCourseView(ParentViewModel);
        }

        private void BtnDelCourse_OnClick(object sender, RoutedEventArgs e)
        {
            ((CourseInfoVM)OwnViewModel).DeleteCourse();
        }

        /// <summary>
        /// Sign the customer up for the Course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSignUp_OnClick(object sender, RoutedEventArgs e)
        {
            ((CourseInfoVM)OwnViewModel).SignUserUp();
        }

    }
}
