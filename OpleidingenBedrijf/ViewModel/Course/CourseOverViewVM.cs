using BedrijfsOpleiding.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.View.CourseView;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseOverViewVM : BaseViewModel
    {
        private CourseOverView _view;

        public List<Models.Course> CourseList =>
            GetCourseList();

        public List<Models.Course.DifficultyEnum> CbxDifficultyList =>
            GetDifficultyList();

        public bool IsEmployee =>
            _user.Role == User.RoleEnum.Employee;

        private readonly User _user;

        private string _nameFilter = "";
        private string _difficultyFilter = "";
        private string _locationFilter = "";

        /// <summary>
        /// Gets the difficulty, used in the combobox
        /// </summary>
        /// <returns></returns>
        public List<Models.Course.DifficultyEnum> GetDifficultyList()
        {
            var list = new List<Models.Course.DifficultyEnum>
            {
                Models.Course.DifficultyEnum.Beginner,
                Models.Course.DifficultyEnum.Moderate,
                Models.Course.DifficultyEnum.Expert
            };
            return list;
        }

        public CourseOverViewVM(MainWindowVM vm, CourseOverView view) : base(vm)
        {
            _view = view;
            _user = vm.CurUser;
            OnPropertyChanged(nameof(IsEmployee));
        }
        public void UpdateDataGrid()
        {
            OnPropertyChanged(nameof(CourseList));
        }

        public List<Models.Course> GetCourseList()
        {
            var courseList = new List<Models.Course>();
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Models.Course> result;
                if (MainVM.CurUser.Role == User.RoleEnum.Employee)
                {
                    result = from c in context.Courses
                             orderby c.Archived
                             select c;
                }
                else
                {
                    //Gets a filtered list of courses
                    result = from c in context.Courses
                             join e in context.Enrollments on c.CourseID equals e.CourseID into y
                             from e in y.DefaultIfEmpty()
                             where c.Archived == false && e.UserID != _user.UserID && c.Enrollments.Count < c.MaxParticipants
                             select c;
                }

                foreach (Models.Course course in result)
                {
                    string locationString = $"{course.Location.Street}, {course.Location.City}";

                    if (course.Title.Contains(_nameFilter) == false) continue;
                    if (course.Difficulty.ToString().Contains(_difficultyFilter) == false) continue;
                    if (locationString.Contains(_locationFilter) == false) continue;

                    courseList.Add(course);
                }
            }
            return courseList;
        }

        public void FilterText()
        {
            _nameFilter = _view.TxtCourseName.Text;
            if (_view.CbxDifficulty.SelectedValue != null)
                _difficultyFilter = _view.CbxDifficulty.SelectedValue.ToString();

            _locationFilter = _view.TxtLocation.Text;
            UpdateDataGrid();
        }
    }
}
