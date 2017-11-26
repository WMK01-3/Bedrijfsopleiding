using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseView
    {
        public CourseView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            OwnViewModel = new CourseOverViewVM(this);

            courses.ItemsSource = ((CourseOverViewVM)OwnViewModel).CourseList;
        }

        private void ToCourseInfo(object sender, System.Windows.RoutedEventArgs e)
        {

            ParentViewModel.CurrentView = new CourseInfoView(((Course)courses.SelectedItem).CourseID, ParentViewModel);
        }
    }
}