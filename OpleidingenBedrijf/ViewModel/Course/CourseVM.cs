using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseVM : BaseViewModel
    {
        public CourseVM(UserControl boundView) : base(boundView)
        {
        }

        public void Info()
        {
            CourseView CourseV = (CourseView)CurrentView;
            CourseV.CourseInfo.Text = "Test";
        }
    }
}
