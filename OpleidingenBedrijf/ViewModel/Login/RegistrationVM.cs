using System.Linq;
using BedrijfsOpleiding.View.LoginView;
using System.Windows;
using System.Windows.Media;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;


namespace BedrijfsOpleiding.ViewModel.Login
{
    public class RegistrationVM : BaseViewModel
    {
        private RegistrationView _view;
        private int _errorCount;
        private readonly SolidColorBrush _redBrush = new SolidColorBrush(Colors.Tomato);
        private readonly SolidColorBrush _blueBrush = new SolidColorBrush(Colors.CornflowerBlue);

        public RegistrationVM(MainWindowVM vm, RegistrationView v) : base(vm)
        {
            _view = v;
        }

        public void RegisterUser()
        {
            _errorCount = 0;

            using (CustomDbContext context = new CustomDbContext())
            {
                int userNameCount = (from u in context.Users
                                     where u.UserName == _view.TbUsername.Text
                                     select u.UserName).Count();

                #region ErrorControllers
                #region ErrorBorders

                _view.PbPassword.BorderBrush = _view.PbPassword.Password.Length == 0 ? _redBrush : _blueBrush;
                _view.TbFirstName.BorderBrush = _view.TbFirstName.Text.Length == 0 ? _redBrush : _blueBrush;
                _view.TbLastName.BorderBrush = _view.TbLastName.Text.Length == 0 ? _redBrush : _blueBrush;
                _view.DpBirthDate.BorderBrush = _view.DpBirthDate.ToString().Length == 0 ? _redBrush : _blueBrush;
                _view.TbEmail.BorderBrush = _view.TbEmail.Text.Length == 0 ? _redBrush : _blueBrush;
                _view.PbPasswordRepeat.BorderBrush = _view.PbPasswordRepeat.Password.Length == 0 ? _redBrush : _blueBrush;
                _view.TbFirstName.BorderBrush = _view.TbFirstName.Text.IsName() ? _blueBrush : _redBrush;
                _view.TbLastName.BorderBrush = _view.TbLastName.Text.IsName() ? _blueBrush : _redBrush;
                _view.TbEmail.BorderBrush = _view.TbEmail.Text.IsEmail() ? _blueBrush : _redBrush;
                _view.PbPassword.BorderBrush = _view.PbPassword.Password.IsPassword() ? _blueBrush : _redBrush;
                _view.PbPasswordRepeat.BorderBrush = _view.PbPasswordRepeat.Password.IsPassword() ? _blueBrush : _redBrush;
                _view.TbUsername.BorderBrush = userNameCount == 0 ? _blueBrush : _redBrush;

                if (_view.TbUsername.Text.Length == 0)
                    _view.TbUsername.BorderBrush = _redBrush;
                else
                    _view.TbUsername.BorderBrush = userNameCount == 0 ? _blueBrush : _redBrush;

                #endregion

                #region ErrorMessages

                _view.ElbPassword.Content = _view.PbPassword.Password.Length == 0 ? "Dit veld is verplicht. " : "";
                _view.ElbBirthDate.Content = _view.DpBirthDate.ToString().Length == 0 ? "Dit veld is verplicht. " : "";
                _view.ElbLastName.Content = _view.TbLastName.Text.Length == 0 ? "Dit veld is verplicht. " : "";
                _view.ElbPasswordRepeat.Content = _view.PbPasswordRepeat.Password.Length == 0 ? "Dit veld is verplicht. " : "";


                if (_view.TbUsername.Text.Length == 0)
                    _view.ElbUsername.Content = "Dit veld is verplicht";
                else
                    _view.ElbUsername.Content = userNameCount == 0 ? "" : "Deze gebruikersnaam is al in gebruik.";
                if (_view.TbEmail.Text.Length == 0)
                    _view.ElbEmail.Content = "Dit veld is verplicht.";
                else
                    _view.ElbEmail.Content = !_view.TbEmail.Text.IsEmail() ? "Email adres voldoet niet aan de eisen." : "";
                if (_view.TbFirstName.Text.Length == 0)
                    _view.ElbFirstName.Content = "Dit veld is verplicht.";
                else
                    _view.ElbFirstName.Content = !_view.TbFirstName.Text.IsName() ? "Voornaam voldoet niet aan de eisen." : "";
                if (_view.TbLastName.Text.Length == 0)
                    _view.ElbLastName.Content = "Dit veld is verplicht.";
                else
                    _view.ElbLastName.Content = !_view.TbLastName.Text.IsName() ? "Achternaam voldoet niet aan de eisen." : "";
                if (_view.PbPassword.Password.Length == 0)
                    _view.ElbPassword.Content = "Dit veld is verplicht.";
                else
                {
                    _view.ElbPassword.Content = !_view.PbPassword.Password.IsPassword() ? "Wachtwoord veriest een 1 number, 1 letter, 1 special character, between 8 and 24 characters" : "";
                    if (!Equals(_view.PbPassword.Password, _view.PbPasswordRepeat.Password))
                        _view.ElbPassword.Content = "Wachtwoorden komen niet overeen.";
                }
                if (_view.PbPasswordRepeat.Password.Length == 0)
                    _view.ElbPasswordRepeat.Content = "Dit veld is verplicht.";
                else
                    _view.ElbPasswordRepeat.Content = !_view.PbPasswordRepeat.Password.IsPassword() ? "Wachtwoord veriest een 1 number, 1 letter, 1 special character, between 8 and 24 characters" : "";

                #endregion

                #endregion

                _errorCount += _view.TbEmail.Text.Length == 0 ? 1 : 0;
                _errorCount += userNameCount == 0 ? 0 : 1;
                _errorCount += _view.TbFirstName.Text.Length == 0 ? 1 : 0;
                _errorCount += _view.TbLastName.Text.Length == 0 ? 1 : 0;
                _errorCount += _view.TbUsername.Text.Length == 0 ? 1 : 0;
                _errorCount += _view.PbPassword.Password.Length == 0 ? 1 : 0;
                _errorCount += _view.PbPasswordRepeat.Password.Length == 0 ? 1 : 0;
                _errorCount += _view.DpBirthDate.ToString().Length == 0 ? 1 : 0;
                _errorCount += Equals(_view.PbPassword.Password, _view.PbPasswordRepeat.Password) ? 0 : 1;
                _errorCount += _view.TbEmail.Text.IsEmail() ? 0 : 1;
                _errorCount += _view.PbPassword.Password.IsPassword() ? 0 : 1;
                _errorCount += _view.TbFirstName.Text.IsName() ? 0 : 1;
                _errorCount += _view.TbLastName.Text.IsName() ? 0 : 1;

                if (_errorCount != 0) return;

                User addedUser = new User
                {
                    FirstName = _view.TbFirstName.Text,
                    LastName = _view.TbLastName.Text,
                    UserName = _view.TbUsername.Text,
                    PassWord = _view.PbPassword.Password,
                    Email = _view.TbEmail.Text,
                    Role = User.RoleEnum.Customer
                };
                context.Users.Add(addedUser);
                context.SaveChanges();
                MainVM.CurrentView = new LoginView(MainVM);
            }

        }
    }
}
