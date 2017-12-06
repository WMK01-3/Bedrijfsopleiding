using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class AddCourseView
    {
        #region OwnViewModel : BaseViewModel

        private AddCourseVM _viewModel;
        public AddCourseVM ViewModel
        {
            get => _viewModel = _viewModel ?? new AddCourseVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public int CourseId;

        public AddCourseView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
        }

        public AddCourseView(MainWindowVM vm, int id) : this(vm)
        {
            throw new System.NotImplementedException();
        }
    }
}
