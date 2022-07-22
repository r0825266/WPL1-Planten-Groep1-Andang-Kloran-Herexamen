using System;
using System.ComponentModel;
using Plantjes.Dao;
using Plantjes.Models.Db;

namespace Plantjes.ViewModels.Services;

public class DetailService : INotifyPropertyChanged {
    private static DetailService _detailService;

    public Plant SelectedPlant
    {
        get => _selectedPlant;
        set
        {
            _selectedPlant = value; 
            SelectedPlantChanged.Invoke(this, value);
        }
    }
    //Robin
    //Op dit moment wordt de service niet gebruikt, deze is opgezet om later de plantdetails te kunnen weergeven en te kunnen toevoegen in alle usercontrols

    private DAOLogic _dao;
    private SearchService _searchService;
    private Plant _selectedPlant;

    public DetailService(SearchService searchService) {
        _dao = DAOLogic.Instance();
        _searchService = searchService;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    
    public event EventHandler<Plant> SelectedPlantChanged;
}