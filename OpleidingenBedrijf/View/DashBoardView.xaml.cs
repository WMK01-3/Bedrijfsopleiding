
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using BedrijfsOpleiding.ViewModel;
using Newtonsoft.Json;
using MessageBox = System.Windows.MessageBox;
using WebBrowser = System.Windows.Controls.WebBrowser;

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

        public DashBoardView(MainWindowVM mainVM) : base(mainVM)
        {
            InitializeComponent();
            DataContext = new DashBoardVM(mainVM, this);

            string curDir = Directory.GetCurrentDirectory();
            Uri url = new Uri(String.Format("file:///{0}/View/GoogleMaps/map.html", curDir));
            wbMaps.Navigate(url);
        }

        private void wbMaps_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            wbMaps.InvokeScript("initialize");
            wbMaps.InvokeScript("alert", "Document loaded and initialized");
            
            wbMaps.InvokeScript("addMarker", JsonConvert.SerializeObject(new
            {
                Lat = new double[] { 48.209331, 20.0, 30.0 },
                Long = new double[] { 16.381302, 20.0, 30.0 }
            }));

        }
    }
}
