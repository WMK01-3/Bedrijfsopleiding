using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseOverViewVM : BaseViewModel
    {
        public ObservableCollection<Models.Course> CourseList => GetCourseList();

        public CourseOverViewVM(MainWindowVM vm, UserControl boundView) : base(vm)
        {

        }

        public ObservableCollection<Models.Course> GetCourseList()
        {
            var courseList = new ObservableCollection<Models.Course>();

            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Models.Course> result = from c in context.Courses
                                                   select c;

                foreach (Models.Course course in result)
                {
                    courseList.Add(course);
                }
            }
            return courseList;
        }
    }
}
