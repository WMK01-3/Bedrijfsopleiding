using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.Tools;
using BedrijfsOpleiding.View.CourseView;
using AddCourseView = BedrijfsOpleiding.View.CourseView.AddCourse.AddCourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseInfoVM : BaseViewModel
    {
        private readonly User _user;
        private readonly Location _location;
        public string CourseStatus { get; }

        //THIS IS THE CURRENT LOADED COURSE
        public Models.Course Course { get; }
        public string CourseDesc => Course.Description;
        public string CourseTitle => Course.Title;
        public string CoursePrice => $"Prijs: €{Course.Price.ToString(CultureInfo.CurrentCulture)} / les";
        public string CourseLessonCount => $"{GetCourseDates().Count()} lessen";
        public string CourseMinutesPerLesson => $"Minuten per les: {Course.Duration}";

        public string CourseParticipants =>
            Course.Enrollments == null
                ? $"Aantal deelnemers: 0/{Course.MaxParticipants}"
                : $"Aantal deelnemers: {Course.Enrollments.Count}/{Course.MaxParticipants}";

        public string CourseLevel => $"Niveau: {Course.Difficulty}";

        public bool IsEmployee => _user.Role == User.RoleEnum.Employee;
        public bool IsUser => _user.Role == User.RoleEnum.Customer;
        public bool IsTeacher => _user.Role == User.RoleEnum.Teacher;

        public string UserEmail => _user.Email;
        public string CourseStreet => _location.Street;
        public string CourseCity => $"{_location.City} , {_location.Country}";
        public string CourseClassRoom => GetClassRoom();

        public IEnumerable<string> CourseDates => GetCourseDatesStrings();

        public CourseInfoVM(int courseId, MainWindowVM vm) : base(vm)
        {
            _user = vm.CurUser;


            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course c = (from course in context.Courses
                                   where course.CourseID == courseId
                                   select course).First();

                CourseStatus = c.Archived ? "Dearchiveer cursus" : "Archiveer cursus";
                Course = new Models.Course
                {
                    CourseID = c.CourseID,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Duration = c.Duration,
                    UserID = c.UserID,
                    LocationID = c.LocationID,
                    MaxParticipants = c.MaxParticipants,
                    Price = c.Price,
                    Title = c.Title,
                    Archived = c.Archived,
                    Enrollments = c.Enrollments
                };

                _location = (from location in context.Locations
                             where location.LocationID == Course.LocationID
                             select location).First();
            }
        }

        private IEnumerable<DateTime> GetCourseDates()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<DateTime> dateList = from d in context.CourseDates
                                                where d.CourseID == Course.CourseID
                                                select d.Date;

                return dateList.Any() ? dateList.ToList() : new List<DateTime>();
            }
        }

        private IEnumerable<string> GetCourseDatesStrings() =>
         GetCourseDates().Select(dateTime => dateTime.ToString(CultureInfo.CurrentCulture)).ToList();

        private string GetClassRoom()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<string> classroom = from d in context.CourseDates
                                               where d.CourseID == Course.CourseID
                                               select d.ClassRoom;

                return classroom.Any() ? classroom.ToString() : "";
            }
        }

        internal void EditCourse()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                int id = (from course in context.Courses
                          where course.CourseID == Course.CourseID
                          select course.CourseID).First();

                MainVM.CurrentView = new AddCourseView(MainVM, id);
            }
        }

        public void DeleteCourse()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course course = (from c in context.Courses
                                        where c.CourseID == Course.CourseID
                                        select c).First();
                course.Archived = !course.Archived;

                context.SaveChanges();
                MainVM.CurrentView = new CourseOverView(MainVM);
            }
        }

        public bool IsUserSignedUp(bool ischeck)
        {
            if (!ischeck)
            {
                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<Enrollment> result = from e in context.Enrollments
                                                    where e.UserID == _user.UserID && e.CourseID == Course.CourseID
                                                    select e;
                    if (result.Any()) return true;
                    context.Enrollments.Add(new Enrollment(_user.UserID, Course.CourseID, false));
                    context.SaveChanges();
                    int crsID = Course.CourseID;
                    IQueryable<Models.Course> crsList = from c in context.Courses
                                                        where c.CourseID == crsID
                                                        select c;

                    Models.Course course = crsList.First();

                    Invoice invoice = new Invoice(DateTime.Now, _user);
                    invoice.Add(course);

                    string pdf = GenerateInvoice.NewPdf(invoice);
                    if (pdf != "noFile")
                    {
                        GenerateInvoice.mailInvoice(pdf, invoice, _user.Email);
                    }

                    return false;
                }
            }
            using (CustomDbContext context = new CustomDbContext())
            {
                return (from e in context.Enrollments
                        where e.UserID == _user.UserID && e.CourseID == Course.CourseID
                        select e).Any();
            }
        }
    }
}
