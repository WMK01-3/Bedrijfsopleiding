using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.Profile;

namespace BedrijfsOpleiding.ViewModel.Profile
{
    public class ProfileVM : BaseViewModel
    {
        private User _user;


        public bool IsTeacher => _user.Role == User.RoleEnum.Teacher;


        #region ProfessionTab : ProfileProfessionTab

        private ProfileProfessionTab _profileTab;
        public ProfileProfessionTab ProfessionTab =>
            _profileTab = _profileTab ?? new ProfileProfessionTab(MainVM);

        #endregion
        #region BasicTab : ProfileBasicTab

        private ProfileBasicTab _profileBasicTab;
        public ProfileBasicTab ProfileBasicTab =>
            _profileBasicTab = _profileBasicTab ?? new ProfileBasicTab(MainVM);

        #endregion


        public ProfileVM(MainWindowVM vm) : base(vm)
        {
            _user = vm.CurUser;
        }
    }
}
