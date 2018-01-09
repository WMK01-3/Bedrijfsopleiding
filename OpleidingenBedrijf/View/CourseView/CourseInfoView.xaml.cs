using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseInfoView
    {
        private readonly int _courseID;

        #region OwnViewModel : BaseViewModel

        private CourseInfoVM _viewModel;
        public CourseInfoVM ViewModel
        {
            get => _viewModel = _viewModel ?? new CourseInfoVM(_courseID, MainVM);
            set => _viewModel = value;
        }

        #endregion

        public CourseInfoView(int courseId, MainWindowVM vm) : base(vm)
        {
            _courseID = courseId;
            InitializeComponent();


            bool isSignedUp = _viewModel.IsUserSignedUp(true);

            if (isSignedUp)
            {
                BtnSignUp.Visibility = Visibility.Hidden;
                SignedUp.Visibility = Visibility.Visible;
            }
            else
            {
                BtnSignUp.Visibility = Visibility.Visible;
            }
        }

        private void BtnEditCourse_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.EditCourse();
        }

        private void BtnDelCourse_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteCourse();
        }

        /// <summary>
        /// Sign the customer up for the Course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSignUp_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.IsUserSignedUp(false);
            BtnSignUp.Visibility = Visibility.Hidden;
            SignedUp.Visibility = Visibility.Visible;
        }
    }
}
