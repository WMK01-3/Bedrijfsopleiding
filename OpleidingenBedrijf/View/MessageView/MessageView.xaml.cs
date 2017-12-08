using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Message;


namespace BedrijfsOpleiding.View.MessageView
{
    public partial class MessageView 
    {
        public MessageView(MainWindowVM vm) : base(vm)
        {
            InitializeComponent();
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainVM.CurrentView = new DashBoardView(MainVM);
        }
    }
}
