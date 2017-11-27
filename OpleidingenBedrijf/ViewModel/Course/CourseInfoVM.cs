using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseInfoVM : BaseViewModel
    {
        private User _user;
        private Location _location;

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
        public bool IsTeacher => _user.Role == User.RoleEnum.Teacher;

        public string UserEmail => _user.Email;
        public string CourseStreet => _location.Street;
        public string CourseCity => $"{_location.City} , {_location.Zipcode}";

        public Visibility IsUserSignedUp
        {
            get
            {
                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<Enrollment> result = (from e in context.Enrollments
                                         where e.UserID == _user.UserID && e.CourseID == Course.CourseID
                                         select e);

                    return result.Any() ? Visibility.Visible : Visibility.Hidden;
                    
                }
            }
        }

        public IEnumerable<DateTime> CourseDates => Course.Dates?.ToList();

        public CourseInfoVM(int courseId, UserControl boundView) : base(boundView)
        {
            CourseInfoView signup = (CourseInfoView)boundView;
            MainWindowVM mainWindow = (MainWindowVM)signup.ParentViewModel;
            _user = mainWindow.CurUser;

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

                _location = (from location in context.Locations
                             where location.LocationID == Course.LocationID
                             select location).First();
            }
        }

        public void DeleteCourse()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course course = (from c in context.Courses
                                        where c.CourseID == Course.CourseID
                                        select c).First();

                context.Courses.Remove(course);
                context.SaveChanges();

                ((MainWindowVM)((CourseInfoView)CurrentView).ParentViewModel).CurrentView = new CourseView((MainWindowVM)((CourseInfoView)CurrentView).ParentViewModel);

            }
        }

        public void SignUserUp()
        {
            
            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course course = (from c in context.Courses
                                        where c.CourseID == Course.CourseID
                                        select c).First();

                course.Enrollments.Add(new Enrollment(_user, course));
                
                context.SaveChanges();
            }
        }
    }
}
