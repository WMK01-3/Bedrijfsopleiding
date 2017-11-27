using System.Windows.Controls;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View.CourseView
{
    internal class AddCourseView : UserControl
    {
        private BaseViewModel parentViewModel;

        public AddCourseView(BaseViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;
        }
    }
}