using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseSignUpView
    {
        public User currentUser;

        public CourseSignUpView(Course course, BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            currentUser = ((MainWindowVM) parent).CurUser;
            DataContext = new CourseSignUpVM(course, this);
        }
    }
}
