using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.Utilities.Attributes;
using Plantjes.ViewModels.Services;

namespace Plantjes.ViewModels; 

public class ViewModelHabitat : ViewModelBase 
{
    private readonly DAOLogic _dao;
    private DetailService _detailService;

    public ViewModelHabitat(DetailService detailservice) {
        _dao = DAOLogic.Instance();
        _detailService = detailservice;
        _detailService.SelectedPlantChanged += (sender, plant) =>
        {
            ClearAllFields();

            FillPollenNectar();
            FillOntwikkelsnelheid();
            FillSociabiliteit();
            FillPlantEigenschappen();
            FillLevensvorm();
            FillStrategie();
        };

    }



    #region Filling elements based on plant selection
    //region written by Warre based FillGrondSoort by Marijn & Xander

    public void FillPollenNectar()
    {
        List<ExtraEigenschap> ListPolNec =
            DAOExtraEigenschap.FilterExtraEigenschapFromPlant((int)_detailService.SelectedPlant.PlantId);
        if (ListPolNec.Any(x => x.Pollenwaarde != null))
        {
            SelectedPollenwaardeMin = ListPolNec.Min(x => x.Pollenwaarde);
            SelectedPollenwaardeMax = ListPolNec.Max(x => x.Pollenwaarde);
        }
        if (ListPolNec.Any(x => x.Nectarwaarde != null))
        {
            SelectedNectarwaardeMin = ListPolNec.Min(x => x.Nectarwaarde);
            SelectedNectarwaardeMax = ListPolNec.Max(x => x.Nectarwaarde);
        }
    }

    public void FillOntwikkelsnelheid()
    {
        var modeltype = typeof(ViewModelHabitat);
        List<Commensalisme> CommListOntwikkel =
            DAOCommensalisme.FilterCommensalismeFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Commensalisme comm in CommListOntwikkel)
        {
            string field = "SelectedCheckBoxOntwikkelsnelheid";
            if (comm.Ontwikkelsnelheid != null)
            {
                field += comm.Ontwikkelsnelheid;
            }
            else
            {
                field += "Onbekend";
            }
            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object?[] { true });
        }
    }

    public void FillSociabiliteit()
    {
        var modeltype = typeof(ViewModelHabitat);
        List<CommensalismeMulti> CommListSocia =
            DAOCommensalisme.FilterCommensalismeMulti((int)_detailService.SelectedPlant.PlantId);

        foreach (CommensalismeMulti commmulti in CommListSocia)
        {
            string field = "SelectedCheckBoxSociabiliteit";
            if (commmulti.Waarde != null)
            {
                field += commmulti.Waarde;
            }
            else
            {
                field += "Onbekend";
            }
            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object?[] { true });
        }
    }

    public void FillPlantEigenschappen()
    {
        var modeltype = typeof(ViewModelHabitat);
        List<ExtraEigenschap> ListExtraEig =
            DAOExtraEigenschap.FilterExtraEigenschapFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (ExtraEigenschap extra in ListExtraEig)
        {
            string field = "";

            if (extra.Bijvriendelijke.HasValue && extra.Bijvriendelijke.Value)
            {
                field = "SelectedCheckBoxBijvriendelijk";
            }

            else if (extra.Eetbaar.HasValue && extra.Eetbaar.Value)
            {
                field = "SelectedCheckBoxEetbaar";
            }

            else if (extra.Kruidgebruik.HasValue && extra.Kruidgebruik.Value)
            {
                field = "SelectedCheckBoxKruidbaar";
            }

            else if (extra.Geurend.HasValue && extra.Geurend.Value)
            {
                field = "SelectedCheckBoxGeurend";
            }

            else if (extra.Vlindervriendelijk.HasValue && extra.Vlindervriendelijk.Value)
            {
                field = "SelectedCheckBoxVlindervriendelijk";
            }

            else if (extra.Vorstgevoelig.HasValue && extra.Vorstgevoelig.Value)
            {
                field = "SelectedCheckBoxVorstgevoelig";
            }
            else
            {
                field = "SelectedCheckBoxLevensvormOnbekend";
            }

            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object?[] { true });
        }
    }

    public void FillLevensvorm()
    {
        var modeltype = typeof(ViewModelHabitat);
        List<CommensalismeMulti> CommultiListLeven =
            DAOCommensalisme.FilterCommensalismeMulti((int)_detailService.SelectedPlant.PlantId);
        bool exist = false;

        string field = "SelectedCheckBoxLevensvorm";
        for (int i = 0; i < CommultiListLeven.Count; i++)
        {
            exist = CommultiListLeven[i].Eigenschap.Contains("levensvorm");
        }

        if (exist)
        {
            foreach (CommensalismeMulti commulti in CommultiListLeven)
            {
                if (commulti.Eigenschap == "levensvorm")
                {
                    switch (commulti.Waarde)
                    {
                        case "zogenaamde bodembedekker, weinig verdraagzaam met andere planten":
                            field += "1";
                            break;
                        case "verdraagzame bodembedekker, ook voor een soortenrijke aanplant":
                            field += "2";
                            break;
                        case "woekerende soort, worteluitlopers":
                            field += "3";
                            break;
                        case "weinig of niet woekerend, goed te combineren":
                            field += "4";
                            break;
                        case "robuuste, meestal grote plant, ook als solitair":
                            field += "5";
                            break;
                        case "zich sterk uitzaaiende soort":
                            field += "6";
                            break;
                        case "kortlevende plant":
                            field += "7";
                            break;
                        case "geeft een goeide snijbloem":
                            field += "8";
                            break;
                        case "slelt meer eisen qua 'eten en drinken' of winterbescherming":
                            field += "9";
                            break;
                        default:
                            field += "Onbekend";
                            break;
                    }
                }
            }
        }
        else
        {
            field += "Onbekend";
        }

        var prop = modeltype.GetProperty(field);
        var propsetter = prop.GetSetMethod();
        propsetter.Invoke(this, new object?[] { true });
    }

    public void FillStrategie()
    {
        var modeltype = typeof(ViewModelHabitat);
        List<Commensalisme> CommListStrat =
            DAOCommensalisme.FilterCommensalismeFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Commensalisme comm in CommListStrat)
        {
            string field = "SelectedCheckBoxStrategie";
            if (comm.Strategie != null)
            {
                field += comm.Strategie;
            }
            else
            {
                field += "Onbekend";
            }

            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object?[] { true });
        }
    }

    #endregion



    #region Binding Pollen & Nectar

    private string _selectedPollenwaardeOnbekend;
    [Clearable<string>]
    public string SelectedPollenwaardeOnbekend
    {
        get => _selectedPollenwaardeOnbekend;
        set
        {
            _selectedPollenwaardeOnbekend = value;
            OnPropertyChanged();
        }
    }

    private string _selectedPollenwaardeMin;
    [Clearable<string>]
    public string SelectedPollenwaardeMin
    {
        get => _selectedPollenwaardeMin;
        set
        {
            _selectedPollenwaardeMin = value;
            OnPropertyChanged();
        }
    }


    private string _selectedPollenwaardeMax;
    [Clearable<string>]
    public string SelectedPollenwaardeMax
    {
        get => _selectedPollenwaardeMax;
        set
        {
            _selectedPollenwaardeMax = value;
            OnPropertyChanged();
        }
    }

    private string _selectedNectarwaardeMin;
    [Clearable<string>]
    public string SelectedNectarwaardeMin
    {
        get => _selectedNectarwaardeMin;
        set
        {
            _selectedNectarwaardeMin = value;
            OnPropertyChanged();
        }
    }

    private string _selectedNectarwaardeMax;
    [Clearable<string>]
    public string SelectedNectarwaardeMax
    {
        get => _selectedNectarwaardeMax;
        set
        {
            _selectedNectarwaardeMax = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding Ontwikkelsnelheid
    //Gemaakt door Warre

    private bool _selectedCheckBoxOntwikkelsnelheidOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxOntwikkelsnelheidOnbekend
    {
        get => _selectedCheckBoxOntwikkelsnelheidOnbekend;
        set
        {
            _selectedCheckBoxOntwikkelsnelheidOnbekend = value;
            OnPropertyChanged();
        }
    }


    private bool _selectedCheckBoxOntwikkelsnelheidTraag;
    [Clearable<bool>]
    public bool SelectedCheckBoxOntwikkelsnelheidTraag
    {
        get => _selectedCheckBoxOntwikkelsnelheidTraag;
        set {
            _selectedCheckBoxOntwikkelsnelheidTraag = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxOntwikkelsnelheidMatig;
    [Clearable<bool>]
    public bool SelectedCheckBoxOntwikkelsnelheidMatig
    {
        get => _selectedCheckBoxOntwikkelsnelheidMatig;
        set {
            _selectedCheckBoxOntwikkelsnelheidMatig = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxOntwikkelsnelheidSnel;
    [Clearable<bool>]
    public bool SelectedCheckBoxOntwikkelsnelheidSnel
    {
        get => _selectedCheckBoxOntwikkelsnelheidSnel;
        set {
            _selectedCheckBoxOntwikkelsnelheidSnel = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Sociabliliteit

    private bool _selectedCheckBoxSociabiliteitOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxSociabiliteitOnbekend
    {
        get => _selectedCheckBoxSociabiliteitOnbekend;
        set
        {
            _selectedCheckBoxSociabiliteitOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSociabiliteitA;
    [Clearable<bool>]
    public bool SelectedCheckBoxSociabiliteitA
    {
        get => _selectedCheckBoxSociabiliteitA;
        set
        {
            _selectedCheckBoxSociabiliteitA = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSociabiliteitB;
    [Clearable<bool>]
    public bool SelectedCheckBoxSociabiliteitB
    {
        get => _selectedCheckBoxSociabiliteitB;
        set
        {
            _selectedCheckBoxSociabiliteitB = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSociabiliteitC;
    [Clearable<bool>]
    public bool SelectedCheckBoxSociabiliteitC
    {
        get => _selectedCheckBoxSociabiliteitC;
        set
        {
            _selectedCheckBoxSociabiliteitC = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSociabiliteitD;
    [Clearable<bool>]
    public bool SelectedCheckBoxSociabiliteitD
    {
        get => _selectedCheckBoxSociabiliteitD;
        set
        {
            _selectedCheckBoxSociabiliteitD = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSociabiliteitE;
    [Clearable<bool>]
    public bool SelectedCheckBoxSociabiliteitE
    {
        get => _selectedCheckBoxSociabiliteitE;
        set
        {
            _selectedCheckBoxSociabiliteitE = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region PlantEigenschappen

    private bool _selectedCheckBoxOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxOnbekend
    {
        get => _selectedCheckBoxOnbekend;
        set
        {
            _selectedCheckBoxOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBijvriendelijk;
    [Clearable<bool>]
    public bool SelectedCheckBoxBijvriendelijk {
        get => _selectedCheckBoxBijvriendelijk;
        set {
            _selectedCheckBoxBijvriendelijk = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxEetbaar;
    [Clearable<bool>]
    public bool SelectedCheckBoxEetbaar {
        get => _selectedCheckBoxEetbaar;
        set {
            _selectedCheckBoxEetbaar = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxKruidbaar;
    [Clearable<bool>]
    public bool SelectCheckBoxKruidbaar
    {
        get => _selectedCheckBoxKruidbaar;
        set
        {
            _selectedCheckBoxKruidbaar = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGeurend;
    [Clearable<bool>]
    public bool SelectedCheckBoxGeurend {
        get => _selectedCheckBoxGeurend;
        set {
            _selectedCheckBoxGeurend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVlindervriendelijk;
    [Clearable<bool>]
    public bool SelectedCheckBoxVlindervriendelijk {
        get => _selectedCheckBoxVlindervriendelijk;
        set {
            _selectedCheckBoxVlindervriendelijk = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVorstgevoelig;
    [Clearable<bool>]
    public bool SelectedCheckBoxVorstgevoelig {
        get => _selectedCheckBoxVorstgevoelig;
        set {
            _selectedCheckBoxVorstgevoelig = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Levensvorm

    private bool _selectedCheckBoxLevensvormOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvormOnbekend
    {
        get => _selectedCheckBoxLevensvormOnbekend;
        set
        {
            _selectedCheckBoxLevensvormOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm1;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm1
    {
        get => _selectedCheckBoxLevensvorm1;
        set {
            _selectedCheckBoxLevensvorm1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm2;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm2
    {
        get => _selectedCheckBoxLevensvorm2;
        set {
            _selectedCheckBoxLevensvorm2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm3;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm3
    {
        get => _selectedCheckBoxLevensvorm3;
        set {
            _selectedCheckBoxLevensvorm3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm4;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm4
    {
        get => _selectedCheckBoxLevensvorm4;
        set {
            _selectedCheckBoxLevensvorm4 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm5;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm5
    {
        get => _selectedCheckBoxLevensvorm5;
        set {
            _selectedCheckBoxLevensvorm5 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm6;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm6
    {
        get => _selectedCheckBoxLevensvorm6;
        set {
            _selectedCheckBoxLevensvorm6 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm7;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm7
    {
        get => _selectedCheckBoxLevensvorm7;
        set {
            _selectedCheckBoxLevensvorm7 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm8;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm8
    {
        get => _selectedCheckBoxLevensvorm8;
        set {
            _selectedCheckBoxLevensvorm8 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvorm9;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvorm9
    {
        get => _selectedCheckBoxLevensvorm9;
        set {
            _selectedCheckBoxLevensvorm9 = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region CheckboxStrategie

    private bool _selectedCheckBoxStrategieOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxStrategieOnbekend
    {
        get => _selectedCheckBoxStrategieOnbekend;

        set
        {
            _selectedCheckBoxStrategieOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStrategieC;
    [Clearable<bool>]
    public bool SelectedCheckBoxStrategieC
    {
        get => _selectedCheckBoxStrategieC;

        set
        {
            _selectedCheckBoxStrategieC = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStrategieCR;
    [Clearable<bool>]
    public bool SelectedCheckBoxStrategieCR
    {
        get => _selectedCheckBoxStrategieCR;

        set
        {
            _selectedCheckBoxStrategieCR = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStrategieR;
    [Clearable<bool>]
    public bool SelectedCheckBoxStrategieR
    {
        get => _selectedCheckBoxStrategieR;

        set
        {
            _selectedCheckBoxStrategieR = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStrategieRS;
    [Clearable<bool>]
    public bool SelectedCheckBoxStrategieRS
    {
        get => _selectedCheckBoxStrategieRS;

        set
        {
            _selectedCheckBoxStrategieRS = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStrategieS;
    [Clearable<bool>]
    public bool SelectedCheckBoxStrategieS
    {
        get => _selectedCheckBoxStrategieS;

        set
        {
            _selectedCheckBoxStrategieS = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStrategieSC;
    [Clearable<bool>]
    public bool SelectedCheckBoxStrategieSC
    {
        get => _selectedCheckBoxStrategieSC;

        set
        {
            _selectedCheckBoxStrategieSC = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStrategieCSR;
    [Clearable<bool>]
    public bool SelectedCheckBoxStrategieCSR
    {
        get => _selectedCheckBoxStrategieCSR;

        set
        {
            _selectedCheckBoxStrategieCSR = value;
            OnPropertyChanged();
        }
    }

    #endregion
}