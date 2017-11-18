using System.Windows.Controls;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Login;

namespace BedrijfsOpleiding.View
{
    public class BaseView : UserControl
    {
        public BaseViewModel ParentViewModel { get; }
        public BaseViewModel OwnViewModel { get; set; }

        public BaseView(BaseViewModel parent)
        {
            ParentViewModel = parent;

            DataContext = OwnViewModel;
        }
    }
}
