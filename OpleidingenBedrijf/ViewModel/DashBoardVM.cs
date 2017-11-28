using System.Windows.Controls;
using BedrijfsOpleiding.View;

namespace BedrijfsOpleiding.ViewModel
{
    public class DashBoardVM : BaseViewModel
    {
        public bool IsCursist => MainVM.IsEmployee == false;
        public bool IsEmployee => MainVM.IsEmployee;

        public DashBoardVM(MainWindowVM vm, DashBoardView v) : base(vm)
        {
        }
    }
}
