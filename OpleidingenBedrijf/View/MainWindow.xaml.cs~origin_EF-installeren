using System.Windows.Media;
using BedrijfsOpleiding.View;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class MainWindow
    {

        public MainWindowVM MainWindowView { get; }

        public MainWindow()
        {
            InitializeComponent();
            MainWindowView = new MainWindowVM();
            MainWindowView.CurrentView = new BlueScreen(MainWindowView, Colors.Tomato);
            DataContext = MainWindowView;

            
        }
    }
}
