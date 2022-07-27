using Microsoft.Toolkit.Mvvm.Input;
using Plantjes.Models.Db;
using Plantjes.ViewModels.Services;
using Plantjes.Views.Home;
using System.Windows;

namespace Plantjes.ViewModels
{
    //Written by Andang Kloran
    public class ViewModelChangePassword : ViewModelBase
    {
        Gebruiker _gebruiker;
        public ViewModelChangePassword(LoginUserService loginUserService)
        {
            _loginService = loginUserService;
            _gebruiker = _loginService.gebruiker;
            changePasswordCommand = new RelayCommand(ChangePasswordButtonClick);
            AnnulerenCommand = new RelayCommand(AnnulerenButtonClick);
        }

        public LoginUserService _loginService { get; }
        public RelayCommand changePasswordCommand { get; set; }
        public RelayCommand AnnulerenCommand { get; set; }

        public void ChangePasswordButtonClick()
        {
            var Message = string.Empty;

            //If the two passwords correspond, then the password is saved and we navigate back to the main window
            
                errorMessage = _loginService.NewPasswordButton(passwordInput, passwordRepeatInput);

                //close register window if no error
                if (errorMessage == null || errorMessage == string.Empty)
                {
                    //Message box to confirm to user that the password has been successfully changed
                    MessageBox.Show("Password sucessfully changed.");

                    //Opening main window again after changing password
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    //Current window closing
                    Application.Current.Windows[0]?.Close();
                }
  
        }
        public void AnnulerenButtonClick()
        {
            //Close current window and reopen main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0]?.Close();

        }
        #region MVVM TextFieldsBinding

        public string _passwordInput;
        public string _passwordRepeatInput;
        private string _errorMessage;

        public string errorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;

                RaisePropertyChanged("errorMessage");
            }
        }
        public string passwordInput
        {
            get => _passwordInput;
            set
            {
                _passwordInput = value;
                OnPropertyChanged();
            }
        }

        public string passwordRepeatInput
        {
            get => _passwordRepeatInput;
            set
            {
                _passwordRepeatInput = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
    //------------------------------------------------------------------------------------
}
