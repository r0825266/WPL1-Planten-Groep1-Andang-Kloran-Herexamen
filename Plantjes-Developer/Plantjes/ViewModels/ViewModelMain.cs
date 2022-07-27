using Plantjes.ViewModels.HelpClasses;
using Plantjes.ViewModels.Services;
using Plantjes.Views.Home;
using Plantjes.ViewModels.HelpClasses;
using System;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows;

namespace Plantjes.ViewModels; 

public class ViewModelMain : ViewModelBase {
    private ViewModelBase _currentViewModel;

    private readonly ViewModelRepo _viewModelRepo;
    //geschreven door kenny, adhv een voorbeeld van roy


    public ViewModelMain(LoginUserService loginUserService, SearchService searchService)
    {
        loggedInMessage = loginUserService.LoggedInMessage();

        _viewModelRepo = (ViewModelRepo)App.Current.Services.GetService(typeof(ViewModelRepo));
        this.searchService = (SearchService)searchService;
        this.loginUserService = loginUserService;

        mainNavigationCommand = new MyICommand<string>(_onNavigationChanged);

        // <-- Written by ANDANG KLORAN--> Navigation to the AddPlantWindow, LoginWindow and ChangePasswordWindow
        AddPlantCommand = new RelayCommand(AddPlantButtonView);
        LogoutCommand = new RelayCommand(LogoutButtonView);
        WachtwoordVeranderingCommand = new RelayCommand(VeranderWachtwoordButtonView);

    }

    public RelayCommand AddPlantCommand { get; set; }
    public RelayCommand LogoutCommand { get; set; }
    public RelayCommand WachtwoordVeranderingCommand { get; set; }
    private void AddPlantButtonView()
    {
        var addPlantWindow = new AddPlantWindow();
        addPlantWindow.Show();
        Application.Current.Windows[0]?.Close();
    }
    private void LogoutButtonView()
    {
        MessageBoxResult messageBoxResult = MessageBox.Show("Weet u zeker dat u wilt uitloggen?", "Uitloggen", MessageBoxButton.OKCancel);

        if (messageBoxResult == MessageBoxResult.OK)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Application.Current.Windows[0]?.Close();
        }
        else if (messageBoxResult == MessageBoxResult.Cancel)
        {
            return;
        }
    }
    private void VeranderWachtwoordButtonView()
    {
        var changePasswordWindow = new ChangePasswordWindow();
        changePasswordWindow.Show();
        Application.Current.Windows[0]?.Close();
    }
    //End----------------------------------------------------------------------------------------------

    public MyICommand<string> mainNavigationCommand { get; set; }

   

    public ViewModelBase currentViewModel {
        get => _currentViewModel;
        set => SetProperty(ref _currentViewModel, value);
    }

    private string _loggedInMessage { get; set; }

    public string loggedInMessage {
        get => _loggedInMessage;
        set {
            _loggedInMessage = value;

            RaisePropertyChanged("loggedInMessage");
        }
    }
    public string rol
    {
        get => loginUserService.gebruiker.Rol.Omschrijving;
    }

    public void _onNavigationChanged(string userControlName)
    {
        currentViewModel = _viewModelRepo.GetViewModel(userControlName);
    }
}