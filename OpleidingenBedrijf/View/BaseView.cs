using System.Windows.Controls;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public class BaseView : UserControl
    {
        public MainWindowVM MainVM { get; }
        
        public BaseView(MainWindowVM mainVM)
        {
            MainVM = mainVM;
        }
    }
}
