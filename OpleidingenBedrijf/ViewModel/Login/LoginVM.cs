using System.Linq;
using System.Windows;
using System.Windows.Media;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.LoginView;

namespace BedrijfsOpleiding.ViewModel.Login
{
    public class LoginVM : BaseViewModel
    {
        private LoginView _view;
        public string Password { get; set; }
        private readonly SolidColorBrush _redBrush = new SolidColorBrush(Colors.Tomato);
        private readonly SolidColorBrush _blueBrush = new SolidColorBrush(Colors.CornflowerBlue);

        public LoginVM(MainWindowVM vm, LoginView v) : base(vm)
        {
            _view = v;
        }

       
        public void Login()
        {
            _view.Password.BorderBrush = _blueBrush;
            _view.Username.BorderBrush = _blueBrush;
            _view.ErrorMessage.Visibility = Visibility.Hidden;
            if (_view.Username.Text == "" || _view.Password.Password == "")
            {
                _view.ErrorMessage.Visibility = Visibility.Visible;
                _view.Username.BorderBrush = _redBrush;
                _view.Password.BorderBrush = _redBrush;
                _view.ErrorMessageMessage.Content = "Een of meerdere velden zijn leeg";
            }
            else
            {
                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<User> result = from u in context.Users
                                              where u.UserName == _view.Username.Text
                                              select u;

                    if (!result.Any())
                    {
                        _view.ErrorMessage.Visibility = Visibility.Visible;
                        _view.Username.BorderBrush = _redBrush;
                        _view.ErrorMessageMessage.Content = "Gebruikersnaam bestaat niet";
                        return;
                    }

                    User user = result.First();

                    if (user.PassWord == _view.Password.Password)
                        MainVM.Login(user);
                    else
                    {
                        _view.ErrorMessage.Visibility = Visibility.Visible;
                        _view.Username.BorderBrush = _redBrush;
                        _view.Password.BorderBrush = _redBrush;
                        _view.ErrorMessageMessage.Content = "Gebruikersnaam of wachtwoord fout";
                    }
                }
            }
        }
    }
}
