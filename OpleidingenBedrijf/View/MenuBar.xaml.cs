using System.Windows;
using BedrijfsOpleiding.ViewModel;
using ProfileView = BedrijfsOpleiding.View.Profile.ProfileView;

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
            MainVM.CurrentView = new CustomerView(MainVM);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new LoginView.LoginView(MainVM);
            MainVM.CurUser = null;
            MainVM.MenuView = null;
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new ProfileView(MainVM);
            //MainVM.CurrentView = new MessageView.MessageView(MainVM);
        }
    }
}
