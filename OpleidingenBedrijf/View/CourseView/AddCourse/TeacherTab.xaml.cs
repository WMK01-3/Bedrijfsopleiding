using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course.AddCourse;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class TeacherTab
    {
        private AddCourseView _view;

        private List<ListBox> _professionList;
        private List<Label> _fullnameList;

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
            _professionList = new List<ListBox>();
            _fullnameList = new List<Label>();
            InitializeComponent();

            UpdateDataGrid();
        }

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            _view.tabControl.SelectedIndex -= 1;
        }

        private void UpdateDataGrid()
        {
            ICollection<User> teachers = _viewModel.TeacherInfo;

            foreach (User teacher in teachers)
            {
                var data = new DataGridItem();
                
                data.FullName = teacher.FullName;

                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<string> professions = from p in context.Professions
                                                     where p.UserID == teacher.UserID
                                                     select p.ProfessionName;

                    data.Professions = professions.ToArray();
                }

                teacherGrid.Items.Add(data);
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(((Label)sender).Content);
        }
    }

    public class DataGridItem
    {
        public string FullName { get; set; }
        public string[] Professions { get; set; }
    }
}
