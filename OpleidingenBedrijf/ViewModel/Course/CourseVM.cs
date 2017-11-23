using System.Windows.Controls;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseVM : BaseViewModel
    {
        public CourseVM(UserControl boundView) : base(boundView)
        {
        }

        public void AddCourse(Models.Course course)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.Courses.Add(course);
                context.SaveChanges();
            }
        }
    }
}
