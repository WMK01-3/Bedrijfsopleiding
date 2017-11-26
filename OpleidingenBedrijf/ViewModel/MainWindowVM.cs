using System.Windows.Controls;
using System.Windows.Media;
using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.ViewModel
{
    public class MainWindowVM : BaseViewModel
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

        private User _user;

        public User CurUser
        {
            get => _user ?? new User{Role = User.RoleEnum.Customer};
            private set => _user = value;
        }

        public bool IsEmployee => CurUser?.Role == User.RoleEnum.Employee;


        public MainWindowVM() : base(null)
        {
        }

        public void SetUser(User user)
        {
            CurUser = user;
        }
    }
}
