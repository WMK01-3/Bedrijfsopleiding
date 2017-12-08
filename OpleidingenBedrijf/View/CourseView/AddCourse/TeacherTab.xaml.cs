using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course.AddCourse;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class TeacherTab
    {
        private AddCourseView _view;

        #region OwnViewModel : BaseViewModel

        private TeacherTabVM _viewModel;
        public TeacherTabVM ViewModel
        {
            get => _viewModel = _viewModel ?? new TeacherTabVM(MainVM);
            set => _viewModel = value;
        }

        #endregion
        
        public TeacherTab(AddCourseView view, MainWindowVM vm) : base(vm)
        {
            _view = view;
            InitializeComponent();
            UpdateDataGrid();
        }

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex -= 1;
        }

        public void UpdateDataGrid()
        {
            List<User> teachers = _viewModel.GetTeachers();
            var items = new List<DataGridItem>();

            foreach (User teacher in teachers)
            {
                DataGridItem data = new DataGridItem { FullName = teacher.FullName };

                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<string> professions = from p in context.Professions
                                                     where p.UserID == teacher.UserID
                                                     select p.ProfessionName;

                    data.Professions = professions.ToArray();
                }
                items.Add(data);
            }

            teacherGrid.ItemsSource = items;
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnChooseLocation_Click(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex += 2;
        }
    }

    public class DataGridItem
    {
        public string FullName { get; set; }
        public string[] Professions { get; set; }
        public bool IsSelected { get; set; }
    }
}
