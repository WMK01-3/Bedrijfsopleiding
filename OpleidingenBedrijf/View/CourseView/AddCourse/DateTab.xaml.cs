using System.Windows;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class DateTab
    {
        private AddCourseView _view;

        public DateTab(AddCourseView view, MainWindowVM vm) : base(vm)
        {
            _view = view;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex += 1;
        }
    }
}
