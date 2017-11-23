
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class DashBoardView
    {

        public DashBoardView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            DataContext = new DashBoardVM(parent, this);
        }
    }
}
