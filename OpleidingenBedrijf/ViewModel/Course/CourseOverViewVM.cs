using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseOverViewVM : BaseViewModel
    {
        public List<Models.Course> CourseList => GetCourseList();

        public CourseOverViewVM(UserControl boundView) : base(boundView)
        {

        }

        public List<Models.Course> GetCourseList()
        {
            var courseList = new List<Models.Course>();

            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Models.Course> result = (from c in context.Courses
                                                    select c);

                foreach (Models.Course course in result)
                {
                    courseList.Add(course);
                }
            }
            return courseList;
        }
    }
}
