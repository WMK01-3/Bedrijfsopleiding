using System.Windows;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class CourseOverView
    {
        #region OwnViewModel : BaseViewModel

        private CourseOverViewVM _viewModel;
        public CourseOverViewVM ViewModel
        {
            get => _viewModel = _viewModel ?? new CourseOverViewVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public CourseOverView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
            //courses.ItemsSource = _viewModel.CourseList;
            if (MainVM.IsEmployee) return;
            btnAddCourse.Height = 0;
            btnAddCourse.Visibility = Visibility.Hidden;
            Thickness thickness = btnAddCourse.Margin;
            thickness.Top = 0;
            thickness.Bottom = 0;
        }

        private void ToCourseInfo(object sender, RoutedEventArgs e)
        {
            if (courses.SelectedItem == null) return;

            if (courses.SelectedItem is Course == false) return;
            

            MainVM.CurrentView =
                new CourseInfoView(((Course)courses.SelectedItem).CourseID, MainVM);
        }

        private void BtnAddCourse_OnClick(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new AddCourse.AddCourseView(MainVM);
            MainVM.CurrentView = new AddCourse.AddCourseView(MainVM);
        }
    }
}