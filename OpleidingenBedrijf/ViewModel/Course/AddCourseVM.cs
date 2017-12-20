using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using BedrijfsOpleiding.Models;
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
            get => _mainTab = _mainTab ?? new MainTab(_view, MainVM);
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
            get => _locationTab = _locationTab ?? new LocationTab(_view, MainVM);
            set => _locationTab = value;
        }

        #endregion

        private AddCourseView _view;

        public AddCourseVM(MainWindowVM vm, AddCourseView v) : base(vm)
        {
            _view = v;
        }







        public void AddCourse(int locID)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course course = new Models.Course();

                if (_view.CourseId > 0)
                {
                    IQueryable<Models.Course> co = from c in context.Courses
                                                   where c.CourseID == _view.CourseId
                                                   select c;

                    if (co.Any())
                        course = co.First();
                }

                //Everything from the first tab
                course.Title = _mainTab.CourseName.Text;
                course.Description = new TextRange(_mainTab.Description.Document.ContentStart, _mainTab.Description.Document.ContentEnd).Text;
                course.Difficulty = (Models.Course.DifficultyEnum) _mainTab.Difficulty.SelectedItem;
                course.Duration = int.Parse(_mainTab.Duration.Text);
                course.Price = decimal.Parse(_mainTab.Price.Text);

                //Teacher
                course.UserID = _teacherTab.ViewModel.SelectedTeacher.UserID;



                //Dates


                //Location
                course.LocationID = locID;

                context.Messages.Add(new Models.Message { UserID = course.UserID, MessageText = $"U bent toegevoegd als leraar aan {course.Title}", Read = false, Timestamp = DateTime.Now, Title = "Toegevoegd aan cursus"});
                
                context.Courses.AddOrUpdate(course);
                context.SaveChanges();

                MainVM.CurrentView = new CourseOverView(MainVM);


            }
        }

    }
}
