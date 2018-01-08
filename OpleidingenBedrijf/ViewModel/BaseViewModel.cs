using System.ComponentModel;
using System.Runtime.CompilerServices;
using BedrijfsOpleiding.Annotations;

namespace BedrijfsOpleiding.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public MainWindowVM MainVM;

        public BaseViewModel(MainWindowVM vm)
        {
            MainVM = vm;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
