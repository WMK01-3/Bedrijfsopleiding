using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BedrijfsOpleiding.View.CourseView;
using BedrijfsOpleiding.View.LoginView;
using BedrijfsOpleiding.ViewModel.Course;

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
                        loginV.ParentViewModel.CurrentView = new CourseView(loginV.ParentViewModel);
                    }
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
