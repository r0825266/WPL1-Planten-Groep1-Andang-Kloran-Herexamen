using Microsoft.Toolkit.Mvvm.Input;
using Plantjes.Models.Db;
using Plantjes.ViewModels.Services;
using Plantjes.Views.Home;
using System.Windows;

namespace Plantjes.ViewModels
{
    //Class by Kjell
    internal class ViewModelNieuwWachtwoord : ViewModelBase
    {
        Gebruiker _gebruiker;
        public ViewModelNieuwWachtwoord(LoginUserService loginUserService)
        {
            _loginService = loginUserService;
            _gebruiker = _loginService.gebruiker;
            changePasswordCommand = new RelayCommand(ChangePasswordButtonClick);
        }

        public LoginUserService _loginService { get; }
        public RelayCommand changePasswordCommand { get; set; }

        public void ChangePasswordButtonClick()
        {
            var Message = string.Empty;

            //Als de 2 wachtwoorden overeenkomen dan kan het wachtwoord opgeslaan worden terug naar login window gegaan worden
            
                errorMessage = _loginService.NewPasswordButton(passwordInput, passwordRepeatInput);

                //Kjell - close register window if no error - Based on Xander
                if (errorMessage == null || errorMessage == string.Empty)
                {
                    //Kjell - clear input on register and open  - Based on Xander
                    passwordInput = passwordRepeatInput = string.Empty;

                    //Opening login window again after changing password
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();

                    //Current window closing
                    Application.Current.Windows[0]?.Close();
                }                   
        }
        #region MVVM TextFieldsBinding

        private string _vivesNrInput;
        private string _firstNameInput;
        private string _lastNameInput;
        private string _emailAdresInput;
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

        public string vivesNrInput
        {
            get => _vivesNrInput;
            set
            {
                _vivesNrInput = value;
                OnPropertyChanged();
            }
        }

        public string firstNameInput
        {
            get => _firstNameInput;
            set
            {
                _firstNameInput = value;
                OnPropertyChanged();
            }
        }

        public string lastNameInput
        {
            get => _lastNameInput;
            set
            {
                _lastNameInput = value;
                OnPropertyChanged();
            }
        }

        public string emailAdresInput
        {
            get => _emailAdresInput;
            set
            {
                _emailAdresInput = value;
                OnPropertyChanged();
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
