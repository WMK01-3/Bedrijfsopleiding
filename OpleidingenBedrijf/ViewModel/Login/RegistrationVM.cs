using System.Linq;
using System.Windows.Controls;
using BedrijfsOpleiding.View.LoginView;
using System.Windows;
using System.Windows.Media;
using BedrijfsOpleiding.Models;


namespace BedrijfsOpleiding.ViewModel.Login
{
    public class RegistrationVM : BaseViewModel
    {
        private int _errorCount;
        private readonly SolidColorBrush _redBrush = new SolidColorBrush(Colors.Tomato);
        private readonly SolidColorBrush _blueBrush = new SolidColorBrush(Colors.CornflowerBlue);

        public RegistrationVM(UserControl boundView) : base(boundView)
        {
        }

        public void RegisterUser()
        {
            _errorCount = 0;
            RegistrationView rv = (RegistrationView)CurrentView;



            

            using (CustomDbContext context = new CustomDbContext())
            {
                int userNameCount = (from u in context.Users
                                     where u.UserName == rv.tbUsername.Text
                                     select u.UserName).Count();

                #region ErrorControllers

                #region ErrorIcons

                if (rv.tbUsername.Text.Length == 0)
                    rv.ecUsername.Visibility = Visibility.Visible;
                else
                    rv.ecUsername.Visibility = userNameCount == 0 ? Visibility.Hidden : Visibility.Visible;

                rv.ecBirthdate.Visibility = rv.dpBirthDate.ToString().Length == 0 ? Visibility.Visible : Visibility.Hidden;
                rv.ecFirstName.Visibility = rv.tbFirstName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                rv.ecEmail.Visibility = rv.tbEmail.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                rv.ecLastName.Visibility = rv.tbLastName.Text.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                rv.ecPassword.Visibility = rv.pbPassword.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                rv.ecRepeatPassword.Visibility = rv.pbPasswordRepeat.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
                rv.ecLastName.Visibility = rv.tbLastName.Text.IsName() ? Visibility.Hidden : Visibility.Visible;
                rv.ecEmail.Visibility = rv.tbEmail.Text.IsEmail() ? Visibility.Hidden : Visibility.Visible;
                rv.ecFirstName.Visibility = rv.tbFirstName.Text.IsName() ? Visibility.Hidden : Visibility.Visible;
                rv.ecPassword.Visibility = rv.pbPassword.Password.IsPassword() ? Visibility.Hidden : Visibility.Visible;
                rv.ecRepeatPassword.Visibility = rv.pbPasswordRepeat.Password.IsPassword() ? Visibility.Hidden : Visibility.Visible;

                #endregion

                #region ErrorBorders

                rv.pbPassword.BorderBrush = rv.pbPassword.Password.Length == 0 ? _redBrush : _blueBrush;
                rv.tbFirstName.BorderBrush = rv.tbFirstName.Text.Length == 0 ? _redBrush : _blueBrush;
                rv.tbLastName.BorderBrush = rv.tbLastName.Text.Length == 0 ? _redBrush : _blueBrush;
                rv.dpBirthDate.BorderBrush = rv.dpBirthDate.ToString().Length == 0 ? _redBrush : _blueBrush;
                rv.tbEmail.BorderBrush = rv.tbEmail.Text.Length == 0 ? _redBrush : _blueBrush;
                rv.pbPasswordRepeat.BorderBrush = rv.pbPasswordRepeat.Password.Length == 0 ? _redBrush : _blueBrush;
                rv.tbFirstName.BorderBrush = rv.tbFirstName.Text.IsName() ? _blueBrush : _redBrush;
                rv.tbLastName.BorderBrush = rv.tbLastName.Text.IsName() ? _blueBrush : _redBrush;
                rv.tbEmail.BorderBrush = rv.tbEmail.Text.IsEmail() ? _blueBrush : _redBrush;
                rv.pbPassword.BorderBrush = rv.pbPassword.Password.IsPassword() ? _blueBrush : _redBrush;
                rv.pbPasswordRepeat.BorderBrush = rv.pbPasswordRepeat.Password.IsPassword() ? _blueBrush : _redBrush;
                rv.tbUsername.BorderBrush = userNameCount == 0 ? _blueBrush : _redBrush;

                if (rv.tbUsername.Text.Length == 0)
                    rv.tbUsername.BorderBrush = _redBrush;
                else
                    rv.tbUsername.BorderBrush = userNameCount == 0 ? _blueBrush : _redBrush;

                #endregion

                #region ErrorMessages

                rv.elbPassword.Content = rv.pbPassword.Password.Length == 0 ? "Dit veld is verplicht. " : "";
                rv.elbBirthDate.Content = rv.dpBirthDate.ToString().Length == 0 ? "Dit veld is verplicht. " : "";
                rv.elbLastName.Content = rv.tbLastName.Text.Length == 0 ? "Dit veld is verplicht. " : "";
                rv.elbPasswordRepeat.Content = rv.pbPasswordRepeat.Password.Length == 0 ? "Dit veld is verplicht. " : "";


                if (rv.tbUsername.Text.Length == 0)
                    rv.elbUsername.Content = "Dit veld is verplicht";
                else
                    rv.elbUsername.Content = userNameCount == 0 ? "" : "Deze gebruikersnaam is al in gebruik.";
                if (rv.tbEmail.Text.Length == 0)
                    rv.elbEmail.Content = "Dit veld is verplicht.";
                else
                    rv.elbEmail.Content = !rv.tbEmail.Text.IsEmail() ? "Email adres voldoet niet aan de eisen." : "";
                if (rv.tbFirstName.Text.Length == 0)
                    rv.elbFirstName.Content = "Dit veld is verplicht.";
                else
                    rv.elbFirstName.Content = !rv.tbFirstName.Text.IsName() ? "Voornaam voldoet niet aan de eisen." : "";
                if (rv.tbLastName.Text.Length == 0)
                    rv.elbLastName.Content = "Dit veld is verplicht.";
                else
                    rv.elbLastName.Content = !rv.tbLastName.Text.IsName() ? "Achternaam voldoet niet aan de eisen." : "";
                if (rv.pbPassword.Password.Length == 0)
                    rv.elbPassword.Content = "Dit veld is verplicht.";
                else
                {
                    rv.elbPassword.Content = !rv.pbPassword.Password.IsPassword() ? "Wachtwoord veriest een 1 number, 1 letter, 1 special character, between 8 and 24 characters" : "";
                    if (!Equals(rv.pbPassword.Password, rv.pbPasswordRepeat.Password))
                        rv.elbPassword.Content = "Wachtwoorden komen niet overeen.";
                }
                if (rv.pbPasswordRepeat.Password.Length == 0)
                    rv.elbPasswordRepeat.Content = "Dit veld is verplicht.";
                else
                    rv.elbPasswordRepeat.Content = !rv.pbPasswordRepeat.Password.IsPassword() ? "Wachtwoord veriest een 1 number, 1 letter, 1 special character, between 8 and 24 characters" : "";

                #endregion

                #endregion


                _errorCount += rv.tbEmail.Text.Length == 0 ? 1 : 0;
                _errorCount += userNameCount == 0 ? 0 : 1;
                _errorCount += rv.tbFirstName.Text.Length == 0 ? 1 : 0;
                _errorCount += rv.tbLastName.Text.Length == 0 ? 1 : 0;
                _errorCount += rv.tbUsername.Text.Length == 0 ? 1 : 0;
                _errorCount += rv.pbPassword.Password.Length == 0 ? 1 : 0;
                _errorCount += rv.pbPasswordRepeat.Password.Length == 0 ? 1 : 0;
                _errorCount += rv.dpBirthDate.ToString().Length == 0 ? 1 : 0;
                _errorCount += Equals(rv.pbPassword.Password, rv.pbPasswordRepeat.Password) ? 0 : 1;
                _errorCount += rv.tbEmail.Text.IsEmail() ? 0 : 1;
                _errorCount += rv.pbPassword.Password.IsPassword() ? 0 : 1;
                _errorCount += rv.tbFirstName.Text.IsName() ? 0 : 1;
                _errorCount += rv.tbLastName.Text.IsName() ? 0 : 1;


                if (_errorCount != 0) return;

                User addedUser = new User
                {
                    FirstName = rv.tbFirstName.Text,
                    LastName = rv.tbLastName.Text,
                    UserName = rv.tbUsername.Text,
                    PassWord = rv.pbPassword.Password,
                    Email = rv.tbEmail.Text,
                    Role = User.RoleEnum.Customer,
                    Street = "yet to be implemented street",
                    City = "Yet to be implemented city",
                    Zipcode = "Yet to be implemented zip"
                    //FirstName = "Diederik",
                    //LastName = "Achternaam",
                    //UserName = "DiederikDocent",
                    //PassWord = "Welkom1!",
                    //Email = "DiederikDocent@mail.com",
                    //Role = User.RoleEnum.Teacher,
                    //Street = "yet to be implemented street",
                    //City = "Yet to be implemented city",
                    //Zipcode = "Yet to be implemented zip"
                };
                context.Users.Add(addedUser);
                context.SaveChanges();
                rv.ParentViewModel.CurrentView = new LoginView(rv.ParentViewModel);
            }
            
        }
    }
}
