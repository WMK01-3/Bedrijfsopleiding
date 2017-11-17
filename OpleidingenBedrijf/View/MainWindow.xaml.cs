using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class MainWindow
    {

        public MainWindowVM MainWindowView { get; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowView = new MainWindowVM
            {
                CurrentView = new LoginView.LoginView(MainWindowView)
            };
            DataContext = MainWindowView;
        }
    }
}
