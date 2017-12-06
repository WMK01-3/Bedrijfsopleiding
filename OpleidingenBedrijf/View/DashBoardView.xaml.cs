
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using BedrijfsOpleiding.ViewModel;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;
using WebBrowser = System.Windows.Controls.WebBrowser;

namespace BedrijfsOpleiding.View
{
    [ComVisible(true)]
    public partial class DashBoardView
    {
        #region OwnViewModel : BaseViewModel

        private DashBoardVM _viewModel;
        public static System.Windows.Controls.ListBox ListBox;

        public DashBoardVM ViewModel
        {
            get => _viewModel = _viewModel ?? new DashBoardVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion
        public DashBoardView(MainWindowVM mainVM) : base(mainVM)
        {
            InitializeComponent();
            DataContext = new DashBoardVM(mainVM, this);
            ListBox = lbCourses;

            string curDir = Directory.GetCurrentDirectory();
            Uri url = new Uri(String.Format("file:///{0}/View/GoogleMaps/map.html", curDir));
            wbMaps.Navigate(url);

            wbMaps.ObjectForScripting = new MapsFunctions();
        }

        private void wbMaps_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            wbMaps.InvokeScript("initialize");

            ((DashBoardVM) ViewModel).LoadMarkers(wbMaps);
        }

        private void lbCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }
    }

    public class lbItem
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
                    ListBox.Items.Add(new lbItem() { Value = course.CourseID, Text = course.Title });
                }
                ListBox.SelectedIndex = 0;
            }
        }

    }
}
