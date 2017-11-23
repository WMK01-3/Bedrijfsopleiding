using System.Diagnostics;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowVM mainWindowView = new MainWindowVM();
            //mainWindowView.CurrentView = new LoginView.LoginView(mainWindowView);
            mainWindowView.CurrentView = new CursusView.CursusView(mainWindowView);
            DataContext = mainWindowView;
            animatedContentControl.ShouldAnimate = true;
        }
    }
}
