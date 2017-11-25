using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseSignUpView
    {
        public CourseSignUpView(Course course, BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            DataContext = new CourseSignUpVM(course, this);
        }

        private void BtnEditCourse_OnClick(object sender, RoutedEventArgs e)
        {
            //((MainWindowVM) ParentViewModel).CurrentView = new AddCourseView();
        }

        private void BtnDelCourse_OnClick(object sender, RoutedEventArgs e)
        {
            ((CourseSignUpVM) OwnViewModel).DeleteCourse();
        }
        
        private void BtnSignUp_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
