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
                using (var context = new CustomDbContext())
                {
                    var result = (from u in context.Users
                                  where u.UserName == loginV.Username.Text
                                  select u.PassWord);
                    foreach (var element in result)
                    {
                        Password = element;
                    }
                    if (Password == loginV.Password.Password)
                    {
                        //loginV.ParentViewModel.CurrentView = new DashboardView(loginV.ParentViewModel);
                    }
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
