using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class AddCourseVM : BaseViewModel
    {
        private int _errorCount;
        private readonly SolidColorBrush _redBrush = new SolidColorBrush(Colors.Tomato);
        private readonly SolidColorBrush _blueBrush = new SolidColorBrush(Colors.CornflowerBlue);

        public AddCourseVM(UserControl boundView) : base(boundView)
        {

        }

        public void AddCourse()
        {
            _errorCount = 0;
            AddCourseView av = (AddCourseView)CurrentView;

            #region ErrorIcons
            av.ecCourseName.Visibility = (av.CourseName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden);
            av.ecMaxParticipants.Visibility = (av.MaxParticipants.Value == 0 ? Visibility.Visible : Visibility.Hidden);
            av.ecStartDate.Visibility = (av.StartDate.ToString().Length == 0 ? Visibility.Visible : Visibility.Hidden);
            av.ecPrice.Visibility = (av.Price.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden);

            #endregion

            #region ErrorBorders

            av.CourseName.BorderBrush = av.CourseName.Text.Length == 0 ? _redBrush : _blueBrush;
            av.MaxParticipants.Background = av.MaxParticipants.Value == 0 ? _redBrush : _blueBrush;
            av.StartDate.BorderBrush = av.StartDate.ToString().Length == 0 ? _redBrush : _blueBrush;
            av.Price.BorderBrush = av.Price.Text.Length == 0 ? _redBrush : _blueBrush;

            #endregion

            #region ErrorCount

            _errorCount += av.CourseName.Text.Length == 0 ? 1 : 0;
            _errorCount += av.MaxParticipants.Value == 0 ? 1 : 0;
            _errorCount += av.StartDate.ToString().Length == 0 ? 1 : 0;
            _errorCount += av.Price.Text.Length == 0 ? 1 : 0;

            #endregion

            if (_errorCount != 0)
            {
                av.ErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            av.ErrorMessage.Visibility = Visibility.Hidden;

            Models.Course course = new Models.Course();
            course.Title = av.CourseName.Text;
            course.Difficulty = (Models.Course.DifficultyEnum)av.Difficulty.SelectedItem;
            course.MaxParticipants = (int)av.MaxParticipants.Value;
            course.Duration = (Models.Course.DurationEnum)av.Duration.SelectedItem;
            course.Price = int.Parse(av.Price.Text);
            course.Description = new TextRange(av.Description.Document.ContentStart, av.Description.Document.ContentEnd).Text;
            course.UserID = short.Parse(av.TeacherID.Text);
            course.LocationID = int.Parse(av.LocationID.Text);
            //course.Dates.Add(av.StartDate.);

            using (CustomDbContext context = new CustomDbContext())
            {
                context.Courses.Add(course);
                context.SaveChanges();
            }
        }

        public void SaveCourse()
        {
            AddCourseView av = (AddCourseView)CurrentView;

            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course oldCourse = (from c in context.Courses
                                           where c.CourseID == av.CourseId
                                           select c).First();

                oldCourse.Title = av.CourseName.Text;
                oldCourse.Difficulty = (Models.Course.DifficultyEnum)av.Difficulty.SelectedItem;
                oldCourse.MaxParticipants = (int)av.MaxParticipants.Value;
                oldCourse.Duration = (Models.Course.DurationEnum)av.Duration.SelectedItem;
                oldCourse.Price = decimal.Parse(av.Price.Text);
                oldCourse.Description =
                    new TextRange(av.Description.Document.ContentStart, av.Description.Document.ContentEnd).Text;


                // oldCourse.UserID = short.Parse(av.TeacherID.Text);
                //oldCourse.LocationID = int.Parse(av.TeacherID.Text);

                context.Courses.AddOrUpdate(oldCourse);
                context.SaveChanges();
            }
        }
    }
}
