using System.Windows;
using BedrijfsOpleiding.View.CourseView;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class MenuBar
    {
        public MenuBar(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
        }

        private void BtnDashBoard_Click(object sender, RoutedEventArgs e)
        {
            ParentViewModel.CurrentView = new DashBoardView(ParentViewModel);
        }

        private void BtnCourseOverview_Click(object sender, RoutedEventArgs e)
        {
           // ParentViewModel.CurrentView = new CursusView.CursusView(ParentViewModel);
            ParentViewModel.CurrentView = new AddCourseView(ParentViewModel);
        }

        private void BtnCustomerOverview_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
