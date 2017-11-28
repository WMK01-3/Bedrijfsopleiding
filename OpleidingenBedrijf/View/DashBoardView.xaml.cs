
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class DashBoardView
    {
        #region OwnViewModel : BaseViewModel

        private DashBoardVM _viewModel;
        public DashBoardVM ViewModel
        {
            get => _viewModel = _viewModel ?? new DashBoardVM(MainVM, this);
            set => _viewModel = value;
        }

        #endregion

        public DashBoardView(MainWindowVM mainVM) : base(mainVM)
        {
            InitializeComponent();
            DataContext = new DashBoardVM(mainVM, this);
        }
    }
}
