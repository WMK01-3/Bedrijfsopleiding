using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class AddCourseVM : BaseViewModel
    {
        private AddCourseView _view;
        private int _errorCount;
        private readonly SolidColorBrush _redBrush = new SolidColorBrush(Colors.Tomato);
        private readonly SolidColorBrush _blueBrush = new SolidColorBrush(Colors.CornflowerBlue);

        public AddCourseVM(MainWindowVM vm, AddCourseView v) : base(vm)
        {
            _view = v;
        }

        public void AddCourse()
        {
            _errorCount = 0;

            //Checking for empty values
            #region ErrorIcons

            _view.ecCourseName.Visibility = (_view.CourseName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden);
            _view.ecMaxParticipants.Visibility = (_view.MaxParticipants.Value == 0 ? Visibility.Visible : Visibility.Hidden);
            //av.ecStartDate.Visibility = (av.StartDate.ToString().Length == 0 ? Visibility.Visible : Visibility.Hidden);
            _view.ecPrice.Visibility = (_view.Price.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden);

            #endregion

            #region ErrorBorders

            _view.CourseName.BorderBrush = _view.CourseName.Text.Length == 0 ? _redBrush : _blueBrush;
            _view.MaxParticipants.Background = _view.MaxParticipants.Value == 0 ? _redBrush : _blueBrush;
            //av.StartDate.BorderBrush = av.StartDate.ToString().Length == 0 ? _redBrush : _blueBrush;
            _view.Price.BorderBrush = _view.Price.Text.Length == 0 ? _redBrush : _blueBrush;

            #endregion

            #region ErrorCount

            _errorCount += _view.CourseName.Text.Length == 0 ? 1 : 0;
            _errorCount += _view.MaxParticipants.Value == 0 ? 1 : 0;
            //_errorCount += av.StartDate.ToString().Length == 0 ? 1 : 0;
            _errorCount += _view.Price.Text.Length == 0 ? 1 : 0;
            _errorCount += _view.Price.Text.IsMoney() ? 0 : 1;

            #endregion

            //If everything has some sort of value, continu
            if (_errorCount != 0)
            {
                _view.ErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            _view.ErrorMessage.Visibility = Visibility.Hidden;

            Models.Course course = new Models.Course();
            course.Title = _view.CourseName.Text;
            course.Difficulty = (Models.Course.DifficultyEnum)_view.Difficulty.SelectedItem;
            course.MaxParticipants = (int)_view.MaxParticipants.Value;
            course.Duration = (Models.Course.DurationEnum)_view.Duration.SelectedItem;

            course.Price = decimal.Parse(_view.Price.Text);

            course.Description = new TextRange(_view.Description.Document.ContentStart, _view.Description.Document.ContentEnd).Text;
            //course.UserID = short.Parse(av.TeacherID.Text);
            //course.LocationID = int.Parse(av.LocationID.Text);
            //course.Dates.Add(av.StartDate.);

            course.LocationID = 1;
            course.UserID = 1;

            using (CustomDbContext context = new CustomDbContext())
            {
                context.Courses.Add(course);
                context.SaveChanges();

                MainVM.CurrentView = new CourseOverView(MainVM);
            }
        }

        public void SaveCourse()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course oldCourse = (from c in context.Courses
                                           where c.CourseID == _view.CourseId
                                           select c).First();

                oldCourse.Title = _view.CourseName.Text;
                oldCourse.Difficulty = (Models.Course.DifficultyEnum)_view.Difficulty.SelectedItem;
                oldCourse.MaxParticipants = (int)_view.MaxParticipants.Value;
                oldCourse.Duration = (Models.Course.DurationEnum)_view.Duration.SelectedItem;
                oldCourse.Price = decimal.Parse(_view.Price.Text);
                oldCourse.Description =
                    new TextRange(_view.Description.Document.ContentStart, _view.Description.Document.ContentEnd).Text;

                // oldCourse.UserID = short.Parse(av.TeacherID.Text);
                //oldCourse.LocationID = int.Parse(av.TeacherID.Text);

                context.Courses.AddOrUpdate(oldCourse);
                context.SaveChanges();

                MainVM.CurrentView = new CourseOverView(MainVM);
            }
        }
    }
}
