using System.Diagnostics;
using System.Windows.Controls;
using BedrijfsOpleiding.View;

namespace BedrijfsOpleiding.ViewModel
{
    public class DashBoardVM : BaseViewModel
    {
        private readonly BaseViewModel _parent;

        public bool IsCursist => ((MainWindowVM)_parent).IsEmployee == false;

        public bool IsEmployee => ((MainWindowVM)_parent).IsEmployee;

        public DashBoardVM(BaseViewModel parent, UserControl boundView) : base(boundView)
        {
            _parent = parent;
        }
    }
}
