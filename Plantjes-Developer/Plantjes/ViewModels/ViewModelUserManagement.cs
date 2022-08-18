using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.Input;
using Plantjes.Dao;
using Plantjes.Models.Db;

namespace Plantjes.ViewModels {
    internal class ViewModelUserManagement : ViewModelBase {
        private static ViewModelUserManagement instance;
        private static Gebruiker _currentUser;
        private static GebruikersInBehandeling _currentUserWaiting;

        public ViewModelUserManagement() {
            instance = this;
            DAOLogic.context.ChangeTracker.StateChanged += (sender, args) => { args.NewState.PrintDebug(); };
            users.CollectionChanged += (sender, args) => {
                foreach (var argsNewItem in args.NewItems!) {
                    argsNewItem.PrintDebug();
                }
            };
            //Andang Kloran
            usersWaiting.CollectionChanged += (sender, args) => {
                foreach (var argsNewItem in args.NewItems!)
                {
                    argsNewItem.PrintDebug();
                }
            };
        }

        public ObservableCollection<Gebruiker> users {
            get => new(DAOUser.GetAllUsersNoTracking());
        }
        
        public ObservableCollection<GebruikersInBehandeling> usersWaiting //Andang Kloran
        {
            get => new(DAOUser.GetAllUsersWaitingNoTracking());
        }

        public Gebruiker currentUser {
            get => _currentUser;
            set {
                _currentUser = value;
                OnPropertyChanged();
            }
        }
        public GebruikersInBehandeling currentUserWaiting //Andang Kloran
        {
            get => _currentUserWaiting;
            set
            {
                _currentUserWaiting = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand DeleteCommand { get; } = new(() => {
            if (_currentUser != null) DAOUser.DeleteUser(_currentUser.Id);
            instance.OnPropertyChanged("users");
        });

        public RelayCommand SaveUserCommand { get; } = new(() => {
            DAOUser.AddOrUpdate(_currentUser);
            instance.OnPropertyChanged("users");
        });

        public RelayCommand NewUserCommand { get; } = new(() => {
            //set current user to empty user
            _currentUser = new();
            instance.OnPropertyChanged("currentUser");
        });

        public RelayCommand ImportCsvCommand { get; } = new(DAOUser.AddUsersFromCsv);

        public RelayCommand RejectCommand { get; } = new(() => {
            if (_currentUserWaiting != null) DAOUser.RejectUserWaiting(_currentUserWaiting.Id);
            instance.OnPropertyChanged("usersWaiting");
        });

        public RelayCommand AcceptCommand { get; } = new(() => {
            DAOUser.AcceptUser(_currentUserWaiting); //Add user to Gegruikers list
            DAOUser.RejectUserWaiting(_currentUserWaiting.Id);//Remove user from GebruikersIBehandeling list
            instance.OnPropertyChanged("users");
            instance.OnPropertyChanged("usersWaiting");
        });

    }
}