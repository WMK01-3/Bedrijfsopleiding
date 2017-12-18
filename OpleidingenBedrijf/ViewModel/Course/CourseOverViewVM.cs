using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseOverViewVM : BaseViewModel
    {
        public List<Models.Course> CourseList => GetCourseList();

        public CourseOverViewVM(MainWindowVM vm, UserControl boundView) : base(vm)
        {   

        }

        public List<Models.Course> GetCourseList()
        {
            var courseList = new List<Models.Course>();

            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Models.Course> result;
                if (MainVM.CurUser.Role == Models.User.RoleEnum.Employee)
                {
                     result = (from c in context.Courses
                               select c);
                }
                else
                {
                     result = (from c in context.Courses
                               where c.Archived == false
                               select c);
                }

                    
                foreach (Models.Course course in result)
                {
                    course.Location = (from l in context.Locations
                        where l.LocationID == course.LocationID
                        select l).First();
                    courseList.Add(course);

                }
            }
            return courseList;
        }
    }
}
