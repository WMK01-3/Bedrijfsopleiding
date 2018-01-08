using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.CourseView.AddCourse;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class TeacherTabVM : BaseViewModel
    {
        private AddCourseView _view;

        private List<Category> _categories;
        public List<Category> Categories => _categories = _categories ?? GetTeacherProfessions();

        #region SelectedTeacher : DataGridItem

        private DataGridItem _selectedTeacher;
        public DataGridItem SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                if (_selectedTeacher != null)
                    _selectedTeacher.IsSelected = false;

                _selectedTeacher = value;
                if (_selectedTeacher != null)
                    _selectedTeacher.IsSelected = true;
                OnPropertyChanged(nameof(SelectedTeacher));
            }
        }

        #endregion

        #region IsNoTeacherSelected : bool

        private bool _isNoTeacherSelected;
        public bool IsNoTeacherSelected
        {
            get => _isNoTeacherSelected;
            set
            {
                _isNoTeacherSelected = value;
                OnPropertyChanged(nameof(IsNoTeacherSelected));
            }
        }

        #endregion

        public TeacherTabVM(MainWindowVM vm, AddCourseView view) : base(vm)
        {
            _view = view;
        }

        public List<User> GetTeachers()
        {
            var teachers = new List<User>();

            using (CustomDbContext context = new CustomDbContext())
            {
                int areCatsSelected = Categories.Count(category => category.IsChecked);

                if (areCatsSelected == 0)
                {
                    teachers = (from t in context.Users
                                where t.Role == User.RoleEnum.Teacher
                                select t).ToList();
                }
                else
                {
                    foreach (Category category in Categories)
                    {
                        if (!category.IsChecked) continue;

                        IQueryable<User> teachArray = from c in context.Professions
                                                      where c.ProfessionName == category.Name && c.User.Role == User.RoleEnum.Teacher
                                                      select c.User;

                        foreach (User teach in teachArray)
                        {
                            if (teachers.Contains(teach) == false)
                                teachers.Add(teach);
                        }
                    }
                }
            }
            return teachers;
        }

        public List<Category> GetTeacherProfessions()
        {
            var professionList = new List<Category>();

            using (CustomDbContext context = new CustomDbContext())
            {
                IQueryable<string> profs = (from p in context.Professions
                                            select p.ProfessionName).Distinct();

                foreach (string prof in profs)
                    professionList.Add(new Category(prof, false));
            }

            return professionList;
        }

        public void CheckData()
        {
            if (SelectedTeacher != null)
            {
                IsNoTeacherSelected = false;
                _view.tabControl.SelectedIndex += 1;
            }
            else
                IsNoTeacherSelected = true;
        }
    }

    public class Category
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public Category(string name, bool isChecked)
        {
            Name = name;
            IsChecked = isChecked;
        }
    }
}
