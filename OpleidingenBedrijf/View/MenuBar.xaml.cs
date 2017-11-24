using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
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
            ParentViewModel.CurrentView = new CourseView.CourseView(ParentViewModel);
        }

        private void BtnCustomerOverview_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
