using System.Linq;
using BedrijfsOpleiding.View.LoginView;
using System.Windows;
using System.Windows.Media;
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
                                     where u.UserName == _view.tbUsername.Text
                                     select u.UserName).Count();

                #region ErrorControllers

                #region ErrorIcons

                if (_view.tbUsername.Text.Length == 0)
                    _view.ecUsername.Visibility = Visibility.Visible;
                else
                    _view.ecUsername.Visibility = userNameCount == 0 ? Visibility.Hidden : Visibility.Visible;

                _view.ecBirthdate.Visibility = _view.dpBirthDate.ToString().Length == 0 ? Visibility.Visible : Visibility.Hidden;
                _view.ecFirstName.Visibility = _view.tbFirstName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                _view.ecEmail.Visibility = _view.tbEmail.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                _view.ecLastName.Visibility = _view.tbLastName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                _view.ecPassword.Visibility = _view.pbPassword.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                _view.ecRepeatPassword.Visibility = _view.pbPasswordRepeat.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                _view.ecLastName.Visibility = _view.tbLastName.Text.IsName() ? Visibility.Hidden : Visibility.Visible;
                _view.ecEmail.Visibility = _view.tbEmail.Text.IsEmail() ? Visibility.Hidden : Visibility.Visible;
                _view.ecFirstName.Visibility = _view.tbFirstName.Text.IsName() ? Visibility.Hidden : Visibility.Visible;
                _view.ecPassword.Visibility = _view.pbPassword.Password.IsPassword() ? Visibility.Hidden : Visibility.Visible;
                _view.ecRepeatPassword.Visibility = _view.pbPasswordRepeat.Password.IsPassword() ? Visibility.Hidden : Visibility.Visible;

                #endregion

                #region ErrorBorders

                _view.pbPassword.BorderBrush = _view.pbPassword.Password.Length == 0 ? _redBrush : _blueBrush;
                _view.tbFirstName.BorderBrush = _view.tbFirstName.Text.Length == 0 ? _redBrush : _blueBrush;
                _view.tbLastName.BorderBrush = _view.tbLastName.Text.Length == 0 ? _redBrush : _blueBrush;
                _view.dpBirthDate.BorderBrush = _view.dpBirthDate.ToString().Length == 0 ? _redBrush : _blueBrush;
                _view.tbEmail.BorderBrush = _view.tbEmail.Text.Length == 0 ? _redBrush : _blueBrush;
                _view.pbPasswordRepeat.BorderBrush = _view.pbPasswordRepeat.Password.Length == 0 ? _redBrush : _blueBrush;
                _view.tbFirstName.BorderBrush = _view.tbFirstName.Text.IsName() ? _blueBrush : _redBrush;
                _view.tbLastName.BorderBrush = _view.tbLastName.Text.IsName() ? _blueBrush : _redBrush;
                _view.tbEmail.BorderBrush = _view.tbEmail.Text.IsEmail() ? _blueBrush : _redBrush;
                _view.pbPassword.BorderBrush = _view.pbPassword.Password.IsPassword() ? _blueBrush : _redBrush;
                _view.pbPasswordRepeat.BorderBrush = _view.pbPasswordRepeat.Password.IsPassword() ? _blueBrush : _redBrush;
                _view.tbUsername.BorderBrush = userNameCount == 0 ? _blueBrush : _redBrush;

                if (_view.tbUsername.Text.Length == 0)
                    _view.tbUsername.BorderBrush = _redBrush;
                else
                    _view.tbUsername.BorderBrush = userNameCount == 0 ? _blueBrush : _redBrush;

                #endregion

                #region ErrorMessages

                _view.elbPassword.Content = _view.pbPassword.Password.Length == 0 ? "Dit veld is verplicht. " : "";
                _view.elbBirthDate.Content = _view.dpBirthDate.ToString().Length == 0 ? "Dit veld is verplicht. " : "";
                _view.elbLastName.Content = _view.tbLastName.Text.Length == 0 ? "Dit veld is verplicht. " : "";
                _view.elbPasswordRepeat.Content = _view.pbPasswordRepeat.Password.Length == 0 ? "Dit veld is verplicht. " : "";


                if (_view.tbUsername.Text.Length == 0)
                    _view.elbUsername.Content = "Dit veld is verplicht";
                else
                    _view.elbUsername.Content = userNameCount == 0 ? "" : "Deze gebruikersnaam is al in gebruik.";
                if (_view.tbEmail.Text.Length == 0)
                    _view.elbEmail.Content = "Dit veld is verplicht.";
                else
                    _view.elbEmail.Content = !_view.tbEmail.Text.IsEmail() ? "Email adres voldoet niet aan de eisen." : "";
                if (_view.tbFirstName.Text.Length == 0)
                    _view.elbFirstName.Content = "Dit veld is verplicht.";
                else
                    _view.elbFirstName.Content = !_view.tbFirstName.Text.IsName() ? "Voornaam voldoet niet aan de eisen." : "";
                if (_view.tbLastName.Text.Length == 0)
                    _view.elbLastName.Content = "Dit veld is verplicht.";
                else
                    _view.elbLastName.Content = !_view.tbLastName.Text.IsName() ? "Achternaam voldoet niet aan de eisen." : "";
                if (_view.pbPassword.Password.Length == 0)
                    _view.elbPassword.Content = "Dit veld is verplicht.";
                else
                {
                    _view.elbPassword.Content = !_view.pbPassword.Password.IsPassword() ? "Wachtwoord veriest een 1 number, 1 letter, 1 special character, between 8 and 24 characters" : "";
                    if (!Equals(_view.pbPassword.Password, _view.pbPasswordRepeat.Password))
                        _view.elbPassword.Content = "Wachtwoorden komen niet overeen.";
                }
                if (_view.pbPasswordRepeat.Password.Length == 0)
                    _view.elbPasswordRepeat.Content = "Dit veld is verplicht.";
                else
                    _view.elbPasswordRepeat.Content = !_view.pbPasswordRepeat.Password.IsPassword() ? "Wachtwoord veriest een 1 number, 1 letter, 1 special character, between 8 and 24 characters" : "";

                #endregion

                #endregion


                _errorCount += _view.tbEmail.Text.Length == 0 ? 1 : 0;
                _errorCount += userNameCount == 0 ? 0 : 1;
                _errorCount += _view.tbFirstName.Text.Length == 0 ? 1 : 0;
                _errorCount += _view.tbLastName.Text.Length == 0 ? 1 : 0;
                _errorCount += _view.tbUsername.Text.Length == 0 ? 1 : 0;
                _errorCount += _view.pbPassword.Password.Length == 0 ? 1 : 0;
                _errorCount += _view.pbPasswordRepeat.Password.Length == 0 ? 1 : 0;
                _errorCount += _view.dpBirthDate.ToString().Length == 0 ? 1 : 0;
                _errorCount += Equals(_view.pbPassword.Password, _view.pbPasswordRepeat.Password) ? 0 : 1;
                _errorCount += _view.tbEmail.Text.IsEmail() ? 0 : 1;
                _errorCount += _view.pbPassword.Password.IsPassword() ? 0 : 1;
                _errorCount += _view.tbFirstName.Text.IsName() ? 0 : 1;
                _errorCount += _view.tbLastName.Text.IsName() ? 0 : 1;



                
                if (_errorCount != 0) return;

                User addedUser = new User
                {
                    FirstName = _view.tbFirstName.Text,
                    LastName = _view.tbLastName.Text,
                    UserName = _view.tbUsername.Text,
                    PassWord = _view.pbPassword.Password,
                    Email = _view.tbEmail.Text,
                    Role = User.RoleEnum.Customer
                };
                context.Users.Add(addedUser);
                context.SaveChanges();
                MainVM.CurrentView = new LoginView(MainVM);
            }

        }
    }
}
