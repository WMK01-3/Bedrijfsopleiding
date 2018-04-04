using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class DashBoardView
    {
        #region OwnViewModel : BaseViewModel

        private DashBoardVM _viewModel;
        public DashBoardVM ViewModel
        {
            get => _viewModel = _viewModel ?? new DashBoardVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public static ListBox ListBox;

        public DashBoardView(MainWindowVM mainVM) : base(mainVM)
        {
            InitializeComponent();
            DataContext = new DashBoardVM(mainVM, this);
            ListBox = LbCourses;

            string curDir = Directory.GetCurrentDirectory();

            Uri url = new Uri($"file:///{curDir}/View/GoogleMaps/map.html");     // development versie
                                                                                 //Uri url = new Uri(String.Format("file:///{0}/Data/map.html", curDir));                  // Productie versie

            WbMaps.Navigate(url);

            WbMaps.ObjectForScripting = new MapsFunctions();

            ViewModel.LoadStandardDataBoxes(DataLabel1, DataLabel2, DataLabel3);
        }

        private void wbMaps_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            WbMaps.InvokeScript("initialize");

            ViewModel.LoadMarkers(WbMaps);
        }

        private void lbCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (LbItem item in LbCourses.SelectedItems)
                ViewModel.LoadCourseBox(LblCourseTitle, TbxCourseDesc, item.Value);
        }

    }

    public class LbItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    [ComVisible(true)]
    public class MapsFunctions
    {
        public static System.Windows.Controls.ListBox ListBox;

        public MapsFunctions()
        {
            ListBox = DashBoardView.ListBox;
        }

        public void LoadCoursesInListBox(int location_id)
        {
            using (var context = new CustomDbContext())
            {
                var courses = (from c in context.Courses
                               where c.LocationID == location_id
                               select c).ToList();

                if (ListBox.Items.Count > 0)
                {
                    ListBox.Items.Clear();
                }

                foreach (var course in courses)
                {
                    ListBox.Items.Add(new LbItem() { Value = course.CourseID, Text = course.Title });
                }
                ListBox.SelectedIndex = 0;
            }
        }

    }
}
