using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course.AddCourse;

namespace BedrijfsOpleiding.View.CourseView.AddCourse
{
    public partial class DateTab
    {
        private AddCourseView _view;
        private int _index;

        #region OwnViewModel : BaseViewModel
        private DateVM _viewModel;
        public DateVM ViewModel
        {
            get => _viewModel = _viewModel ?? new DateVM(MainVM);
            set => _viewModel = value;
        }



        #endregion

        public DateTab(AddCourseView view, MainWindowVM vm) : base(vm)
        {
            _view = view;
            InitializeComponent();
        }
    }
}
