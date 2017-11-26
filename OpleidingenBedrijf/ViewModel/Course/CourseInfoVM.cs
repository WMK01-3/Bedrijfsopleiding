using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseInfoVM : BaseViewModel
    {
        private User _user;
        public Models.Course Course { get; }
        public string CourseDesc => Course.Description;
        public string CourseTitle => Course.Title;
        public string CoursePrice => $"Prijs: €{Course.Price.ToString(CultureInfo.CurrentCulture)} / les";
        public string CourseLessonCount => $"{Course.Dates} lessen";
        public string CourseMinutesPerLesson => $"Minuten per les: {Course.Duration}";

        public string CourseParticipants =>
            Course.Enrollments == null ? "Aantal deelnemers: ONBEKEND" : $"Aantal deelnemers: {Course.Enrollments.Count}/{Course.MaxParticipants}";

        public string CourseLevel => $"Niveau: {Course.Difficulty}";
        public bool IsEmployee => _user.Role == User.RoleEnum.Employee;
        public bool IsUser => _user.Role == User.RoleEnum.Customer;
        public string UserEmail => _user.Email;


        //public IEnumerable<DateTime> CourseDates => new ObservableCollection<DateTime> { Course.Dates };

        public CourseInfoVM(int courseId, UserControl boundView) : base(boundView)
        {
            CourseInfoView signup = (CourseInfoView)boundView;
            MainWindowVM mainWindow = (MainWindowVM)signup.ParentViewModel;
            _user = mainWindow.CurUser;

            ((CourseInfoView) boundView).EmailText.Text = _user.Email;

            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course c = (from course in context.Courses
                                   where course.CourseID == courseId
                                   select course).First();

                Course = new Models.Course
                {
                    CourseID = c.CourseID,
                    Created_at = c.Created_at,
                    Dates = c.Dates,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Duration = c.Duration,
                    Enrollments = c.Enrollments,
                    LocationID = c.LocationID,
                    MaxParticipants = c.MaxParticipants,
                    Price = c.Price,
                    Title = c.Title
                };
            }
        }

        public void DeleteCourse()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                context.Courses.Remove(Course);
                context.SaveChanges();
            }
        }
    }
}
