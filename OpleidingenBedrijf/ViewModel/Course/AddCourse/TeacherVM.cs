using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class TeacherTabVM : BaseViewModel
    {
        #region Categories : ICollection<CheckBox>

        private ICollection<Category> _categories;
        public ICollection<Category> Categories
        {
            get
            {
                return _categories = _categories ?? new List<Category>
            {
                new Category("wiskunde", true),
                new Category("Natuurkunde", false),
                new Category("Scheikunde", false),
                new Category("Computer", false)
            };
            }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        #endregion

        public ICollection<User> TeacherInfo => GetTeachers();

        public TeacherTabVM(MainWindowVM vm) : base(vm)
        {
        }

        private ICollection<User> GetTeachers()
        {
            ICollection<User> teachers = new List<User>();

            using (CustomDbContext context = new CustomDbContext())
            {
                foreach (Category category in Categories)
                {
                    if (category.IsChecked == false) break;

                    IQueryable<User> teachArray = from c in context.Professions
                                                  where c.ProfessionName == category.Name
                                                  select c.User;

                    foreach (User teach in teachArray)
                    {
                        if (teachers.Contains(teach) == false)
                            teachers.Add(teach);
                    }
                }
            }
            return teachers;
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
