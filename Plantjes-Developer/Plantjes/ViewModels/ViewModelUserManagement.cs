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

        public ViewModelUserManagement() {
            instance = this;
            DAOLogic.context.ChangeTracker.StateChanged += (sender, args) => { args.NewState.PrintDebug(); };
            users.CollectionChanged += (sender, args) => {
                foreach (var argsNewItem in args.NewItems!) {
                    argsNewItem.PrintDebug();
                }
            };
        }

        public ObservableCollection<Gebruiker> users {
            get => new(DAOUser.GetAllUsersNoTracking());
        }

        public Gebruiker currentUser {
            get => _currentUser;
            set {
                _currentUser = value;
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
    }
}