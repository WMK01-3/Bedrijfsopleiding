using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View;
using BedrijfsOpleiding.View.CourseView;
using BedrijfsOpleiding.View.LoginView;

namespace BedrijfsOpleiding.ViewModel.Login
{
    public class LoginVM : BaseViewModel
    {
        public string Password { get; set; }
        public LoginVM(UserControl boundView) : base(boundView)
        {
        }

        public void Login()
        {
            LoginView loginV = (LoginView)CurrentView;
            loginV.Password.BorderBrush = System.Windows.Media.Brushes.CornflowerBlue;
            loginV.Username.BorderBrush = System.Windows.Media.Brushes.CornflowerBlue;
            loginV.ErrorMessage.Visibility = Visibility.Hidden;
            if (loginV.Username.Text == "" || loginV.Password.Password == "")
            {
                loginV.ErrorMessage.Visibility = Visibility.Visible;
                loginV.Username.BorderBrush = System.Windows.Media.Brushes.Red;
                loginV.Password.BorderBrush = System.Windows.Media.Brushes.Red;
                loginV.ErrorMessageMessage.Content = "Een of meerdere velden zijn leeg";
            }
            else
            {
                using (CustomDbContext context = new CustomDbContext())
                {
                    User user = (from u in context.Users
                                where u.UserName == loginV.Username.Text
                                select u).First();

                    if (user.PassWord == loginV.Password.Password)
                        ((MainWindowVM) loginV.ParentViewModel).Login(user);
                    else
                    {
                        loginV.ErrorMessage.Visibility = Visibility.Visible;
                        loginV.Username.BorderBrush = System.Windows.Media.Brushes.Red;
                        loginV.Password.BorderBrush = System.Windows.Media.Brushes.Red;
                        loginV.ErrorMessageMessage.Content = "Gebruikersnaam of wachtwoord fout";
                    }
                }
            }
        }
    }
}
