using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using BedrijfsOpleiding.Annotations;
using BedrijfsOpleiding.View;

namespace BedrijfsOpleiding.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel(UserControl boundView) =>
            _currentView = boundView;

        #region CurrentView : UserControl
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
