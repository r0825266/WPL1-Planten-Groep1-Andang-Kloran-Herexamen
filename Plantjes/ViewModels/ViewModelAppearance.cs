using System.Collections.Generic;
using System.Windows;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.Utilities.Attributes;
using Plantjes.ViewModels.Services;

namespace Plantjes.ViewModels; 

public class ViewModelAppearance : ViewModelBase {
    private DAOLogic _dao;
    private DetailService _detailService;

    private string _selectedBladHoogte;

    public ViewModelAppearance(DetailService detailservice)
    {
        _dao = DAOLogic.Instance();
        _detailService = detailservice;
        _detailService.SelectedPlantChanged += (sender, plant) =>
        {
            ClearAllFields();

            FillBladKleur();
            FillSpruitFene();
            FillBladgrootte();
            FillStengelvormBladvorm();
            FillLevensvormen();
            
        };
    }

    public string SelectedBladHoogte {
        get => _selectedBladHoogte;
        set {
            _selectedBladHoogte = value;
            OnPropertyChanged();
        }
    }

    //geschreven door christophe op basis van owens code

    #region Filling elements based on plant selection
    //region written by Warre based on FillGrondSoort by Marijn & Xander

    public void FillBladKleur()
    {
        var modeltype = typeof(ViewModelAppearance);
        List<FenotypeMulti> FenoListKleur =
            DAOFenotype.FilterFenotypeMultiFromPlant((int)_detailService.SelectedPlant.PlantId);
        bool exists = false;

        string field = "SelectedCheckBoxBladkleur";
        for (int i = 0; i < FenoListKleur.Count; i++)
        {
            exists = FenoListKleur[i].Eigenschap.Contains("bladkleur");
        }

        if (exists)
        {
            foreach (FenotypeMulti fnmulti in FenoListKleur)
            {
                if (fnmulti.Eigenschap == "bladkleur")
                {
                    switch (fnmulti.Waarde)
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
        propsetter.Invoke(this, new object?[] { true });
        
    }

    public void FillBladgrootte()
    {
        var modeltype = typeof(ViewModelAppearance);
        List<Fenotype> FenoListBladGrootte =
            DAOFenotype.filterFenoTypeFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Fenotype Feno in FenoListBladGrootte)
        {
            string field = "";

            switch (Feno.Bladgrootte)
            {
                case 5:
                    field = "SelectedCheckBoxGrootte5";
                    break;
                case 10:
                    field = "SelectedCheckBoxGrootte10";
                    break;
                case 20:
                    field = "SelectedCheckBoxGrootte20";
                    break;
                case 50:
                    field = "SelectedCheckBoxGrootte50";
                    break;
                case 100:
                    field = "SelectedCheckBoxGrootte100";
                    break;
                case 150:
                    field = "SelectedCheckBoxGrootte150";
                    break;
                default:
                    field = "SelectedCheckBoxGrootteOnbekend";
                    break;


            }

            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    public void FillSpruitFene()
    {
        var modeltype = typeof(ViewModelAppearance);
        List<Fenotype> FenoListSpruit =
            DAOFenotype.filterFenoTypeFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Fenotype Feno in FenoListSpruit)
        {
            string field = "";

            switch (Feno.Spruitfenologie)
            {
                case "zomergroen" :
                    field = "SelectedCheckBoxSpruitZomergroen";
                    break;
                case "wintergroen":
                    field = "SelectedCheckBoxSpruitWintergroen";
                    break;
                case "altijd groen":
                    field = "SelectedCheckBoxSpruitAltijdGroen";
                    break;
                case "voorjaarsgroen":
                    field = "SelectedCheckBoxSpruitVoorjaarsgroen";
                    break;
                default:
                    field = "SelectedCheckBoxSpruitOnbekend";
                    break;
            }

            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }

    }

    public void FillStengelvormBladvorm()
    {
        var modeltype = typeof(ViewModelAppearance);
        List<Fenotype> FenoListStengelBlad =
            DAOFenotype.filterFenoTypeFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Fenotype feno in FenoListStengelBlad)
        {
            string field = "";
            switch (feno.Spruitfenologie)
            {
                case "tuffed":
                    field = "SelectedCheckBoxStengelvormenVorm1";
                    break;
                case "upright arching":
                    field = "SelectedCheckBoxStengelvormenVorm2";
                    break;
                case "arching":
                    field = "SelectedCheckBoxStengelvormenVorm3";
                    break;
                case "upright divergent":
                    field = "SelectedCheckBoxStengelvormenVorm4";
                    break;
                case "upright erect":
                    field = "SelectedCheckBoxStengelvormenVorm5";
                    break;
                case "mounted":
                    field = "SelectedCheckBoxStengelvormenVorm6";
                    break;
                case "kruipend, horizontaal groeiend":
                    field = "SelectedCheckBoxBladvormenVorm1";
                    break;
                case "rond/waaiervormig":
                    field = "SelectedCheckBoxBladvormenVorm2";
                    break;
                case "kussenvormend":
                    field = "SelectedCheckBoxBladvormenVorm3";
                    break;
                case "uitbuigend":
                    field = "SelectedCheckBoxBladvormenVorm4";
                    break;
                case "wortelrozetplant":
                    field = "SelectedCheckBoxBladvormenVorm5";
                    break;
                case "secculenten":
                    field = "SelectedCheckBoxBladvormenVorm6";
                    break;
                case "polvormers":
                    field = "SelectedCheckBoxBladvormenVorm7";
                    break;
                case "parasolvormig":
                    field = "SelectedCheckBoxBladvormenVorm8";
                    break;
                default:
                    field = "SelectedCheckBoxBladvormenOnbekend";
                    break;
            }
            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    public void FillLevensvormen()
    {
        var modeltype = typeof(ViewModelAppearance);
        List<Fenotype> FenoListLeven =
            DAOFenotype.filterFenoTypeFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Fenotype Feno in FenoListLeven)
        {
            string field = "";

            switch (Feno.Levensvorm)
            {
                case "1. Hydrofyten - waterplanten":
                    field = "SelectedCheckBoxLevensvormenVorm1";
                    break;
                case "3. Helofyten - winterknoppen onder water, bloeiende planten boven water":
                    field = "SelectedCheckBoxLevensvormenVorm1";
                    break;
                case "4.Cryptofyten of Geofyten -winterknoppen onder de grond":
                    field = "SelectedCheckBoxLevensvormenVorm1";
                    break;
                case "6. HemiCryptofyten - winterknoppen op of iets onder de grond":
                    field = "SelectedCheckBoxLevensvormenVorm1";
                    break;
                case "7. Chamaefyten - winterknoppen tot 50 cm boven de grond":
                    field = "SelectedCheckBoxLevensvormenVorm1";
                    break;
                case "9. P¨hanerofyten - winterknoppen minstens 50 cm boven de grond":
                    field = "SelectedCheckBoxLevensvormenVorm1";
                    break;
                default:
                    field = "SelectedCheckBoxLevensvormenOnbekend";
                    break;


            }

            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    #endregion



    #region Binding checkboxen Bladkleur
    
    private bool _selectedCheckBoxBladkleurOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurOnbekend
    {
        get => _selectedCheckBoxBladkleurOnbekend;
        set
        {
            _selectedCheckBoxBladkleurOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurZwart;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurZwart {
        get => _selectedCheckBoxBladkleurZwart;

        set {
            _selectedCheckBoxBladkleurZwart = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurWit;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurWit {
        get => _selectedCheckBoxBladkleurWit;

        set {
            _selectedCheckBoxBladkleurWit = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurRosé;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurRosé {
        get => _selectedCheckBoxBladkleurRosé;

        set {
            _selectedCheckBoxBladkleurRosé = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurRood;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurRood {
        get => _selectedCheckBoxBladkleurRood;

        set {
            _selectedCheckBoxBladkleurRood = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurOranje;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurOranje {
        get => _selectedCheckBoxBladkleurOranje;

        set {
            _selectedCheckBoxBladkleurOranje = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurLila;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurLila {
        get => _selectedCheckBoxBladkleurLila;

        set {
            _selectedCheckBoxBladkleurLila = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurGrijs;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurGrijs {
        get => _selectedCheckBoxBladkleurGrijs;

        set {
            _selectedCheckBoxBladkleurGrijs = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurGroen;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurGroen {
        get => _selectedCheckBoxBladkleurGroen;

        set {
            _selectedCheckBoxBladkleurGroen = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurGeel;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurGeel {
        get => _selectedCheckBoxBladkleurGeel;

        set {
            _selectedCheckBoxBladkleurGeel = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurBlauw;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurBlauw {
        get => _selectedCheckBoxBladkleurBlauw;

        set {
            _selectedCheckBoxBladkleurBlauw = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurViolet;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurViolet {
        get => _selectedCheckBoxBladkleurViolet;

        set {
            _selectedCheckBoxBladkleurViolet = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurPaars;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurPaars {
        get => _selectedCheckBoxBladkleurPaars;

        set {
            _selectedCheckBoxBladkleurPaars = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladkleurBruin;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladkleurBruin {
        get => _selectedCheckBoxBladkleurBruin;

        set {
            _selectedCheckBoxBladkleurBruin = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding checkboxen BladHoogte

    private bool _selectedCheckBoxBladHoogteOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteOnbekend
    {
        get => _selectedCheckBoxBladHoogteOnbekend;
        set
        {
            _selectedCheckBoxBladHoogteOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteJan;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteJan {
        get => _selectedCheckBoxBladHoogteJan;

        set {
            _selectedCheckBoxBladHoogteJan = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteFeb;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteFeb {
        get => _selectedCheckBoxBladHoogteFeb;

        set {
            _selectedCheckBoxBladHoogteFeb = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteMar;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteMar {
        get => _selectedCheckBoxBladHoogteMar;

        set {
            _selectedCheckBoxBladHoogteMar = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteApr;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteApr {
        get => _selectedCheckBoxBladHoogteApr;

        set {
            _selectedCheckBoxBladHoogteApr = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteMay;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteMay {
        get => _selectedCheckBoxBladHoogteMay;

        set {
            _selectedCheckBoxBladHoogteMay = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteJun;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteJun {
        get => _selectedCheckBoxBladHoogteJun;

        set {
            _selectedCheckBoxBladHoogteJun = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteJul;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteJul {
        get => _selectedCheckBoxBladHoogteJul;

        set {
            _selectedCheckBoxBladHoogteJul = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteAug;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteAug {
        get => _selectedCheckBoxBladHoogteAug;

        set {
            _selectedCheckBoxBladHoogteAug = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteSep;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteSep {
        get => _selectedCheckBoxBladHoogteSep;

        set {
            _selectedCheckBoxBladHoogteSep = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteOct;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteOct {
        get => _selectedCheckBoxBladHoogteOct;

        set {
            _selectedCheckBoxBladHoogteOct = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteNov;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteNov {
        get => _selectedCheckBoxBladHoogteNov;

        set {
            _selectedCheckBoxBladHoogteNov = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladHoogteDec;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladHoogteDec {
        get => _selectedCheckBoxBladHoogteDec;

        set {
            _selectedCheckBoxBladHoogteDec = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding checkboxen Bladgrootte
    // Gemaakt door Warre

    private bool _selectedCheckBoxGrootteOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxGrootteOnbekend
    {
        get => _selectedCheckBoxGrootteOnbekend;
        set
        {
            _selectedCheckBoxGrootteOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrootte5;
    [Clearable<bool>]
    public bool SelectedCheckBoxGrootte5
    {
        get => _selectedCheckBoxGrootte5;
        set
        {
            _selectedCheckBoxGrootte5 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrootte10;
    [Clearable<bool>]
    public bool SelectedCheckBoxGrootte10
    {
        get => _selectedCheckBoxGrootte10;
        set
        {
            _selectedCheckBoxGrootte10 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrootte20;
    [Clearable<bool>]
    public bool SelectedCheckBoxGrootte20
    {
        get => _selectedCheckBoxGrootte20;
        set
        {
            _selectedCheckBoxGrootte20 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrootte50;
    [Clearable<bool>]
    public bool SelectedCheckBoxGrootte50
    {
        get => _selectedCheckBoxGrootte50;
        set
        {
            _selectedCheckBoxGrootte50 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrootte100;
    [Clearable<bool>]
    public bool SelectedCheckBoxGrootte100
    {
        get => _selectedCheckBoxGrootte100;
        set
        {
            _selectedCheckBoxGrootte100 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrootte150;
    [Clearable<bool>]
    public bool SelectedCheckBoxGrootte150
    {
        get=> _selectedCheckBoxGrootte150;
        set
        {
            _selectedCheckBoxGrootte150 = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding checkboxen Spruitfenelogie
    //Gemaakt door Warre

    private bool _selectedCheckBoxSpruitOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxSpruitOnbekend
    {
        get => _selectedCheckBoxSpruitOnbekend;
        set
        {
            _selectedCheckBoxSpruitOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSpruitZomergroen;
    [Clearable<bool>]
    public bool SelectedCheckBoxSpruitZomergroen
    {
        get => _selectedCheckBoxSpruitZomergroen;
        set
        {
            _selectedCheckBoxSpruitZomergroen = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSpruitWintergroen;
    [Clearable<bool>]
    public bool SelectCheckBoxSpruitWintergroen
    {
        get => _selectedCheckBoxSpruitWintergroen;
        set
        {
            _selectedCheckBoxSpruitWintergroen = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSpruitAltijdGroen;
    [Clearable<bool>]
    public bool SelectedCheckBoxSpruitAltijdGroen
    {
        get => _selectedCheckBoxSpruitAltijdGroen;
        set
        {
            _selectedCheckBoxSpruitAltijdGroen = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSpruitVoorjaarsgroen;
    [Clearable<bool>]
    public bool SelectedCheckBoxSpruitVoorjaarsgroen
    {
        get => _selectedCheckBoxSpruitVoorjaarsgroen;
        set
        {
            _selectedCheckBoxSpruitVoorjaarsgroen = value;
            OnPropertyChanged();
        }
    }




    #endregion

    #region Binding checkboxen Bladvormen

    private bool _selectedCheckBoxBladvormenOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenOnbekend
    {
        get => _selectedCheckBoxBladvormenOnbekend;
        set
        {
            _selectedCheckBoxBladvormenOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm1;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm1 {
        get => _selectedCheckBoxBladvormenVorm1;

        set {
            _selectedCheckBoxBladvormenVorm1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm2;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm2 {
        get => _selectedCheckBoxBladvormenVorm2;

        set {
            _selectedCheckBoxBladvormenVorm2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm3;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm3 {
        get => _selectedCheckBoxBladvormenVorm3;

        set {
            _selectedCheckBoxBladvormenVorm3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm4;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm4 {
        get => _selectedCheckBoxBladvormenVorm4;

        set {
            _selectedCheckBoxBladvormenVorm4 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm5;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm5 {
        get => _selectedCheckBoxBladvormenVorm5;

        set {
            _selectedCheckBoxBladvormenVorm5 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm6;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm6 {
        get => _selectedCheckBoxBladvormenVorm6;

        set {
            _selectedCheckBoxBladvormenVorm6 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm7;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm7 {
        get => _selectedCheckBoxBladvormenVorm7;

        set {
            _selectedCheckBoxBladvormenVorm7 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm8;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm8 {
        get => _selectedCheckBoxBladvormenVorm8;

        set {
            _selectedCheckBoxBladvormenVorm8 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBladvormenVorm9;
    [Clearable<bool>]
    public bool SelectedCheckBoxBladvormenVorm9 {
        get => _selectedCheckBoxBladvormenVorm9;

        set {
            _selectedCheckBoxBladvormenVorm9 = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding checkboxen Stengelvormen

    private bool _selectedCheckBoxStengelvormenVorm1;
    [Clearable<bool>]
    public bool SelectedCheckBoxStengelvormenVorm1 {
        get => _selectedCheckBoxStengelvormenVorm1;

        set {
            _selectedCheckBoxStengelvormenVorm1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStengelvormenVorm2;
    [Clearable<bool>]
    public bool SelectedCheckBoxStengelvormenVorm2 {
        get => _selectedCheckBoxStengelvormenVorm2;

        set {
            _selectedCheckBoxStengelvormenVorm2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStengelvormenVorm3;
    [Clearable<bool>]
    public bool SelectedCheckBoxStengelvormenVorm3 {
        get => _selectedCheckBoxStengelvormenVorm3;

        set {
            _selectedCheckBoxStengelvormenVorm3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStengelvormenVorm4;
    [Clearable<bool>]
    public bool SelectedCheckBoxStengelvormenVorm4 {
        get => _selectedCheckBoxStengelvormenVorm4;

        set {
            _selectedCheckBoxStengelvormenVorm4 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStengelvormenVorm5;
    [Clearable<bool>]
    public bool SelectedCheckBoxStengelvormenVorm5 {
        get => _selectedCheckBoxStengelvormenVorm5;

        set {
            _selectedCheckBoxStengelvormenVorm5 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxStengelvormenVorm6;
    [Clearable<bool>]
    public bool SelectedCheckBoxStengelvormenVorm6 {
        get => _selectedCheckBoxStengelvormenVorm6;

        set {
            _selectedCheckBoxStengelvormenVorm6 = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Binding checkboxen Levensvormen

    private bool _selectedCheckBoxLevensvormenOnbekend;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvormenOnbekend
    {
        get => _selectedCheckBoxLevensvormenOnbekend;

        set
        {
            _selectedCheckBoxLevensvormenOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvormenVorm1;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvormenVorm1 {
        get => _selectedCheckBoxLevensvormenVorm1;

        set {
            _selectedCheckBoxLevensvormenVorm1 = value;
            OnPropertyChanged();
        }
    }

    //dubbel
    //private bool _selectedCheckBoxLevensvormenVorm2;

    //public bool SelectedCheckBoxLevensvormenVorm2 {
    //    get => _selectedCheckBoxLevensvormenVorm2;

    //    set {
    //        _selectedCheckBoxLevensvormenVorm2 = value;
    //        OnPropertyChanged();
    //    }
    //}

    private bool _selectedCheckBoxLevensvormenVorm3;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvormenVorm3 {
        get => _selectedCheckBoxLevensvormenVorm3;

        set {
            _selectedCheckBoxLevensvormenVorm3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvormenVorm4;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvormenVorm4 {
        get => _selectedCheckBoxLevensvormenVorm4;

        set {
            _selectedCheckBoxLevensvormenVorm4 = value;
            OnPropertyChanged();
        }
    }

    //dubbel
    //private bool _selectedCheckBoxLevensvormenVorm5;

    //public bool SelectedCheckBoxLevensvormenVorm5 {
    //    get => _selectedCheckBoxLevensvormenVorm5;

    //    set {
    //        _selectedCheckBoxLevensvormenVorm5 = value;
    //        OnPropertyChanged();
    //    }
    //}

    private bool _selectedCheckBoxLevensvormenVorm6;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvormenVorm6 {
        get => _selectedCheckBoxLevensvormenVorm6;

        set {
            _selectedCheckBoxLevensvormenVorm6 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxLevensvormenVorm7;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvormenVorm7 {
        get => _selectedCheckBoxLevensvormenVorm7;

        set {
            _selectedCheckBoxLevensvormenVorm7 = value;
            OnPropertyChanged();
        }
    }

    //dubbel
    //private bool _selectedCheckBoxLevensvormenVorm8;

    //public bool SelectedCheckBoxLevensvormenVorm8 {
    //    get => _selectedCheckBoxLevensvormenVorm8;

    //    set {
    //        _selectedCheckBoxLevensvormenVorm8 = value;
    //        OnPropertyChanged();
    //    }
    //}

    private bool _selectedCheckBoxLevensvormenVorm9;
    [Clearable<bool>]
    public bool SelectedCheckBoxLevensvormenVorm9 {
        get => _selectedCheckBoxLevensvormenVorm9;

        set {
            _selectedCheckBoxLevensvormenVorm9 = value;
            OnPropertyChanged();
        }
    }

    #endregion
}