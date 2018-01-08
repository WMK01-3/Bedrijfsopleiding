using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Windows.Documents;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.CourseView;
using BedrijfsOpleiding.View.CourseView.AddCourse;
using BedrijfsOpleiding.ViewModel.Course.AddCourse;

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

        private readonly AddCourseView _view;

        public AddCourseVM(MainWindowVM vm, AddCourseView v) : base(vm)
        {
            _view = v;
        }







        public void AddCourse()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                Models.Course course = new Models.Course();

                //For editing a course
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
                course.Difficulty = (Models.Course.DifficultyEnum)_mainTab.Difficulty.SelectedItem;
                course.Duration = int.Parse(_mainTab.Duration.Text);
                course.Price = decimal.Parse(_mainTab.Price.Text);
                course.MaxParticipants = int.Parse(_mainTab.MaxParticipants.Text);

                //Teacher
                course.UserID = _teacherTab.ViewModel.SelectedTeacher.UserID;

                //Dates
                foreach (SelectedInfoClass dateItem in _dateTab.ViewModel.DateItemList)
                    context.CourseDates.Add(new CourseDate { CourseID = course.CourseID, Date = dateItem.Date, ClassRoom = dateItem.ClassRoom });

                //Location
                int locID = _locationTab.cboChooseLocation.SelectedValue.ToString() == "Nieuwe locatie toevoegen" ? _locationTab.ViewModel.AddLocation(_locationTab.tbCity.Text) : _locationTab.ViewModel.GetLocation(_locationTab.cboChooseLocation.SelectedValue.ToString());
                course.LocationID = locID;

                //Send message to the teacher
                context.Messages.Add(new Models.Message { UserID = course.UserID, MessageText = $"U bent toegevoegd als leraar aan {course.Title}", Read = false, Timestamp = DateTime.Now, Title = "Toegevoegd aan cursus" });

                context.Courses.AddOrUpdate(course);
                context.SaveChanges();

                MainVM.CurrentView = new CourseOverView(MainVM);
            }
        }

        /// <summary>
        /// Fills the tabs in when a course needs to be edited
        /// </summary>
        public void FillTabsIn()
        {
            using (CustomDbContext context = new CustomDbContext())
            {

                IQueryable<Models.Course> co = from c in context.Courses
                                               where c.CourseID == _view.CourseId
                                               select c;

                Models.Course course = co.First();

                //First Tab
                _mainTab.CourseName.Text = course.Title;

                _mainTab.Description.Document.Blocks.Clear();
                _mainTab.Description.Document.Blocks.Add(new Paragraph(new Run(course.Description)));
                _mainTab.Difficulty.SelectedItem = course.Difficulty;
                _mainTab.Duration.Text = course.Duration.ToString();
                _mainTab.Price.Text = course.Price.ToString(CultureInfo.CurrentCulture);
                _mainTab.MaxParticipants.Text = course.MaxParticipants.ToString();

                //Teacher
                var dataGridItems = (List<DataGridItem>)_teacherTab.teacherGrid.ItemsSource;

                IEnumerable<DataGridItem> teacherItem = from t in dataGridItems
                                                        where t.UserID == course.UserID
                                                        select t;

                IEnumerable<DataGridItem> gridItems = teacherItem as DataGridItem[] ?? teacherItem.ToArray();
                if (gridItems.Any())
                    _teacherTab.ViewModel.SelectedTeacher = gridItems.First();

                //Dates
                IQueryable<CourseDate> dates = from d in context.CourseDates
                                               where d.CourseID == course.CourseID
                                               select d;

                var index = 0;
                foreach (CourseDate date in dates)
                {
                    _dateTab.ViewModel.DateItemList.Add(new SelectedInfoClass(index) { ClassRoom = date.ClassRoom, Date = date.Date });
                    index++;
                }

                //Location
                var stringArray = new string[_locationTab.cboChooseLocation.Items.Count];

                for (var i = 0; i < _locationTab.cboChooseLocation.Items.Count; i++) stringArray[i] = _locationTab.cboChooseLocation.Items[i].ToString();

                IEnumerable<string> locationList = from i in stringArray
                                                   where i == $"{course.Location.Street},{course.Location.City},{course.Location.Country}"
                                                   select i;

                IEnumerable<string> enumerable = locationList as string[] ?? locationList.ToArray();
                _locationTab.cboChooseLocation.SelectedValue = enumerable.Any() ? enumerable.First() : null;

                MainVM.CurrentView = new CourseOverView(MainVM);
            }
        }
    }
}
