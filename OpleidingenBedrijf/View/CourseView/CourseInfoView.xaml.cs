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
            InitializeComponent();
            DataContext = new CourseInfoVM(courseId, this);
        }

        private void BtnEditCourse_OnClick(object sender, RoutedEventArgs e)
        {
            //((MainWindowVM) ParentViewModel).CurrentView = new AddCourseView();
        }

        private void BtnDelCourse_OnClick(object sender, RoutedEventArgs e)
        {
            ((CourseInfoVM) OwnViewModel).DeleteCourse();
        }
        
        private void BtnSignUp_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
