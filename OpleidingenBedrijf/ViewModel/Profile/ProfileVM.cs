using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.Profile;
using MessageView = BedrijfsOpleiding.View.Profile.MessageView;

namespace BedrijfsOpleiding.ViewModel.Profile
{
    public class ProfileVM : BaseViewModel
    {
        private readonly User _user;

        public bool IsTeacher => _user.Role == User.RoleEnum.Teacher;

        #region ProfileSubscriptions : ProfileSubscriptions

        private ProfileSubscriptions _profileSubscriptions;
        public ProfileSubscriptions ProfileSubscriptions =>
            _profileSubscriptions = _profileSubscriptions ?? new ProfileSubscriptions(MainVM);

        #endregion

        #region ProfessionTab : ProfileProfessionTab

        private ProfileProfessionTab _profileProfessionTab;
        public ProfileProfessionTab ProfessionTab =>
            _profileProfessionTab = _profileProfessionTab ?? new ProfileProfessionTab(MainVM);

        #endregion

        #region BasicTab : ProfileBasicTab

        private ProfileBasicTab _profileBasicTab;
        public ProfileBasicTab ProfileBasicTab =>
            _profileBasicTab = _profileBasicTab ?? new ProfileBasicTab(MainVM);

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
