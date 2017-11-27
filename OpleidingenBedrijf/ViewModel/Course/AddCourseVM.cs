using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    class AddCourseVM : BaseViewModel
    {
        protected AddCourseVM(UserControl boundView) : base(boundView)
        {

        }

        public void AddCourse()
        {
            AddCourseView addCourseView = (AddCourseView) CurrentView;
            

        }
    }
}
