using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseView
    {
        public CourseView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            OwnViewModel = new CourseVM(this);

            Courses.ItemsSource = ((CourseVM) OwnViewModel).CourseList;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //((CourseVM) OwnViewModel).MoreInfo();
        }
    }
}