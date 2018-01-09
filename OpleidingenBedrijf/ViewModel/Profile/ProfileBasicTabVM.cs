using System.Data.Entity.Migrations;
using System.Windows;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.View.Profile;

namespace BedrijfsOpleiding.ViewModel.Profile
{
    public class ProfileBasicTabVM : BaseViewModel
    {
        private ProfileBasicTab _view;

        public string TxtFirstName { get; set; }
        public string TxtLastName { get; set; }
        public string TxtEmail { get; set; }


        private string _errorMessage = "";
        private Visibility _errorVisible = Visibility.Hidden;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {

                _errorMessage = value;

                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public Visibility ErrorVisible
        {
            get => _errorVisible;
            set
            {
                _errorVisible = value;
                OnPropertyChanged(nameof(ErrorVisible));
            }
        }

        public ProfileBasicTabVM(MainWindowVM vm, ProfileBasicTab v) : base(vm)
        {
            _view = v;
            getUserInfo();
            //UpdateProfessionsList();
        }


        private void getUserInfo()
        {
            TxtFirstName = MainVM.CurUser.FirstName;
            TxtLastName = MainVM.CurUser.LastName;
            TxtEmail = MainVM.CurUser.Email;
        }

        public void UpdateName()
        {
            ErrorVisible = Visibility.Hidden;

            using (CustomDbContext context = new CustomDbContext())
            {
                if (TxtFirstName.IsName() && TxtLastName.IsName())
                {
                    MainVM.CurUser.FirstName = TxtFirstName;
                    MainVM.CurUser.LastName = TxtLastName;
                    context.Users.AddOrUpdate(MainVM.CurUser);
                    context.SaveChanges();
                    MainVM.CurrentView = new ProfileView(MainVM, 2);
                }
                else
                {
                    ErrorMessage = "Voornaam of Achternaam niet correct";
                    ErrorVisible = Visibility.Visible;
                }
            }
        }

        public void UpdateEmail()
        {
            ErrorVisible = Visibility.Hidden;

            using (CustomDbContext context = new CustomDbContext())
            {
                if (TxtEmail.IsEmail())
                {
                    MainVM.CurUser.Email = TxtEmail;
                    context.Users.AddOrUpdate(MainVM.CurUser);
                    context.SaveChanges();
                    MainVM.CurrentView = new ProfileView(MainVM, 2);
                }
                else
                {
                    ErrorMessage = "Email niet correct";
                    ErrorVisible = Visibility.Visible;
                }
            }
        }

        public void UpdatePassword()
        {
            ErrorVisible = Visibility.Hidden;

            using (CustomDbContext context = new CustomDbContext())
            {
                if (_view.PbPassword.Password.IsPassword() != _view.PbPasswordRepeat.Password.IsPassword())
                {

                    if (_view.PbPassword.Password.IsPassword() && _view.PbPasswordRepeat.Password.IsPassword())
                    {
                        MainVM.CurUser.FirstName = TxtFirstName;
                        context.Users.AddOrUpdate(MainVM.CurUser);
                        context.SaveChanges();
                        MainVM.CurrentView = new ProfileView(MainVM, 2);
                    }
                    else
                    {
                        ErrorMessage = "Wachtwoord is geen wachtwoord";
                        ErrorVisible = Visibility.Visible;
                    }
                }
                else
                {
                    ErrorMessage = "Wachtwoorden zijn niet hetzelfde";
                    ErrorVisible = Visibility.Visible;
                }
            }
        }
    }
}
