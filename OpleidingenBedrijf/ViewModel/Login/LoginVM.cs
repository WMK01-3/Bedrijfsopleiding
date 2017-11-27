using System.Linq;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;

using BedrijfsOpleiding.View.LoginView;

namespace BedrijfsOpleiding.ViewModel.Login
{
    public class LoginVM : BaseViewModel
    {
        public string Password { get; set; }
        private readonly SolidColorBrush _redBrush = new SolidColorBrush(Colors.Tomato);
        private readonly SolidColorBrush _blueBrush = new SolidColorBrush(Colors.CornflowerBlue);

        public LoginVM(UserControl boundView) : base(boundView)
        {
        }

        public void Login()
        {
            LoginView loginV = (LoginView)CurrentView;
            loginV.Password.BorderBrush = _blueBrush;
            loginV.Username.BorderBrush = _blueBrush;
            loginV.ErrorMessage.Visibility = Visibility.Hidden;
            if (loginV.Username.Text == "" || loginV.Password.Password == "")
            {
                loginV.ErrorMessage.Visibility = Visibility.Visible;
                loginV.Username.BorderBrush = _redBrush;
                loginV.Password.BorderBrush = _redBrush;
                loginV.ErrorMessageMessage.Content = "Een of meerdere velden zijn leeg";
            }
            else
            {
                using (CustomDbContext context = new CustomDbContext())
                {
                    IQueryable<User> result = from u in context.Users
                                              where u.UserName == loginV.Username.Text
                                              select u;

                    if (!result.Any()) return;

                    User user = result.First();

                    if (user.PassWord == loginV.Password.Password)
                        ((MainWindowVM)loginV.ParentViewModel).Login(user);
                    else
                    {
                        loginV.ErrorMessage.Visibility = Visibility.Visible;
                        loginV.Username.BorderBrush = _redBrush;
                        loginV.Password.BorderBrush = _redBrush;
                        loginV.ErrorMessageMessage.Content = "Gebruikersnaam of wachtwoord fout";
                    }
                }
            }
        }
    }
}
