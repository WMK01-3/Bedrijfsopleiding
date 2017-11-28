
namespace BedrijfsOpleiding.ViewModel
{
    public class BaseViewModel
    {
        public MainWindowVM MainVM;

        public BaseViewModel(MainWindowVM vm)
        {
            MainVM = vm;
        }
    }
}
