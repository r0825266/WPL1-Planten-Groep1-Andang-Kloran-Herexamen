using System.Windows;

using Microsoft.Toolkit.Mvvm.Input;
using Plantjes.ViewModels.Services;
using Plantjes.Views.Home;

namespace Plantjes.ViewModels;

//written by kenny
public class ViewModelRegister : ViewModelBase
{
    public ViewModelRegister(LoginUserService loginUserService)
    {
        _loginService = loginUserService;
        registerCommand = new RelayCommand(RegisterButtonClick);
        backCommand = new RelayCommand(BackButtonClick);
    }

    private LoginUserService _loginService { get; }

    public RelayCommand registerCommand { get; set; }
    public RelayCommand backCommand { get; set; }

    public void BackButtonClick()
    {
        var loginWindow = new LoginWindow();
        loginWindow.Show();
        Application.Current.Windows[0]?.Close();
    }

    public void RegisterButtonClick()
    {
        errorMessage = _loginService.RegisterButton(vivesNrInput, lastNameInput, firstNameInput, emailAdresInput, passwordInput, passwordRepeatInput);
        //xander - close register window if no error
        if (errorMessage == null || errorMessage == string.Empty)
        {
            //xander - clear input on register
            vivesNrInput = lastNameInput = firstNameInput = emailAdresInput = passwordInput = passwordRepeatInput = string.Empty;
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