using System;
using BedrijfsOpleiding.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using BedrijfsOpleiding.View;

namespace BedrijfsOpleiding.ViewModel.Course
{
    public class CourseOverViewVM : BaseViewModel
    {
        private List<Models.Course> _courseList;
        public List<Models.Course> CourseList
        {
            get => GetCourseList();
            set
            {
                _courseList = value;
                OnPropertyChanged(nameof(CourseList));
            }
        }

        private List<Models.Course.DifficultyEnum> _cbxDifficultyList;
        public List<Models.Course.DifficultyEnum> CbxDifficultyList
        {
            get => GetDifficultyList();
            set
            {
                _cbxDifficultyList = value;
                OnPropertyChanged(nameof(CbxDifficultyList));
            }
        }
        private readonly User _user;

        private string _nameFilter= "";
        private string _difficultyFilter="";
        private string _locationFilter = "";

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

        public CourseOverViewVM(MainWindowVM vm, UserControl boundView) : base(vm)
        {
            _user = vm.CurUser;
        }
        public void UpdateDataGrid()
        {
            CourseList = GetCourseList();
        }


        public List<Models.Course> GetCourseList()
        {
            var courseList = new List<Models.Course>();
            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<Models.Course> result;
                if (MainVM.CurUser.Role == User.RoleEnum.Employee)
                {
                    result = (from c in context.Courses
                              select c);
                }
                else
                {
                    result = (from c in context.Courses
                              join e in context.Enrollments on c.CourseID equals e.CourseID into y
                              from e in y.DefaultIfEmpty()
                              where (c.Archived == false) && (e.UserID != _user.UserID)
                              select c
                        );
                }

                    
                foreach (Models.Course course in result)
                {
                    course.Location = (from l in context.Locations
                                       where l.LocationID == course.LocationID
                                       select l).First();
                   
                    if (course.Title.Contains(_nameFilter) && course.Difficulty.ToString().Contains(_difficultyFilter) && course.Location.City.ToLower().Contains(_locationFilter.ToLower()))
                    {
                        courseList.Add(course);
                    }
                }
            }
            return courseList;
        }

        public void FilterText(string courseNameFilter, string difficultyFilter, string locationFilter)
        {
            _nameFilter = courseNameFilter;
            _difficultyFilter = difficultyFilter;
            _locationFilter = locationFilter;
            Debug.WriteLine(_nameFilter);
            Debug.WriteLine(CourseList.Count());
            UpdateDataGrid();
        }
    }
}
