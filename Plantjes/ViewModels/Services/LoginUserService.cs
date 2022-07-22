using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Plantjes.Dao;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.Models.Enums;
using Plantjes.Views.Home;

namespace Plantjes.ViewModels.Services;

public class LoginUserService : INotifyPropertyChanged {
    //dao verklaren om data op te vragen en te setten in de databank
    private readonly DAOLogic _dao;

    public LoginUserService() {
        _dao = DAOLogic.Instance();
    } //gebruiker verklaren  om te gebruiken in de logica

    private Gebruiker _gebruiker { get; set; }

    #region Register Region

    public string RegisterButton(string vivesNrInput, string lastNameInput, string firstNameInput, string emailAdresInput, string passwordInput, string passwordRepeatInput) {
        //errorMessage die gereturned wordt om de gebruiker te waarschuwen wat er aan de hand is
        var Message = string.Empty;
        //checken of alle velden ingevuld zijn
        if (vivesNrInput != null && firstNameInput != null && lastNameInput != null && emailAdresInput != null && passwordInput != null && passwordRepeatInput != null) {
            //checken als het emailadres een geldig vives email is.
            if (emailAdresInput != null && emailAdresInput.Contains(".") && emailAdresInput.Contains("@")
                //checken als het email adres al bestaat of niet.

                && !DAOUser.GetEmailInUse(emailAdresInput)) {
                if ((vivesNrInput != null) & (vivesNrInput.Length != 8)) return "Dit is geen geldig vives nummer!";

                //checken als het herhaalde wachtwoord klopt of niet.
                if (passwordInput == passwordRepeatInput) {
                    //gebruiker registreren.
                    DAOUser.RegisterUser(vivesNrInput, firstNameInput, lastNameInput, emailAdresInput, passwordInput, last_login: System.DateTime.Today);
                    //Message = $"{firstNameInput}, je bent succevol geregistreerd,"+"\r\n"+$" uw gebruikersnaam is {emailAdresInput}." + 
                    // "\r\n" + $" {firstNameInput}, je kan dit venster wegklikken en inloggen.";
                    var loginWindow = new LoginWindow();
                    loginWindow.Show();
                } //foutafhandeling
                else {
                    Message = "Zorg dat de wachtwoorden overeenkomen.";
                }
            }
            else {
                Message = $"Fout! Emailadres is ongeldig, of is al in gebruik.";
            }
        }
        else {
            Message = "Zorg dat alle velden ingevuld zijn.";
        } //Message terugsturen om te binden aan een label in de viewModel.

        return Message;
    }
        


        

    #endregion

    #region Login Region

    //globale gebruiker om te gebruiken in de service
    public Gebruiker gebruiker = new();

    //zorgen dat de INotifyPropertyChanged geimplementeerd wordt
    public event PropertyChangedEventHandler PropertyChanged;

    //het eigenlijke loginsysteem
    //Xander - querying users without password entered is pointless and slow, only query when password isnt empty
    public LoginResult CheckCredentials(string userNameInput, string passwordInput)
    {
        //Nieuw loginResult om te gebruiken, status op NotLoggedIn zetten
        var loginResult = new LoginResult { loginStatus = LoginStatus.NotLoggedIn };

        //check if email is valid email
        if (userNameInput != null) //&& userNameInput.Contains("@student.vives.be")
        {
            //xander - password check
            if (passwordInput != null)
            {
                //gebruiker zoeken in de databank
                gebruiker = DAOUser.GetGebruikerWithEmail(userNameInput);
                loginResult.gebruiker = gebruiker;

                //omzetten van het ingegeven passwoord naar een gehashed passwoord
                var passwordBytes = Encoding.ASCII.GetBytes(passwordInput);
                var md5Hasher = new MD5CryptoServiceProvider();
                var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

                if (gebruiker != null)
                {
                    _gebruiker = gebruiker;
                    loginResult.gebruiker = gebruiker;
                    //passwoord controle
                    if (gebruiker.HashPaswoord != null && passwordHashed.SequenceEqual(gebruiker.HashPaswoord))
                    {
                        //indien true status naar LoggedIn zetten
                        loginResult.loginStatus = LoginStatus.LoggedIn;
                    }
                    else
                        //indien false errorMessage opvullen
                        loginResult.errorMessage += "\r\n" + "FOUT! Het ingegeven wachtwoord is niet juist. Gelieve opnieuw te proberen.";
                }
                else
                {
                    // als de gebruiker niet gevonden wordt, errorMessage invullen
                    loginResult.errorMessage = $"FOUT! Er is geen account gevonden voor {userNameInput}" + "\r\n" + "Gelieve eerst te registreren.";
                }
            }
            else
            {
                //xander - password check
                loginResult.errorMessage = "Gelieve een wachtwoord in te geven.";
            }
        }
        else
        {
            //indien geen geldig emailadress, errorMessage opvullen
            loginResult.errorMessage = "FOUT! Dit is geen geldig Vives emailadres.";
            return loginResult;
        }

        return loginResult;
    }

    //Functie om naam weer te geven in loginWindow, als login succesvol is
    //Xander - return object directly
    public string LoggedInMessage() {
        if (gebruiker != null)
        {
            return $"Ingelogd als: {gebruiker.Voornaam} {gebruiker.Achternaam}";
        }
        return string.Empty;
    }


    //Kjell
    public string NewPasswordButton(string passwordInput, string passwordRepeatInput)
    {
        var Message = string.Empty;

        //If password and repeat password are equal
        //Then password can be saved in database
        if (passwordInput == passwordRepeatInput)
        {
            //Calling DAOUser to save password and date in database
            DAOUser.ChangePassword(passwordInput, _gebruiker);
        }
        //If password and repeat password are not equal
        //Error message appears
        else if(passwordInput != passwordRepeatInput)
        {
            Message = "Wachtwoorden komen niet overeen!";
        }
        return Message;
    }

    #endregion
}