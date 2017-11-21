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
            loginV.Password.BorderBrush = System.Windows.Media.Brushes.CornflowerBlue;
            loginV.Username.BorderBrush = System.Windows.Media.Brushes.CornflowerBlue;
            loginV.ErrorMessage.Visibility = Visibility.Hidden;
            if (loginV.Username.Text == "")
            {
                loginV.ErrorMessage.Visibility = Visibility.Visible;
                loginV.Username.BorderBrush = System.Windows.Media.Brushes.Red;
                loginV.ErrorMessageMessage.Content = "Een of meerdere velden zijn leeg";
            }
            if (loginV.Password.Text == "")
            {
                loginV.ErrorMessage.Visibility = Visibility.Visible;
                loginV.Password.BorderBrush = System.Windows.Media.Brushes.Red;
                loginV.ErrorMessageMessage.Content = "Een of meerdere velden zijn leeg";
            }
            //if(loginV.Username.Text == )
        }
    }
}
