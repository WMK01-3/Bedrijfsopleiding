using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseSignUpVM : BaseViewModel
    {
        public Models.Course Course { get; }
        public string CourseDesc => Course.Description;
        public string CourseTitle => Course.Name;
        public string CoursePrice => $"Prijs: €{Course.Price.ToString(CultureInfo.CurrentCulture)} / les";
        public string CourseLessonCount => $"Aantal Lessen: {Course.Dates}";
        public string CourseMinutesPerLesson => $"Minuten per les: {Course.Duration}";
        public string CourseParticipants => $"Aantal deelnemers: {Course.Enrollments}/{Course.MaxParticipants}";
        public string CourseLevel => $"Niveau: {Course.Difficulty}";


        #region UserEmail : string

        private string _userEmail;
        public string UserEmail
        {
            get => _userEmail;
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }

        #endregion

        //public IEnumerable<DateTime> CourseDates => new ObservableCollection<DateTime> { Course.Dates };

        public CourseSignUpVM(Models.Course course, UserControl boundView) : base(boundView)
        {
            UserEmail = ((CourseSignUpView)boundView).currentUser.Email;
            Course = course;
        }
    }
}
