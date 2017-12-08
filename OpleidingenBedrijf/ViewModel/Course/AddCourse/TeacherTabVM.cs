using System.Collections.Generic;
using System.Windows.Controls;
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
            set => _categories = value;
        }

        #endregion

        public ICollection<Teacher> TeacherInfo =>
            new List<Teacher>
        {
            new Teacher{Email = "teacher@teach.com", FirstName = "Dirk", LastName = "Docent", Professions = new List<string>{"Wiskunde", "Natuurkunde"}}
        };
        
        public TeacherTabVM(MainWindowVM vm) : base(vm)
        {
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
