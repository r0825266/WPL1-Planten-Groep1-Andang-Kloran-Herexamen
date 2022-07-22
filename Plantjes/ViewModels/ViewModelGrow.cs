using System.Collections.Generic;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.Utilities.Attributes;
using Plantjes.ViewModels.Services;

namespace Plantjes.ViewModels; 

public class ViewModelGrow : ViewModelBase {
    private DAOLogic _dao;
    private DetailService _detailService;

    public ViewModelGrow(DetailService detailservice) {
        _detailService = detailservice;
        _dao = DAOLogic.Instance();
        _detailService.SelectedPlantChanged += (sender, plant) =>
        {
            ClearAllFields();

            FillHabitat();
            FillBezonning();
            FillVoedingsbehoefte();
            FillAntagonisch();
            FillGrondsoort();
            FillVochtbehoefte();
        };
    }

    #region Filling elements based on plant selection
    //region written by Marijn

    //function written with Xander's help
    public void FillHabitat()
    {
        //het nemen van modeltype van de huidige viewmodel en een lijst van abiotiekmulti gegevens op basis van geselecteerde plant id
        var modeltype = typeof(ViewModelGrow);
        List<AbiotiekMulti> AbioList =
            DAOAbiotiek.filterAbiotiekMultiFromPlant((int)_detailService.SelectedPlant.PlantId);

        //checkboxes invullen waar nodig
        foreach (AbiotiekMulti abimulti in AbioList)
        {
            string field = "SelectedCheckBoxHabitat";
            if (abimulti.Waarde != null)
            {
                field += abimulti.Waarde;
            }
            else
            {
                field += "Onbekend";
            }
            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    public void FillBezonning()
    {
        var modeltype = typeof(ViewModelGrow);
        List<Abiotiek> abioList = DAOAbiotiek.filterAbiotiekFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Abiotiek abio in abioList)
        {
            string field = "SelectedCheckBoxBezonning";
            switch (abio.Bezonning)
            {
                case "zon":
                    field += "Z";
                    break;
                case "schaduw":
                    field += "S";
                    break;
                case "half schaduw":
                    field += "HS";
                    break;
                case "zon - schaduw":
                    field += "ZS";
                    break;
                case "zon - half schaduw":
                    field += "ZHS";
                    break;
                case "zon - half schaduw - schaduw":
                    field += "ZHSS";
                    break;
                case "half schaduw - schaduw":
                    field += "HSS";
                    break;
                default:
                    field += "Onbekend";
                    break;
            }
            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    public void FillVoedingsbehoefte()
    {
        var modeltype = typeof(ViewModelGrow);
        List<Abiotiek> AbioList =
            DAOAbiotiek.filterAbiotiekFromPlant((int)_detailService.SelectedPlant.PlantId);
        
        foreach (Abiotiek abio in AbioList)
        {
            string field = "SelectedCheckBoxVoedingsbehoefte";
            switch (abio.Voedingsbehoefte)
            {
                case "arm":
                    field += "Arm";
                    break;
                case "arm matig":
                    field += "ArmMatig";
                    break;
                case "matig":
                    field += "Matig";
                    break;
                case "matig voedselrijk":
                    field += "MatigVoedselrijk";
                    break;
                case "voedselrijk":
                    field += "Voedselrijk";
                    break;
                case "voedselrijk indifferent":
                    field += "VoedselrijkIndifferent";
                    break;
                case "indifferent":
                    field += "Indifferent";
                    break;
                default:
                    field += "Onbekend";
                    break;
            }
            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    public void FillAntagonisch()
    {
        var modeltype = typeof(ViewModelGrow);
        List<Abiotiek> AbioList =
            DAOAbiotiek.filterAbiotiekFromPlant((int)_detailService.SelectedPlant.PlantId);
        
        foreach (Abiotiek abio in AbioList)
        {
            string field = "SelectedCheckBoxAntagonisch";
            switch (abio.AntagonischeOmgeving)
            {
                case "geen invloed":
                    field += "GeenInvloed";
                    break;
                case "terugdringen ontwikkeling tot verlies":
                    field += "TerugDringen";
                    break;
                case "gereduceerde groei - bladmassa":
                    field += "Gereduceerd";
                    break;
                case "toename groei - bladmassa":
                    field += "Groei";
                    break;
                default:
                    field += "Onbekend";
                    break;
            }
            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    public void FillGrondsoort()
    {
        var modeltype = typeof(ViewModelGrow);
        List<Abiotiek> AbioList =
            DAOAbiotiek.filterAbiotiekFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Abiotiek abio in AbioList)
        {
            string field = "SelectedCheckBoxGrondSoort";
            switch (abio.Grondsoort)
            {
                case "Z":
                    field += "Z";
                    break;
                case "ZL":
                    field += "ZL";
                    break;
                case "L":
                    field += "L";
                    break;
                case "LK":
                    field += "LK";
                    break;
                case "K":
                    field += "K";
                    break;
                case "ZLK":
                    field += "ZLK";
                    break;
                default:
                    field += "Onbekend";
                    break;
            }
            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    public void FillVochtbehoefte()
    {
        var modeltype = typeof(ViewModelGrow);
        List<Abiotiek> AbioList =
            DAOAbiotiek.filterAbiotiekFromPlant((int)_detailService.SelectedPlant.PlantId);

        foreach (Abiotiek abio in AbioList)
        {
            string field = "SelectedCheckBoxVochtbehoefte";
            switch (abio.Vochtbehoefte)
            {
                case "droog":
                    field += "Droog";
                    break;
                case "droog fris":
                    field += "DroogFris";
                    break;
                case "fris":
                    field += "Fris";
                    break;
                case "fris vochtig":
                    field += "FrisVochtig";
                    break;
                case "vochtig":
                    field += "Vochtig";
                    break;
                case "vochtig nat":
                    field += "VochtigNat";
                    break;
                case "nat":
                    field += "Nat";
                    break;
                default:
                    field += "Onbekend";
                    break;
            }

            var prop = modeltype.GetProperty(field);
            var propsetter = prop.GetSetMethod();
            propsetter.Invoke(this, new object[] { true });
        }
    }

    #endregion
    //geschreven door christophe, op basis van een voorbeeld van owen

    #region CheckboxHabitat

    private bool _selectedCheckBoxHabitatOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOnbekend
    {
        get => _selectedCheckBoxHabitatOnbekend;

        set
        {
            _selectedCheckBoxHabitatOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatGB1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatGB1
    {
        get => _selectedCheckBoxHabitatGB1;

        set {
            _selectedCheckBoxHabitatGB1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatGB2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatGB2
    {
        get => _selectedCheckBoxHabitatGB2;

        set {
            _selectedCheckBoxHabitatGB2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatGB3;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatGB3
    {
        get => _selectedCheckBoxHabitatGB3;

        set {
            _selectedCheckBoxHabitatGB3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatOP1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOP1
    {
        get => _selectedCheckBoxHabitatOP1;

        set {
            _selectedCheckBoxHabitatOP1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatOP1B;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOP1B
    {
        get => _selectedCheckBoxHabitatOP1B;

        set {
            _selectedCheckBoxHabitatOP1B = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatOP2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOP2
    {
        get => _selectedCheckBoxHabitatOP2;

        set {
            _selectedCheckBoxHabitatOP2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatOP2B;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOP2B
    {
        get => _selectedCheckBoxHabitatOP2B;

        set {
            _selectedCheckBoxHabitatOP2B = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatOP3;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOP3
    {
        get => _selectedCheckBoxHabitatOP3;

        set {
            _selectedCheckBoxHabitatOP3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatOP3B;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOP3B
    {
        get => _selectedCheckBoxHabitatOP3B;

        set {
            _selectedCheckBoxHabitatOP3B = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatSH1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatSH1
    {
        get => _selectedCheckBoxHabitatSH1;

        set {
            _selectedCheckBoxHabitatSH1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatSH2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatSH2
    {
        get => _selectedCheckBoxHabitatSH2;

        set {
            _selectedCheckBoxHabitatSH2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatB1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatB1
    {
        get => _selectedCheckBoxHabitatB1;

        set {
            _selectedCheckBoxHabitatB1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatB2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatB2
    {
        get => _selectedCheckBoxHabitatB2;

        set {
            _selectedCheckBoxHabitatB2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatB3;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatB3
    {
        get => _selectedCheckBoxHabitatB3;

        set {
            _selectedCheckBoxHabitatB3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatGR1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatGR1
    {
        get => _selectedCheckBoxHabitatGR1;

        set {
            _selectedCheckBoxHabitatGR1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatGR2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatGR2
    {
        get => _selectedCheckBoxHabitatGR2;

        set {
            _selectedCheckBoxHabitatGR2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatH1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatH1
    {
        get => _selectedCheckBoxHabitatH1;

        set {
            _selectedCheckBoxHabitatH1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatH2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatH2
    {
        get => _selectedCheckBoxHabitatH2;

        set {
            _selectedCheckBoxHabitatH2 = value;
            OnPropertyChanged();
        }
    }

    //-------------------------------------------------------------

    private bool _selectedCheckBoxHabitatST1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatST1
    {
        get => _selectedCheckBoxHabitatST1;

        set {
            _selectedCheckBoxHabitatST1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatST2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatST2
    {
        get => _selectedCheckBoxHabitatST2;

        set {
            _selectedCheckBoxHabitatST2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatBR1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatBR1
    {
        get => _selectedCheckBoxHabitatBR1;

        set {
            _selectedCheckBoxHabitatBR1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatBR2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatBR2
    {
        get => _selectedCheckBoxHabitatBR2;

        set {
            _selectedCheckBoxHabitatBR2 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatBR3;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatBR3
    {
        get => _selectedCheckBoxHabitatBR3;

        set {
            _selectedCheckBoxHabitatBR3 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatOB1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOB1
    {
        get => _selectedCheckBoxHabitatOB1;

        set {
            _selectedCheckBoxHabitatOB1 = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxHabitatOB2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatOB2
    {
        get => _selectedCheckBoxHabitatOB2;

        set {
            _selectedCheckBoxHabitatOB2 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatA1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatA1
    {
        get => _selectedCheckBoxHabitatA1;

        set
        {
            _selectedCheckBoxHabitatA1 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatA2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatA2
    {
        get => _selectedCheckBoxHabitatA2;

        set
        {
            _selectedCheckBoxHabitatA2 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatM1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatM1
    {
        get => _selectedCheckBoxHabitatM1;

        set
        {
            _selectedCheckBoxHabitatM1 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatM2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatM2
    {
        get => _selectedCheckBoxHabitatM2;

        set
        {
            _selectedCheckBoxHabitatM2 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatO4;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatO4
    {
        get => _selectedCheckBoxHabitatO4;

        set
        {
            _selectedCheckBoxHabitatO4 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatO5;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatO5
    {
        get => _selectedCheckBoxHabitatO5;

        set
        {
            _selectedCheckBoxHabitatO5 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatSV1;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatSV1
    {
        get => _selectedCheckBoxHabitatSV1;

        set
        {
            _selectedCheckBoxHabitatSV1 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatSV2;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatSV2
    {
        get => _selectedCheckBoxHabitatSV2;

        set
        {
            _selectedCheckBoxHabitatSV2 = value;
            OnPropertyChanged();
        }
    }
    private bool _selectedCheckBoxHabitatSV3;

    [Clearable<bool>]
    public bool SelectedCheckBoxHabitatSV3
    {
        get => _selectedCheckBoxHabitatSV3;

        set
        {
            _selectedCheckBoxHabitatSV3 = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Bezonning
    private bool _selectedCheckBoxBezonningOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxBezonningOnbekend
    {
        get => _selectedCheckBoxBezonningOnbekend;
        set
        {
            _selectedCheckBoxBezonningOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBezonningZ;

    [Clearable<bool>]
    public bool SelectedCheckBoxBezonningZ
    {
        get => _selectedCheckBoxBezonningZ;
        set
        {
            _selectedCheckBoxBezonningZ = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBezonningS;

    [Clearable<bool>]
    public bool SelectedCheckBoxBezonningS
    {
        get => _selectedCheckBoxBezonningS;
        set
        {
            _selectedCheckBoxBezonningS = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBezonningHS;

    [Clearable<bool>]
    public bool SelectedCheckBoxBezonningHS
    {
        get => _selectedCheckBoxBezonningHS;
        set
        {
            _selectedCheckBoxBezonningHS = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBezonningZS;

    [Clearable<bool>]
    public bool SelectedCheckBoxBezonningZS
    {
        get => _selectedCheckBoxBezonningZS;
        set
        {
            _selectedCheckBoxBezonningZS = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBezonningZHS;

    [Clearable<bool>]
    public bool SelectedCheckBoxBezonningZHS
    {
        get => _selectedCheckBoxBezonningZHS;
        set
        {
            _selectedCheckBoxBezonningZHS = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBezonningZHSS;

    [Clearable<bool>]
    public bool SelectCheckBoxBezonningZHSS
    {
        get => _selectedCheckBoxBezonningZHSS;
        set
        {
            _selectedCheckBoxBezonningZHSS = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxBezonningHSS;

    [Clearable<bool>]
    public bool SelectedCheckBoxBezonningHSS
    {
        get => _selectedCheckBoxBezonningHSS;
        set
        {
            _selectedCheckBoxBezonningHSS = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region CheckboxVoedingsbehoefte

    private bool _selectedCheckBoxVoedingsbehoefteOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxVoedingsbehoefteOnbekend
    {
        get => _selectedCheckBoxVoedingsbehoefteOnbekend;

        set
        {
            _selectedCheckBoxVoedingsbehoefteOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVoedingsbehoefteArm;

    [Clearable<bool>]
    public bool SelectedCheckBoxVoedingsbehoefteArm {
        get => _selectedCheckBoxVoedingsbehoefteArm;

        set {
            _selectedCheckBoxVoedingsbehoefteArm = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVoedingsbehoefteArmMatig;

    [Clearable<bool>]
    public bool SelectedCheckBoxVoedingsbehoefteArmMatig {
        get => _selectedCheckBoxVoedingsbehoefteArmMatig;

        set {
            _selectedCheckBoxVoedingsbehoefteArmMatig = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVoedingsbehoefteMatig;

    [Clearable<bool>]
    public bool SelectedCheckBoxVoedingsbehoefteMatig {
        get => _selectedCheckBoxVoedingsbehoefteMatig;

        set {
            _selectedCheckBoxVoedingsbehoefteMatig = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk;

    [Clearable<bool>]
    public bool SelectedCheckBoxVoedingsbehoefteMatigVoedselrijk {
        get => _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk;

        set {
            _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVoedingsbehoefteVoedselrijk;

    [Clearable<bool>]
    public bool SelectedCheckBoxVoedingsbehoefteVoedselrijk {
        get => _selectedCheckBoxVoedingsbehoefteVoedselrijk;

        set {
            _selectedCheckBoxVoedingsbehoefteVoedselrijk = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent;

    [Clearable<bool>]
    public bool SelectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent {
        get => _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent;

        set {
            _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVoedingsbehoefteIndifferent;

    [Clearable<bool>]
    public bool SelectedCheckBoxVoedingsbehoefteIndifferent {
        get => _selectedCheckBoxVoedingsbehoefteIndifferent;

        set {
            _selectedCheckBoxVoedingsbehoefteIndifferent = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region CheckboxVochtBehoefte

    private bool _selectedCheckBoxVochtbehoefteOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxVochtbehoefteOnbekend
    {
        get => _selectedCheckBoxVochtbehoefteOnbekend;

        set
        {
            _selectedCheckBoxVochtbehoefteOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVochtbehoefteDroog;

    [Clearable<bool>]
    public bool SelectedCheckBoxVochtbehoefteDroog {
        get => _selectedCheckBoxVochtbehoefteDroog;

        set {
            _selectedCheckBoxVochtbehoefteDroog = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVochtbehoefteDroogFris;

    [Clearable<bool>]
    public bool SelectedCheckBoxVochtbehoefteDroogFris {
        get => _selectedCheckBoxVochtbehoefteDroogFris;

        set {
            _selectedCheckBoxVochtbehoefteDroogFris = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVochtbehoefteFris;

    [Clearable<bool>]
    public bool SelectedCheckBoxVochtbehoefteFris {
        get => _selectedCheckBoxVochtbehoefteFris;

        set {
            _selectedCheckBoxVochtbehoefteFris = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVochtbehoefteFrisVochtig;

    [Clearable<bool>]
    public bool SelectedCheckBoxVochtbehoefteFrisVochtig {
        get => _selectedCheckBoxVochtbehoefteFrisVochtig;

        set {
            _selectedCheckBoxVochtbehoefteFrisVochtig = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVochtbehoefteVochtig;

    [Clearable<bool>]
    public bool SelectedCheckBoxVochtbehoefteVochtig {
        get => _selectedCheckBoxVochtbehoefteVochtig;

        set {
            _selectedCheckBoxVochtbehoefteVochtig = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVochtbehoefteVochtigNat;

    [Clearable<bool>]
    public bool SelectedCheckBoxVochtbehoefteVochtigNat {
        get => _selectedCheckBoxVochtbehoefteVochtigNat;

        set {
            _selectedCheckBoxVochtbehoefteVochtigNat = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxVochtbehoefteNat;

    [Clearable<bool>]
    public bool SelectedCheckBoxVochtbehoefteNat {
        get => _selectedCheckBoxVochtbehoefteNat;

        set {
            _selectedCheckBoxVochtbehoefteNat = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Antagonische omgeving
    //Gemaakt door Warre
    private bool _selectedCheckBoxAntagonischOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxAntagonischOnbekend
    {
        get => _selectedCheckBoxAntagonischOnbekend;
        set
        {
            _selectedCheckBoxAntagonischOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxAntagonischGeenInvloed;

    [Clearable<bool>]
    public bool SelectedCheckBoxAntagonischGeenInvloed
    {
        get => _selectedCheckBoxAntagonischGeenInvloed;
        set
        {
            _selectedCheckBoxAntagonischGeenInvloed = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxAntagonischTerugDringen;

    [Clearable<bool>]
    public bool SelectedCheckBoxAntagonischTerugDringen
    {
        get => _selectedCheckBoxAntagonischTerugDringen;
        set
        {
            _selectedCheckBoxAntagonischTerugDringen = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxAntagonischGereduceerd;

    [Clearable<bool>]
    public bool SelectedCheckBoxAntagonischGereduceerd
    {
        get => _selectedCheckBoxAntagonischGereduceerd;
        set
        {
            _selectedCheckBoxAntagonischGereduceerd = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxAntagonischGroei;

    [Clearable<bool>]
    public bool SelectedCheckBoxAntagonischGroei
    {
        get => _selectedCheckBoxAntagonischGroei;
        set
        {
            _selectedCheckBoxAntagonischGroei = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region Grondsoort
    //Gemaakt door Warre
    private bool _selectedCheckBoxGrondSoortOnbekend;

    [Clearable<bool>]
    public bool SelectedCheckBoxGrondSoortOnbekend
    {
        get => _selectedCheckBoxGrondSoortOnbekend;
        set
        {
            _selectedCheckBoxGrondSoortOnbekend = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrondSoortZ;

    [Clearable<bool>]
    public bool SelectedCheckBoxGrondSoortZ
    {
        get => _selectedCheckBoxGrondSoortZ;
        set
        {
            _selectedCheckBoxGrondSoortZ = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrondSoortZL;

    [Clearable<bool>]
    public bool SelectedCheckBoxGrondSoortZL
    {
        get => _selectedCheckBoxGrondSoortZL;
        set
        {
            _selectedCheckBoxGrondSoortZL = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrondSoortL;

    [Clearable<bool>]
    public bool SelectedCheckBoxGrondSoortL
    {
        get => _selectedCheckBoxGrondSoortL;
        set
        {
            _selectedCheckBoxGrondSoortL = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrondSoortLK;

    [Clearable<bool>]
    public bool SelectedCheckBoxGrondSoortLK
    {
        get => _selectedCheckBoxGrondSoortLK;
        set
        {
            _selectedCheckBoxGrondSoortLK = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrondSoortK;

    [Clearable<bool>]
    public bool SelectedCheckBoxGrondSoortK
    {
        get => _selectedCheckBoxGrondSoortK;
        set
        {
            _selectedCheckBoxGrondSoortK = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxGrondSoortZLK;

    [Clearable<bool>]
    public bool SelectedCheckBoxGrondSoortZLK
    {
        get => _selectedCheckBoxGrondSoortZLK;
        set
        {
            _selectedCheckBoxGrondSoortZLK = value;
            OnPropertyChanged();
        }
    }

    #endregion
}