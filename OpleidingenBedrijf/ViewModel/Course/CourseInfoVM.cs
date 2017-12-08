using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.CourseView;
using AddCourseView = BedrijfsOpleiding.View.CourseView.AddCourse.AddCourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseInfoVM : BaseViewModel
    {
        private User _user;
        private Location _location; 
        public string courseStatus { get; set; }

        //THIS IS THE CURRENT LOADED COURSE
        public Models.Course Course { get; }
        public string CourseDesc => Course.Description;
        public string CourseTitle => Course.Title;
        public string CoursePrice => $"Prijs: €{Course.Price.ToString(CultureInfo.CurrentCulture)} / les";
        public string CourseLessonCount => $"{Course.Dates} lessen";
        public string CourseMinutesPerLesson => $"Minuten per les: {Course.Duration}";

        public string CourseParticipants =>
            Course.Enrollments == null
                ? "Aantal deelnemers: ONBEKEND"
                : $"Aantal deelnemers: {Course.Enrollments.Count}/{Course.MaxParticipants}";

        public string CourseLevel => $"Niveau: {Course.Difficulty}";

        public bool IsEmployee => _user.Role == User.RoleEnum.Employee;
        public bool IsUser => _user.Role == User.RoleEnum.Customer;
        public bool IsTeacher => _user.Role == User.RoleEnum.Teacher;

        public string UserEmail => _user.Email;
        public string CourseStreet => _location.Street;
        public string CourseCity => $"{_location.City} , {_location.Zipcode}";

        public IEnumerable<DateTime> CourseDates => Course.Dates?.ToList();

        public CourseInfoVM(int courseId, MainWindowVM vm, UserControl boundView) : base(vm)
        {
            _user = vm.CurUser;
            

            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course c = (from course in context.Courses
                                   where course.CourseID == courseId
                                   select course).First();

                courseStatus = c.Archieved ? "Dearchiveer cursus" : "Archiveer cursus";
                Course = new Models.Course
                {
                    CourseID = c.CourseID,
                    CreatedAt = c.CreatedAt,
                    Dates = c.Dates,
                    Description = c.Description,
                    Difficulty = c.Difficulty,
                    Duration = c.Duration,
                    UserID = c.UserID,
                    LocationID = c.LocationID,
                    MaxParticipants = c.MaxParticipants,
                    Price = c.Price,
                    Title = c.Title,
                    Archieved = c.Archieved
                    
                };

                _location = (from location in context.Locations
                             where location.LocationID == Course.LocationID
                             select location).First();
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
                if (course.Archieved)
                {
                    course.Archieved = false;
                }
                else
                {
                    course.Archieved = true;
                }
                
                
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

                    context.Enrollments.Add(new Enrollment(_user.UserID, Course.CourseID));
                    context.SaveChanges();
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
