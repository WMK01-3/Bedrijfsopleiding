using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.View.Profile;
using Google.Protobuf.WellKnownTypes;

namespace BedrijfsOpleiding.ViewModel.Profile
{
    public class ProfileBasicTabVM : BaseViewModel
    {
        private ProfileBasicTab _view;

        public string TxtFirstName { get; set; }
        public string TxtLastName { get; set; }
        public string TxtEmail { get; set; }


        private string _errorMessage = "";
        private Visibility _errorVisible = Visibility.Hidden;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {

                _errorMessage = value;

                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public Visibility ErrorVisible
        {
            get => _errorVisible;
            set
            {
                _errorVisible = value;
                OnPropertyChanged(nameof(ErrorVisible));
            }
        }

        public ProfileBasicTabVM(MainWindowVM vm, ProfileBasicTab v) : base(vm)
        {
            _view = v;
            getUserInfo();
            //UpdateProfessionsList();
        }


        private void getUserInfo()
        {
            TxtFirstName = MainVM.CurUser.FirstName;
            TxtLastName = MainVM.CurUser.LastName;
            TxtEmail = MainVM.CurUser.Email;
        }

        public void UpdateAccount()
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                // get user
                var account = context.Users.First(d => d.UserID == MainVM.CurUser.UserID);


                string inputCheck = "";
                string pwCheck = "";
                ErrorMessage = "";

                // check input values
                inputCheck = string.IsNullOrWhiteSpace(TxtFirstName) ? "First name cannot be empty" : inputCheck;
                inputCheck = string.IsNullOrWhiteSpace(TxtLastName) ? "Last name cannot be empty" : inputCheck;
                inputCheck = string.IsNullOrWhiteSpace(TxtEmail) ? "Email cannot be empty" : inputCheck;
   
                inputCheck = !(TxtEmail.IsEmail()) ? "Email is not valid" : inputCheck;

                // update default values
                if (inputCheck == "")
                {
                    account.FirstName = TxtFirstName;
                    account.LastName = TxtLastName;
                    account.Email = TxtEmail;


                    // check pw
                    if (!string.IsNullOrWhiteSpace(_view.pbPassword.Password))
                    {
                        pwCheck = (_view.pbPassword.Password.IsPassword())
                            ? pwCheck
                            : "Wachtwoord voldoet niet aan de eisen";
                        pwCheck = (_view.pbPassword.Password == _view.pbPasswordRepeat.Password)
                            ? pwCheck
                            : "Wachtwoorden matchen niet";

                        account.PassWord = (pwCheck == "") ?  _view.pbPassword.Password : account.PassWord;
                    }
                }

                if (!string.IsNullOrEmpty(inputCheck) || !string.IsNullOrEmpty(pwCheck))
                {
                    ErrorMessage += inputCheck;
                    ErrorMessage += pwCheck;
                    ErrorVisible = Visibility.Visible;
                }else{
                    ErrorVisible = Visibility.Hidden;
                }

                context.SaveChanges();
            }

        }

    }
}
