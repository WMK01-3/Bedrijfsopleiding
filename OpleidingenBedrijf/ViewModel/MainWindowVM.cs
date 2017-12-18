using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using BedrijfsOpleiding.Annotations;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View;

namespace BedrijfsOpleiding.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region MenuView : UserControl
        private UserControl _menuView;
        public UserControl MenuView
        {
            get => _menuView;
            set
            {
                _menuView = value;
                OnPropertyChanged(nameof(MenuView));
            }
        }
        #endregion

        #region NavigationView : UserControl

        private string _navigationText = "";
        public string NavigationText
        {
            get => _navigationText;
            set
            {
                _navigationText = value;
                OnPropertyChanged(nameof(NavigationText));
            }
        }

        #endregion

        #region CurUser : User

        private User _user;
        public User CurUser
        {
            get => _user ?? new User { Role = User.RoleEnum.Customer };
            set => _user = value;
        }

        #endregion

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

        public bool NavigationEmpty => NavigationText.IsEmpty();

        public string FullUserName => $"{CurUser?.FirstName} {CurUser?.LastName}";
        public bool IsEmployee => CurUser?.Role == User.RoleEnum.Employee;
        public bool IsTeacher => CurUser?.Role == User.RoleEnum.Teacher;

        public void SetUser(User user) => CurUser = user;

        public void Login(User user)
        {
            CurUser = user;
            MenuView = new MenuBar(this);
            CurrentView = new DashBoardView(this);
        }

        public void SetNavigationText(string text) => NavigationText = text;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
