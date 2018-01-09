using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Profile;
using BedrijfsOpleiding.View.CourseView;
using System.Diagnostics;
using System.Windows;

namespace BedrijfsOpleiding.View.Profile
{
    /// <summary>
    /// Interaction logic for ProfileSubscriptions.xaml
    /// </summary>
    public partial class ProfileSubscriptions
    {
        #region ViewModel : BaseViewModel

        private ProfileSubscriptionsVM _viewModel;
        public ProfileSubscriptionsVM ViewModel
        {
            get => _viewModel = _viewModel ?? new ProfileSubscriptionsVM(MainVM);
            set => _viewModel = value;
        }

        #endregion
        public ProfileSubscriptions(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
        }

        private void ToCourseInfo(object sender, RoutedEventArgs e)
        {
            if (DgdCourse.SelectedItem == null) return;
            if (DgdCourse.SelectedItem is EnrolledCourses == false) return;
            MainVM.CurrentView =
                new CourseInfoView(((EnrolledCourses)DgdCourse.SelectedItem).Course.CourseID, MainVM);
        }

        private void WriteOut(object sender, RoutedEventArgs e)
        {
            if (DgdCourse.SelectedItem == null) return;
            if (DgdCourse.SelectedItem is EnrolledCourses == false) return;
            _viewModel.WriteOut(((EnrolledCourses)DgdCourse.SelectedItem).Course.CourseID, (((EnrolledCourses)DgdCourse.SelectedItem).Enrollments.UserID));
            MainVM.CurrentView = new ProfileSubscriptions(MainVM);
        }
    }
}
