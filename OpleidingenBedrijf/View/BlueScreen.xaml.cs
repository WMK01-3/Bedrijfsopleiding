using System.Windows;
using System.Windows.Media;
using BedrijfsOpleiding.ViewModel;

namespace BedrijfsOpleiding.View
{
    public partial class BlueScreen
    {
        private readonly BaseViewModel _parentVM;

        public BlueScreen(BaseViewModel _parentVM, Color color)
        {
            this._parentVM = _parentVM;
            InitializeComponent();
            Background = new SolidColorBrush(color);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _parentVM.CurrentView = new BlueScreen(_parentVM, Colors.DodgerBlue);
        }
    }
}
