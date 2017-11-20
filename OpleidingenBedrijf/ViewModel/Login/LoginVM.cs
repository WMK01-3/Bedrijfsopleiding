using System.Windows.Controls;
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

            //Get username and password
            //Check them against the database

            //if (loginV.txtPassword.Text == "test" && loginV.txtUsername.Text == "test")
            //{
                //switch between roles

                //Cursist/student
                //CurrentView = new 
            //}
        }
    }
}
