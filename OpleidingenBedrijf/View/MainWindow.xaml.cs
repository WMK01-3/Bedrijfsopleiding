using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowVM mainWindowView = new MainWindowVM();
            mainWindowView.CurrentView = new LoginView.LoginView(mainWindowView);
            DataContext = mainWindowView;
            AnimatedContentControl.ShouldAnimate = true;
        }
    }
}
