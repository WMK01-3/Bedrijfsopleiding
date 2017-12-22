using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.MessageView;
using BedrijfsOpleiding.View.Profile;

namespace BedrijfsOpleiding.ViewModel.Profile
{
    public class ProfileVM : BaseViewModel
    {
        private User _user;

        public bool IsTeacher => _user.Role == User.RoleEnum.Teacher;

        #region ProfessionTab : ProfileProfessionTab

        private ProfileProfessionTab _profileProfessionTab;
        public ProfileProfessionTab ProfessionTab =>
            _profileProfessionTab = _profileProfessionTab?? new ProfileProfessionTab(MainVM);

        #endregion


        #region MessageT : MessageView

        private MessageView _profileMessageTab;
        public MessageView MessageTab =>
            _profileMessageTab = _profileMessageTab ?? new MessageView(MainVM);

        #endregion



        public ProfileVM(MainWindowVM vm) : base(vm)
        {
            _user = vm.CurUser;
        }
    }
}
