using System;
using System.Collections.Generic;
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
    class AddCourseVM : BaseViewModel
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
            AddCourseView av = (AddCourseView) CurrentView;

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


            using (CustomDbContext context = new CustomDbContext())
            {

                Models.Course course = new Models.Course
                {
                    Name = av.CourseName.Text,
                    Difficulty = (Models.Course.DifficultyEnum)av.Difficulty.SelectedItem,
                    MaxParticipants = (int)av.MaxParticipants.Value,
                    Duration = (Models.Course.DurationEnum)av.Duration.SelectedItem,
                    Price = int.Parse(av.Price.Text),
                    Description = new TextRange(av.Description.Document.ContentStart, av.Description.Document.ContentEnd).Text,

                    UserID = short.Parse(av.TeacherID.Text),
                    LocationID = int.Parse(av.LocationID.Text)
                };


                context.Courses.Add(course);
                context.SaveChanges();
            }

        }
    }
}
