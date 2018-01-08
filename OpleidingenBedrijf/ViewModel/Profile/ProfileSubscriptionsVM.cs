using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.ViewModel.Profile
{
    public class ProfileSubscriptionsVM : BaseViewModel
    {
        public List<Models.Course> CourseList => GetCourseList();
        public List<Enrollment> EnrollmentList = new List<Enrollment>();
        public List<EnrolledCourses> EnrolledCourseList => GetEnrolledCourseList(GetCourseList(), EnrollmentList);
        private User _user;
      
        public ProfileSubscriptionsVM(MainWindowVM vm) : base(vm)
        {
            _user = vm.CurUser;
            int userID = _user.UserID;
        }

        private List<Models.Course> GetCourseList()
        {
            var courseList = new List<Models.Course>();
            IQueryable<Models.Course> resultCourse;
            using (CustomDbContext context = new CustomDbContext())
            {
                resultCourse = from course in context.Courses
                         join enrollment in context.Enrollments on course.CourseID equals enrollment.CourseID
                         where enrollment.UserID == _user.UserID && course.Archived == false && enrollment.CourseID == course.CourseID
                         select course;
                
                foreach (var x in resultCourse)
                {
                    EnrollmentList.Add(GetEnrollment(_user.UserID, x.CourseID));
                    courseList.Add(x);
                }
            }
            return courseList;
        }

        private Models.Enrollment GetEnrollment(int userID, int CourseID)
        {
            Models.Enrollment results; 
            using (CustomDbContext context = new CustomDbContext())
            {
                            results = (from enrollment in context.Enrollments
                                                        where enrollment.UserID == userID && enrollment.CourseID == CourseID
                                                        select enrollment).First();
            }
            return results;
        }
        private List<EnrolledCourses> GetEnrolledCourseList(List<Models.Course> list1, List<Models.Enrollment> list2)
        {
            List<EnrolledCourses> returnList = new List<EnrolledCourses>();
            for (int i = 0; i <list1.Count; i++)
            {
                returnList.Add(new EnrolledCourses()
                {
                    Course = list1[i],
                    Enrollments = list2[i]
                });
            }
            return returnList;
        }

        public void WriteOut(int courseID, int userID)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Enrollment> writeOut = (from enrollment in context.Enrollments
                                                   where courseID == enrollment.CourseID && userID == enrollment.UserID
                                                   select enrollment);
                    context.Enrollments.Remove(writeOut.First());
                    context.SaveChanges();
                    
            }
        }
    }
    public class EnrolledCourses
    {
        public Models.Course Course { get; set; }
        public Enrollment Enrollments { get; set; }
    }
}