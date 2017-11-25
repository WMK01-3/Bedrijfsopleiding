using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseSignUpVM : BaseViewModel
    {
        private readonly User _user;
        public Models.Course Course { get; }
        public string CourseDesc => Course.Description;
        public string CourseTitle => Course.Name;
        public string CoursePrice => $"Prijs: €{Course.Price.ToString(CultureInfo.CurrentCulture)} / les";
        public string CourseLessonCount => $"Aantal Lessen: {Course.Dates}";
        public string CourseMinutesPerLesson => $"Minuten per les: {Course.Duration}";
        public string CourseParticipants => $"Aantal deelnemers: {Course.Enrollments}/{Course.MaxParticipants}";
        public string CourseLevel => $"Niveau: {Course.Difficulty}";
        public bool IsEmployee => _user.Role == User.RoleEnum.Employee;
        public bool IsUser => _user.Role == User.RoleEnum.Customer;
        public string UserEmail => _user.Email;

        //public IEnumerable<DateTime> CourseDates => new ObservableCollection<DateTime> { Course.Dates };

        public CourseSignUpVM(Models.Course course, UserControl boundView) : base(boundView)
        {
            CourseSignUpView signup = (CourseSignUpView)boundView;
            MainWindowVM mainWindow = (MainWindowVM)signup.ParentViewModel;
            _user = mainWindow.CurUser;
            Course = course;
        }

        public void DeleteCourse()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course item = (from course in context.Courses
                                      where course.CourseID == Course.CourseID
                                      select course).First();

                context.Courses.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
