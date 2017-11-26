using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View;

namespace BedrijfsOpleiding.ViewModel
{
    public class MainWindowVM : BaseViewModel
    {
        private readonly MainWindow _mainWindowView;

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

        private string _navigationView;
        public string NavigationText
        {
            get => _navigationView;
            set
            {
                _navigationView = value;
                OnPropertyChanged(nameof(NavigationText));
            }
        }

        #endregion

        #region CurUser : User

        private User _user;
        public User CurUser
        {
            get => _user ?? new User { Role = User.RoleEnum.Customer };
            private set => _user = value;
        }

        #endregion


        public string FullUserName => $"{CurUser?.FirstName} {CurUser?.LastName}";
        public bool IsEmployee => CurUser?.Role == User.RoleEnum.Employee;


        public MainWindowVM(MainWindow mainWindowView) : base(null)
        {
            _mainWindowView = mainWindowView;
        }

        public void SetUser(User user)
        {
            CurUser = user;
        }

        public void Login(User user)
        {
            CurUser = user;
            MenuView = new MenuBar(this);
            _mainWindowView.CntNavigation.Visibility = Visibility.Visible;
            CurrentView = new DashBoardView(this);
        }
    }
}
