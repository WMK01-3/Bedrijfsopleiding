using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View
{
    public partial class MenuBar
    {
        public MenuBar(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void BtnDashBoard_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new DashBoardView(MainVM);
        }

        private void BtnCourseOverview_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new CourseView.CourseOverView(MainVM);
        }

        private void BtnCustomerOverview_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new LoginView.LoginView(MainVM);
            MainVM.CurUser = null;
            MainVM.MenuView = null;
        }
    }
}
