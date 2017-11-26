using System.Windows;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class MenuBar
    {
        public MenuBar(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            DataContext = (MainWindowVM) parent;
        }

        private void BtnDashBoard_Click(object sender, RoutedEventArgs e)
        {
            ParentViewModel.CurrentView = new DashBoardView(ParentViewModel);
        }

        private void BtnCourseOverview_Click(object sender, RoutedEventArgs e)
        {
            ParentViewModel.CurrentView = new CourseView.CourseView(ParentViewModel);
        }

        private void BtnCustomerOverview_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
