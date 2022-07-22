using System.Collections.Generic;
using System.Windows;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.Utilities.Attributes;
using Plantjes.ViewModels.Services;

namespace Plantjes.ViewModels; 

public class ViewModelBloom : ViewModelBase {
    // Using a DependencyProperty as the backing store for 
    //IsCheckBoxChecked.  This enables animation, styling, binding, etc...

    private DAOLogic _dao;
    private DetailService _detailService;

    private SearchService _SearchService = (SearchService)App.Current.Services.GetService(typeof(SearchService));

    //geschreven door christophe, op basis van een voorbeeld van owen
    private string _selectedBloeiHoogte;

    public ViewModelBloom(DetailService detailservice) {
        _detailService = detailservice;
        _dao = DAOLogic.Instance();
        _detailService.SelectedPlantChanged += (sender, plant) =>
        {
            ClearAllFields();

            FillBloeikleur();
            FillBloeihoogte();
            FillBloeitIn();
            FillBloeiwijzevorm();
            FillBloeiBlad();
        };
    }

    #region Filling elements based on plant selection
    //region written by MarijnCo
    public void FillBloeikleur()
    {
        var modeltype = typeof(ViewModelBloom);
        List<FenotypeMulti> fenoList = DAOFenotype.FilterFenotypeMultiFromPlant((int)_detailService.SelectedPlant.PlantId);
        bool exists = false;

        string field = "SelectedCheckBoxBloeikleur";
        for (int i = 0; i < fenoList.Count; i++)
        {
            exists = fenoList[i].Eigenschap.Contains("bloemkleur");
        }
        if (exists)
        {
            foreach (FenotypeMulti feno in fenoList)
            {
                if (feno.Eigenschap == "bloemkleur")
                {
                    switch (feno.Waarde)
                    {
                        case "zwart":
                            field += "Zwart";
                            break;
                        case "wit":
                            field += "Wit";
                            break;
                        case "roze":
                            field += "Roze";
                            break;
                        case "rood":
                            field += "Rood";
                            break;
                        case "oranje":
                            field += "Oranje";
                            break;
                        case "lila":
                            field += "Lila";
                            break;
                        case "grijs":
                            field += "Grijs";
                            break;
                        case "groen":
                            field += "Groen";
                            break;
                        case "geel":
                            field += "Geel";
                            break;
                        case "blauw":
                            field += "Blauw";
                            break;
                        case "violet":
                            field += "Violet";
                            break;
                        case "paars":
                            field += "Paars";
                            break;
                        case "bruin":
                            field += "Bruin";
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
        propsetter.Invoke(this, new object[] { true });
    }

    public void FillBloeihoogte()
    {

    }

    public void FillBloeitIn()
    {

    }

    public void FillBloeiwijzevorm()
    {

    }

    public void FillBloeiBlad()
    {

    }
    #endregion

    #region combobox bloeihoogte

    private string _selectedBloeiHoogteMin;

    [Clearable<string>]
    public string SelectedBloeiHoogteMin
    {
        get => _selectedBloeiHoogteMin;
        set
        {
            _selectedBloeiHoogteMin = value;
            OnPropertyChanged();
        }
    }

    private string _selectedBloeiHoogteMax;

    [Clearable<string>]
    public string SelectedBloeiHoogteMax
    {
        get => _selectedBloeiHoogteMax;
        set
        {
            _selectedBloeiHoogteMax = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Checkbox Bloeikleur

    private bool _selectedCheckBoxBloeikleurOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurOnbekend
    {
        get => _selectedCheckBoxBloeikleurOnbekend;

        set
        {
            _selectedCheckBoxBloeikleurOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurZwart;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurZwart {
        get => _selectedCheckBoxBloeikleurZwart;

        set {
            _selectedCheckBoxBloeikleurZwart = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurWit;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurWit {
        get => _selectedCheckBoxBloeikleurWit;

        set {
            _selectedCheckBoxBloeikleurWit = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurRosé;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurRosé {
        get => _selectedCheckBoxBloeikleurRosé;

        set {
            _selectedCheckBoxBloeikleurRosé = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurRood;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurRood {
        get => _selectedCheckBoxBloeikleurRood;

        set {
            _selectedCheckBoxBloeikleurRood = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurOranje;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurOranje {
        get => _selectedCheckBoxBloeikleurOranje;

        set {
            _selectedCheckBoxBloeikleurOranje = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurLila;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurLila {
        get => _selectedCheckBoxBloeikleurLila;

        set {
            _selectedCheckBoxBloeikleurLila = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurGrijs;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurGrijs {
        get => _selectedCheckBoxBloeikleurGrijs;

        set {
            _selectedCheckBoxBloeikleurGrijs = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurGroen;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurGroen {
        get => _selectedCheckBoxBloeikleurGroen;

        set {
            _selectedCheckBoxBloeikleurGroen = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurGeel;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurGeel {
        get => _selectedCheckBoxBloeikleurGeel;

        set {
            _selectedCheckBoxBloeikleurGeel = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurBlauw;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurBlauw {
        get => _selectedCheckBoxBloeikleurBlauw;

        set {
            _selectedCheckBoxBloeikleurBlauw = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurViolet;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurViolet {
        get => _selectedCheckBoxBloeikleurViolet;

        set {
            _selectedCheckBoxBloeikleurViolet = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurPaars;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurPaars {
        get => _selectedCheckBoxBloeikleurPaars;

        set {
            _selectedCheckBoxBloeikleurPaars = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeikleurBruin;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeikleurBruin {
        get => _selectedCheckBoxBloeikleurBruin;

        set {
            _selectedCheckBoxBloeikleurBruin = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding Checkbox BloeiHoogte

    private bool _selectedCheckBoxBloeiHoogteOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteOnbekend
    {
        get => _selectedCheckBoxBloeiHoogteOnbekend;

        set
        {
            _selectedCheckBoxBloeiHoogteOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteJan;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteJan {
        get => _selectedCheckBoxBloeiHoogteJan;

        set {
            _selectedCheckBoxBloeiHoogteJan = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteFeb;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteFeb {
        get => _selectedCheckBoxBloeiHoogteFeb;

        set {
            _selectedCheckBoxBloeiHoogteFeb = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteMar;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteMar {
        get => _selectedCheckBoxBloeiHoogteMar;

        set {
            _selectedCheckBoxBloeiHoogteMar = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteApr;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteApr {
        get => _selectedCheckBoxBloeiHoogteApr;

        set {
            _selectedCheckBoxBloeiHoogteApr = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteMay;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteMay {
        get => _selectedCheckBoxBloeiHoogteMay;

        set {
            _selectedCheckBoxBloeiHoogteMay = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteJun;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteJun {
        get => _selectedCheckBoxBloeiHoogteJun;

        set {
            _selectedCheckBoxBloeiHoogteJun = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteJul;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteJul {
        get => _selectedCheckBoxBloeiHoogteJul;

        set {
            _selectedCheckBoxBloeiHoogteJul = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteAug;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteAug {
        get => _selectedCheckBoxBloeiHoogteAug;

        set {
            _selectedCheckBoxBloeiHoogteAug = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteSep;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteSep {
        get => _selectedCheckBoxBloeiHoogteSep;

        set {
            _selectedCheckBoxBloeiHoogteSep = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteOct;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteOct {
        get => _selectedCheckBoxBloeiHoogteOct;

        set {
            _selectedCheckBoxBloeiHoogteOct = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteNov;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteNov {
        get => _selectedCheckBoxBloeiHoogteNov;

        set {
            _selectedCheckBoxBloeiHoogteNov = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiHoogteDec;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiHoogteDec {
        get => _selectedCheckBoxBloeiHoogteDec;

        set {
            _selectedCheckBoxBloeiHoogteDec = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding Checkbox Bloeit In

    private bool _selectedCheckBoxBloeitInOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInOnbekend
    {
        get => _selectedCheckBoxBloeitInOnbekend;

        set
        {
            _selectedCheckBoxBloeitInOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInJan;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInJan {
        get => _selectedCheckBoxBloeitInJan;

        set {
            _selectedCheckBoxBloeitInJan = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInFeb;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInFeb {
        get => _selectedCheckBoxBloeitInFeb;

        set {
            _selectedCheckBoxBloeitInFeb = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInMar;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInMar {
        get => _selectedCheckBoxBloeitInMar;

        set {
            _selectedCheckBoxBloeitInMar = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInApr;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInApr {
        get => _selectedCheckBoxBloeitInApr;

        set {
            _selectedCheckBoxBloeitInApr = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInMay;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInMay {
        get => _selectedCheckBoxBloeitInMay;

        set {
            _selectedCheckBoxBloeitInMay = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInJun;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInJun {
        get => _selectedCheckBoxBloeitInJun;

        set {
            _selectedCheckBoxBloeitInJun = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInJul;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInJul {
        get => _selectedCheckBoxBloeitInJul;

        set {
            _selectedCheckBoxBloeitInJul = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInAug;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInAug {
        get => _selectedCheckBoxBloeitInAug;

        set {
            _selectedCheckBoxBloeitInAug = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInSep;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInSep {
        get => _selectedCheckBoxBloeitInSep;

        set {
            _selectedCheckBoxBloeitInSep = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInOct;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInOct {
        get => _selectedCheckBoxBloeitInOct;

        set {
            _selectedCheckBoxBloeitInOct = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInNov;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInNov {
        get => _selectedCheckBoxBloeitInNov;

        set {
            _selectedCheckBoxBloeitInNov = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeitInDec;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeitInDec {
        get => _selectedCheckBoxBloeiHoogteDec;

        set {
            _selectedCheckBoxBloeiHoogteDec = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding checkboxes Bloeiwijzevorm

    private bool _selectedCheckBoxBloeiwijzeVormOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiwijzeVormOnbekend
    {
        get => _selectedCheckBoxBloeiwijzeVormOnbekend;

        set
        {
            _selectedCheckBoxBloeiwijzeVormOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiwijzeVorm1;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiwijzeVorm1 {
        get => _selectedCheckBoxBloeiwijzeVorm1;

        set {
            _selectedCheckBoxBloeiwijzeVorm1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiwijzeVorm2;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiwijzeVorm2 {
        get => _selectedCheckBoxBloeiwijzeVorm2;

        set {
            _selectedCheckBoxBloeiwijzeVorm2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiwijzeVorm3;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiwijzeVorm3 {
        get => _selectedCheckBoxBloeiwijzeVorm3;

        set {
            _selectedCheckBoxBloeiwijzeVorm3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiwijzeVorm4;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiwijzeVorm4 {
        get => _selectedCheckBoxBloeiwijzeVorm4;

        set {
            _selectedCheckBoxBloeiwijzeVorm4 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiwijzeVorm5;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiwijzeVorm5 {
        get => _selectedCheckBoxBloeiwijzeVorm5;

        set {
            _selectedCheckBoxBloeiwijzeVorm5 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBloeiwijzeVorm6;

    [Clearable<bool>]
    public bool SelectedCheckBoxBloeiwijzeVorm6 {
        get => _selectedCheckBoxBloeiwijzeVorm6;

        set {
            _selectedCheckBoxBloeiwijzeVorm6 = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Ratio Bloei/Blad

    private bool _selectedCheckBoxRatioOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxRatioOnbekend
    {
        get => _selectedCheckBoxRatioOnbekend;
        set
        {
            _selectedCheckBoxRatioOnbekend = value;
            OnPropertyChanged();
        }
    }
    // Gemaakt door Warre
    private bool _selectedCheckBoxRatioPachysandra;

    [Clearable<bool>]
    public bool SelectedCheckBoxRatioPachysandra
    {
        get => _selectedCheckBoxRatioPachysandra;
        set
        {
            _selectedCheckBoxRatioPachysandra = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxRatioGeranium;

    [Clearable<bool>]
    public bool SelectedCheckBoxRatioGeranium
    {
        get => _selectedCheckBoxRatioGeranium;
        set
        {
            _selectedCheckBoxRatioGeranium = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxRatioSalvia;

    [Clearable<bool>]
    public bool SelectedCheckBoxRatioSalvia
    {
        get => _selectedCheckBoxRatioSalvia;
        set
        {
            _selectedCheckBoxRatioSalvia = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxRatioAster;

    [Clearable<bool>]
    public bool SelectedCheckBoxRatioAster
    {
        get => _selectedCheckBoxRatioAster;
        set
        {
            _selectedCheckBoxRatioAster = value;
            OnPropertyChanged();
        }
    }
    #endregion
}