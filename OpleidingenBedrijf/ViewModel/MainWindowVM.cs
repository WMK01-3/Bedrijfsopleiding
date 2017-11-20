using BedrijfsOpleiding.Models;

namespace BedrijfsOpleiding.ViewModel
{
    public class MainWindowVM : BaseViewModel
    {
        public User CurUser { get; private set; }

        public MainWindowVM() : base(boundView: null)
        {
        }

        public void SetUser(User user)
        {
            CurUser = user;
        }
    }
}
