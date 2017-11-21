using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BedrijfsOpleiding.View.LoginView;

namespace BedrijfsOpleiding.ViewModel.Login
{
    public class LoginVM : BaseViewModel 
    {
        public LoginVM(UserControl boundView) : base(boundView)
        {
        }

        public void Login()
        {
            LoginView loginV = (LoginView) CurrentView;

            loginV.ErrorMessage.Visibility = Visibility.Hidden;
            if (loginV.Username.Text == "" || loginV.Password.Text == "")
            {
                loginV.ErrorMessage.Visibility = Visibility.Visible;
                loginV.ErrorMessageMessage.Content = "Een of meerdere velden zijn leeg";
            }
        }
    }
}
