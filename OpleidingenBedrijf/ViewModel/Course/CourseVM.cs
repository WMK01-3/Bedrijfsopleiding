using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View;
using BedrijfsOpleiding.View.CursusView;



namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseVM : BaseViewModel
    {
        public CourseVM(UserControl boundView) : base(boundView)
        {
        }

        public void Course()
        {
            CursusView courseV = (CursusView)CurrentView;
        }

        public void AddCourse(Models.Course course)
        {
            using (var context = new CustomDbContext())
            {
                Console.WriteLine("Adding Course");

                context.Courses.Add(course);
                context.SaveChanges();
            }
        }
    }
}
