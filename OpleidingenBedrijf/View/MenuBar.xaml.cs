using System.Windows;

using BedrijfsOpleiding.Models;

using BedrijfsOpleiding.View.CourseView;

using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

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

            // ParentViewModel.CurrentView = new CursusView.CursusView(ParentViewModel);
            ParentViewModel.CurrentView = new CourseView.CourseView(ParentViewModel);

        }

        private void BtnCustomerOverview_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            ParentViewModel.CurrentView = new LoginView.LoginView(ParentViewModel);
            ((MainWindowVM) ParentViewModel).CurUser = null;
            ((MainWindowVM) ParentViewModel).MenuView = null;
        }
    }
}
