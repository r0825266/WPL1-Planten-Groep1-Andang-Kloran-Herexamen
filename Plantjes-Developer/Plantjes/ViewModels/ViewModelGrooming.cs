using System.Collections.ObjectModel;
using System.Linq;
using Plantjes.Dao;
using Plantjes.ViewModels.Services;

namespace Plantjes.ViewModels;

public class ViewModelGrooming : ViewModelBase
{
    private DAOLogic _dao;

    private string _selectedBeheerdaad;


    public ViewModelGrooming(DetailService detailservice)
    {
        _dao = DAOLogic.Instance();

        cmbBeheerdaad = new ObservableCollection<string>();

        fillComboBoxBeheerdaad();
    }

    //geschreven door christophe, op basis van een voorbeeld van owen
    public ObservableCollection<string> cmbBeheerdaad { get; set; }

    public string SelectedBeheer_Maand
    {
        get => _selectedBeheerdaad;
        set
        {
            _selectedBeheerdaad = value;
            OnPropertyChanged();
        }
    }
    //Aangepast door Warre
    
    public void fillComboBoxBeheerdaad()
    {
        var list = DAOBeheerMaand.FillBeheerdaad().ToList();


        foreach (var item in list)
            //if (item != null)
            //{
            cmbBeheerdaad.Add(item.Beheerdaad);
        //}
    }

    #region Binding checkboxen Beheerdaad maand

    private bool _selectedCheckBoxJan;

    public bool SelectedCheckBoxJan
    {
        get => _selectedCheckBoxJan;

        set
        {
            _selectedCheckBoxJan = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxFeb;

    public bool SelectedCheckBoxFeb
    {
        get => _selectedCheckBoxFeb;

        set
        {
            _selectedCheckBoxFeb = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxMar;

    public bool SelectedCheckBoxMar
    {
        get => _selectedCheckBoxMar;

        set
        {
            _selectedCheckBoxMar = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxApr;

    public bool SelectedCheckBoxApr
    {
        get => _selectedCheckBoxApr;

        set
        {
            _selectedCheckBoxApr = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxMay;

    public bool SelectedCheckBoxFMay
    {
        get => _selectedCheckBoxMay;

        set
        {
            _selectedCheckBoxMay = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxJun;

    public bool SelectedCheckBoxJun
    {
        get => _selectedCheckBoxJun;

        set
        {
            _selectedCheckBoxJun = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxJul;

    public bool SelectedCheckBoxJul
    {
        get => _selectedCheckBoxJul;

        set
        {
            _selectedCheckBoxJul = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxAug;

    public bool SelectedCheckBoxAug
    {
        get => _selectedCheckBoxAug;

        set
        {
            _selectedCheckBoxAug = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxSep;

    public bool SelectedCheckBoxSep
    {
        get => _selectedCheckBoxSep;

        set
        {
            _selectedCheckBoxSep = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxOct;

    public bool SelectedCheckBoxOct
    {
        get => _selectedCheckBoxOct;

        set
        {
            _selectedCheckBoxOct = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxNov;

    public bool SelectedCheckBoxNov
    {
        get => _selectedCheckBoxNov;

        set
        {
            _selectedCheckBoxNov = value;
            OnPropertyChanged();
        }
    }

    private bool _selectedCheckBoxDec;

    public bool SelectedCheckBoxDec
    {
        get => _selectedCheckBoxDec;

        set
        {
            _selectedCheckBoxDec = value;
            OnPropertyChanged();
        }
    }

    #endregion
}