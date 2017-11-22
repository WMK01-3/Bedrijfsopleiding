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
        private UserControl _navigationView;
        public UserControl NavigationView
        {
            get => _navigationView;
            set
            {
                _navigationView = value;
                OnPropertyChanged(nameof(NavigationView));
            }
        }
        #endregion
        
        public User CurUser { get; private set; }

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
