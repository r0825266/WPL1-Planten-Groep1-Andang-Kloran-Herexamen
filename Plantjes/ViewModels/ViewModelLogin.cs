using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.Input;
using Plantjes.Dao;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.Models.Enums;
using Plantjes.ViewModels.Services;
using Plantjes.Views.Home;
using XSystem.Security.Cryptography;

//written by kenny
namespace Plantjes.ViewModels; 

public class ViewModelLogin : ViewModelBase 
{
    private string _errorMessage;
    private string _loggedInMessage;
    public string _passwordInput;
    public string _passwordRepeatInput;

    private string _userNameInput;

    public ViewModelLogin(LoginUserService loginUserService) 
    {
        _loginService = loginUserService;
        //Xander - open mainwindow if debugging
#if DEBUG_AUTO_LOGIN
        if (Debugger.IsAttached) {
            var loginResult = new LoginResult { loginStatus = LoginStatus.NotLoggedIn };
            loginResult.gebruiker = DAOUser.context.Gebruikers.Include(x => x.Rol).FirstOrDefault(x => x.Rol.Omschrijving == "Docent");
            if (loginResult.gebruiker == null) {
                MessageBox.Show("Er is een debugger gevonden, maar geen docent-account! Gelieve een aan te maken of in te loggen!");
            }
            else {
                
                loginResult.loginStatus = LoginStatus.LoggedIn;
                _loginService.gebruiker = loginResult.gebruiker;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0]?.Close();
            }
        }
#endif
        loginCommand = new RelayCommand(LoginButtonClick);
        cancelCommand = new RelayCommand(CancelButton);
        registerCommand = new RelayCommand(RegisterButtonView);


    }

    private LoginUserService _loginService { get; }
    public RelayCommand loginCommand { get; set; }
    public RelayCommand cancelCommand { get; set; }
    public RelayCommand registerCommand { get; set; }


    public string errorMessage 
    {
        get => _errorMessage;
        set {
            _errorMessage = value;
        }
    }
    public string userNameInput 
    {
        get => _userNameInput;
        set {
            _userNameInput = value;
            OnPropertyChanged();
        }
    }

    public string passwordInput 
    {
        get => _passwordInput;
        set {
            _passwordInput = value;
            OnPropertyChanged();
        }
    }

    public void RegisterButtonView() 
    {
        var registerWindow = new RegisterWindow();
        registerWindow.Show();
        Application.Current.Windows[0]?.Close();
    }

    public void CancelButton() 
    {
        Application.Current.Shutdown();
    }

    //Binding met de textbox en passwordbox op de login GUI -- Kjell, Warre
    private SolidColorBrush _GebruikersNaamColor;
    public SolidColorBrush GebruikersNaamColor
    {
        get
        {
            return _GebruikersNaamColor;
        }
        set
        {
            _GebruikersNaamColor = value;
            RaisePropertyChanged("GebruikersNaamColor");
        }
    }

    private SolidColorBrush _PasswordColor;
    public SolidColorBrush PasswordColor
    {
        get
        {
            return _PasswordColor;
        }
        set
        {
            _PasswordColor = value;
            RaisePropertyChanged("PasswordColor");
        }
    }


    
    //Code voor textboxen in loginscherm rood kleuren als het foutieve ingave is -- Kjell & Warre
    private void LoginButtonClick() 
    {
        if (!string.IsNullOrWhiteSpace(userNameInput)) 
        {
            var loginResult = _loginService.CheckCredentials(userNameInput, passwordInput);

            
            if (loginResult.loginStatus == LoginStatus.LoggedIn) 
            {
                if(_loginService.gebruiker.LastLogin == null)
                {
                    var niewWachtwoordWindow = new NieuwWachtwoordWindow();
                    niewWachtwoordWindow.Show();
                    Application.Current.Windows[0]?.Close();
                }
                else
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    Application.Current.Windows[0]?.Close();

                    _loginService.gebruiker.LastLogin = DateTime.Now;
                    DAOLogic.context.SaveChanges();
                }
            }
            else if (loginResult.loginStatus == LoginStatus.NotLoggedIn)
            {
                if(string.IsNullOrWhiteSpace(userNameInput))
                {
                    if (string.IsNullOrWhiteSpace(passwordInput))
                    {
                        errorMessage = loginResult.errorMessage;
                        GebruikersNaamColor = new SolidColorBrush(Colors.Red);
                        PasswordColor = new SolidColorBrush(Colors.Transparent);
                    }
                    else if(!string.IsNullOrWhiteSpace(passwordInput))
                    {
                        errorMessage = loginResult.errorMessage;
                        GebruikersNaamColor = new SolidColorBrush(Colors.Red);
                        PasswordColor = new SolidColorBrush(Colors.Transparent);
                        
                    }

                }
                else if(!string.IsNullOrWhiteSpace(userNameInput))
                {
                    if (userNameInput.Contains("@") && userNameInput.Contains("."))
                    {
                        if(string.IsNullOrWhiteSpace(passwordInput))
                        {
                            errorMessage = loginResult.errorMessage;
                            GebruikersNaamColor = new SolidColorBrush(Colors.Transparent);
                            PasswordColor = new SolidColorBrush(Colors.Red);
                        }
                        else if(!string.IsNullOrWhiteSpace(passwordInput))
                        {
                            errorMessage = loginResult.errorMessage;
                            PasswordColor = new SolidColorBrush(Colors.Red);
                        }
                    }
                    else if(!(userNameInput.Contains("@") && userNameInput.Contains(".")))
                    {
                        if(string.IsNullOrWhiteSpace(passwordInput))
                        {
                            errorMessage = loginResult.errorMessage;
                            GebruikersNaamColor = new SolidColorBrush(Colors.Red);
                            PasswordColor = new SolidColorBrush(Colors.Red);
                        }
                        else if (!string.IsNullOrWhiteSpace(passwordInput))
                        {
                            errorMessage = loginResult.errorMessage;
                            PasswordColor = new SolidColorBrush(Colors.Red);

                            if (!((userNameInput.Contains("@")) && (userNameInput.Contains("."))))
                            {
                                errorMessage = loginResult.errorMessage;
                                GebruikersNaamColor = new SolidColorBrush(Colors.Red);
                            }
                        }
                    }
                }
            }

        }
        else
        {
            errorMessage = "Gebruikersnaam invullen.";
            GebruikersNaamColor = new SolidColorBrush(Colors.Red);
            PasswordColor = new SolidColorBrush(Colors.Red);
        }
        RaisePropertyChanged("errorMessage");
    }
    //------------------------------------------------------------------------------------------

    
}