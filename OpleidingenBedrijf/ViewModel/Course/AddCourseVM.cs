using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using BedrijfsOpleiding.View.CourseView;
using BedrijfsOpleiding.View.CourseView.AddCourse;
using AddCourseView = BedrijfsOpleiding.View.CourseView.AddCourse.AddCourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class AddCourseVM : BaseViewModel
    {
        #region MainTab : UserControl

        private MainTab _mainTab;
        public MainTab MainTab
        {
            get => _mainTab = _mainTab ?? new MainTab(_view);
            set => _mainTab = value;
        }

        #endregion

        #region DateTab : UserControl

        private DateTab _dateTab;
        public DateTab DateTab
        {
            get => _dateTab = _dateTab ?? new DateTab(_view, MainVM);
            set => _dateTab = value;
        }

        #endregion

        #region TeacherTab : UserControl

        private TeacherTab _teacherTab;
        public TeacherTab TeacherTab
        {
            get => _teacherTab = _teacherTab ?? new TeacherTab(_view, MainVM);
            set => _teacherTab = value;
        }

        #endregion

        #region LocationTab : UserControl

        private LocationTab _locationTab;
        public LocationTab LocationTab
        {
            get => _locationTab = _locationTab ?? new LocationTab();
            set => _locationTab = value;
        }

        #endregion

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

            _mainTab.ecCourseName.Visibility = (_mainTab.CourseName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden);
            //av.ecStartDate.Visibility = (av.StartDate.ToString().Length == 0 ? Visibility.Visible : Visibility.Hidden);
            _mainTab.ecPrice.Visibility = (_mainTab.Price.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden);

            #endregion

            #region ErrorBorders

            _mainTab.CourseName.BorderBrush = _mainTab.CourseName.Text.Length == 0 ? _redBrush : _blueBrush;
            //av.StartDate.BorderBrush = av.StartDate.ToString().Length == 0 ? _redBrush : _blueBrush;
            _mainTab.Price.BorderBrush = _mainTab.Price.Text.Length == 0 ? _redBrush : _blueBrush;

            #endregion

            #region ErrorCount

            _errorCount += _mainTab.CourseName.Text.Length == 0 ? 1 : 0;
            //_errorCount += av.StartDate.ToString().Length == 0 ? 1 : 0;
            _errorCount += _mainTab.Price.Text.Length == 0 ? 1 : 0;
            _errorCount += _mainTab.Price.Text.IsMoney() ? 0 : 1;

            #endregion

            //If everything has some sort of value, continu
            if (_errorCount != 0)
            {
                _mainTab.errorMessage.Visibility = Visibility.Visible;
                return;
            }

            _mainTab.errorMessage.Visibility = Visibility.Hidden;

            Models.Course course = new Models.Course
            {
                Title = _mainTab.CourseName.Text,
                Difficulty = (Models.Course.DifficultyEnum)_mainTab.Difficulty.SelectedItem,
                Duration = int.Parse(_mainTab.Duration.Text),
                Price = decimal.Parse(_mainTab.Price.Text),
                Description = new TextRange(_mainTab.Description.Document.ContentStart,
                    _mainTab.Description.Document.ContentEnd).Text,
                LocationID = 1,
                UserID = 1
            };

            //course.UserID = short.Parse(av.TeacherID.Text);
            //course.LocationID = int.Parse(av.LocationID.Text);
            //course.Dates.Add(av.StartDate.);


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

                oldCourse.Title = _mainTab.CourseName.Text;
                oldCourse.Difficulty = (Models.Course.DifficultyEnum)_mainTab.Difficulty.SelectedItem;
                oldCourse.Duration = int.Parse(_mainTab.Duration.Text);
                oldCourse.Price = decimal.Parse(_mainTab.Price.Text);
                oldCourse.Description =
                    new TextRange(_mainTab.Description.Document.ContentStart, _mainTab.Description.Document.ContentEnd).Text;

                // oldCourse.UserID = short.Parse(av.TeacherID.Text);
                //oldCourse.LocationID = int.Parse(av.TeacherID.Text);

                context.Courses.AddOrUpdate(oldCourse);
                context.SaveChanges();

                MainVM.CurrentView = new CourseOverView(MainVM);
            }
        }
    }
}
