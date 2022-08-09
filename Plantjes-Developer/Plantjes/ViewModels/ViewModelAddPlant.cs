using Microsoft.Toolkit.Mvvm.Input;
using Plantjes.ViewModels.Services;
using Plantjes.Views.Home;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Plantjes.Models.Db;
using Plantjes.ViewModels.HelpClasses;
using Plantjes.ViewModels.Services;
using Plantjes.Views.Home;
using Plantjes.Views.UserControls;

namespace Plantjes.ViewModels {
    public class ViewModelAddPlant : ViewModelBase {

        // Modified and added to by Andang Kloran

        //private ServiceProvider _serviceProvider;
        private readonly SearchService _searchService = (SearchService)App.Current.Services.GetService(typeof(SearchService));
        private readonly DetailService _detailService = (DetailService)App.Current.Services.GetService(typeof(DetailService));

        public ViewModelAddPlant(SearchService searchService, DetailService detailservice)
        {
            _searchService = searchService;
            //_searchService = new SearchService();
            _detailService = detailservice;

            //Observable Collections 
            ////Obserbable collections to fill with the necessary objects to show in the comboboxes
            cmbTypes = new ObservableCollection<TfgsvType>();
            cmbFamilies = new ObservableCollection<TfgsvFamilie>();
            cmbGeslacht = new ObservableCollection<TfgsvGeslacht>();
            cmbSoort = new ObservableCollection<TfgsvSoort>();
            cmbVariant = new ObservableCollection<TfgsvVariant>();
            cmbRatioBladBloei = new ObservableCollection<Fenotype>();

            ////Observable Collections to bind to listboxes
            filteredPlantResults = new ObservableCollection<Plant>();
            detailsSelectedPlant = new ObservableCollection<string>();

            //ICommands
            ////These will be used to bind our buttons in the xaml and to give them functionality
            cancelCommand = new RelayCommand(cancelButton);

            //These comboboxes will already be filled with data on startup
            fillComboboxes();

            _viewModelRepo2 = (ViewModelRepo2)App.Current.Services.GetService(typeof(ViewModelRepo2));
            mainNavigationCommand = new MyICommand<string>(_onNavigationChanged);
        }


        #region viewmodel things
        public MyICommand<string> mainNavigationCommand { get; set; }
        private ViewModelBase _currentViewModel;

        private readonly ViewModelRepo2 _viewModelRepo2;
        private void _onNavigationChanged(string userControlName)
        {
            currentViewModel = _viewModelRepo2.GetViewModel(userControlName);
        }

        public ViewModelBase currentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        #endregion
        //Observable collections
        ////Bind to comboboxes
        public ObservableCollection<TfgsvType> cmbTypes { get; set; }
        public ObservableCollection<TfgsvFamilie> cmbFamilies { get; set; }
        public ObservableCollection<TfgsvGeslacht> cmbGeslacht { get; set; }
        public ObservableCollection<TfgsvSoort> cmbSoort { get; set; }
        public ObservableCollection<TfgsvVariant> cmbVariant { get; set; }
        public ObservableCollection<Fenotype> cmbRatioBladBloei { get; set; }

        ////Bind to ListBoxes
        public ObservableCollection<Plant> filteredPlantResults { get; set; }

        public ObservableCollection<string> detailsSelectedPlant { get; set; }

        //geschreven door owen
        public void FillAllImages()
        {
            ImageBlad = _searchService.GetImageLocation("blad", SelectedPlantInResult);
            ImageBloei = _searchService.GetImageLocation("bloei", SelectedPlantInResult);
            ImageHabitus = _searchService.GetImageLocation("habitus", SelectedPlantInResult);
        }

        //written by kenny (region)

        #region tussenFunctie voor knoppen met parameters

        public void fillComboboxes()
        {
            _searchService.fillComboBoxType(cmbTypes);
            _searchService.fillComboBoxFamilie(SelectedType, cmbFamilies);
            _searchService.fillComboBoxGeslacht(SelectedFamilie, cmbGeslacht);
            _searchService.fillComboBoxSoort(SelectedGeslacht, cmbSoort);
            _searchService.fillComboBoxVariant(cmbVariant);
            _searchService.fillComboBoxRatioBloeiBlad(cmbRatioBladBloei);
        }

        #endregion


        #region RelayCommands

        //RelayCommands
        private RelayCommand _cancelCommand;
        
        #endregion

        //geschreven door owen en robin

        #region Selected Item variables for each combobox

        private TfgsvType _selectedType;

        public TfgsvType SelectedType
        {
            get => _selectedType; 
            set
            {
                _selectedType = value;

                cmbFamilies.Clear();
                cmbGeslacht.Clear();
                cmbSoort.Clear();
                cmbVariant.Clear();

                _searchService.fillComboBoxFamilie(SelectedType, cmbFamilies);
                OnPropertyChanged();
            }
        }

        private TfgsvFamilie _selectedFamilie;

        public TfgsvFamilie SelectedFamilie
        {
            get => _selectedFamilie;
            set
            {
                _selectedFamilie = value;


                cmbGeslacht.Clear();
                cmbSoort.Clear();
                cmbVariant.Clear();

                _searchService.fillComboBoxGeslacht(SelectedFamilie, cmbGeslacht);
                OnPropertyChanged();
            }
        }

        private TfgsvGeslacht _selectedGeslacht;

        public TfgsvGeslacht SelectedGeslacht
        {
            get => _selectedGeslacht;
            set
            {
                _selectedGeslacht = value;


                cmbSoort.Clear();
                cmbVariant.Clear();

                _searchService.fillComboBoxSoort(SelectedGeslacht, cmbSoort);
                OnPropertyChanged();
            }
        }

        private TfgsvSoort _selectedSoort;

        public TfgsvSoort SelectedSoort
        {
            get => _selectedSoort;
            set
            {
                _selectedSoort = value;

                cmbVariant.Clear();

                OnPropertyChanged();
            }
        }

        private TfgsvVariant _selectedVariant;

        public TfgsvVariant SelectedVariant
        {
            get => _selectedVariant;
            set
            {
                _selectedVariant = value;
                OnPropertyChanged();
            }
        }

        private string _selectedRatioBloeiBlad;

        public string SelectedRatioBloeiBlad
        {
            get => _selectedRatioBloeiBlad;
            set
            {
                _selectedRatioBloeiBlad = value;
                OnPropertyChanged();
            }
        }

        private string _selectedNederlandseNaam;

        public string SelectedNederlandseNaam
        {
            get => _selectedNederlandseNaam;
            set
            {
                if (SelectedNederlandseNaam == "")
                    _selectedNederlandseNaam = null;
                else
                    _selectedNederlandseNaam = value;

                OnPropertyChanged();
            }
        }

        //This will update the selected plant in the result listbox
        //This will be used to show the selected plant details
        private Plant _selectedPlantInResult;

        public Plant SelectedPlantInResult
        {
            get => _selectedPlantInResult;
            set
            {
                _selectedPlantInResult = value;
                FillAllImages();
                OnPropertyChanged();
                _searchService.FillDetailPlantResult(detailsSelectedPlant, SelectedPlantInResult);
                _detailService.SelectedPlant = SelectedPlantInResult;

                //Make the currently selected plant in the Result list available in the SearchService
            }
        }

        #endregion

        #region binding images

        private ImageSource _imageBloei;

        public ImageSource ImageBloei
        {
            get => _imageBloei;
            set
            {
                _imageBloei = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _imageHabitus;

        public ImageSource ImageHabitus
        {
            get => _imageHabitus;
            set
            {
                _imageHabitus = value;
                OnPropertyChanged();
            }
        }

        private ImageSource _imageBlad;

        public ImageSource ImageBlad
        {
            get => _imageBlad;
            set
            {
                _imageBlad = value;
                OnPropertyChanged();
            }
        }

        #endregion


       // private RelayCommand _cancelCommand;

        //Defining variables for checkboxes
        private bool _selectCheckBoxSpruitWintergroen;
        private bool _selectedCheckBox1;
        private bool _selectedCheckBox2;
        private bool _selectedCheckBox3;
        private bool _selectedCheckBox4;
        private bool _selectedCheckBox5;
        private bool _selectedCheckBox6;
        private bool _selectedCheckBox7;
        private bool _selectedCheckBox8;
        private bool _selectedCheckBox9;
        private bool _selectedCheckBoxAntagonischGeenInvloed;
        private bool _selectedCheckBoxAntagonischGereduceerd;
        private bool _selectedCheckBoxAntagonischGroei;
        private bool _selectedCheckBoxAntagonischOnbekend;
        private bool _selectedCheckBoxAntagonischTerugDringen;
        private bool _selectedCheckBoxApr;
        private bool _selectedCheckBoxAug;
        private bool _selectedCheckBoxBezonningHs;
        private bool _selectedCheckBoxBezonningHs1;
        private bool _selectedCheckBoxBezonningHss;
        private bool _selectedCheckBoxBezonningOnbekend;
        private bool _selectedCheckBoxBezonningS;
        private bool _selectedCheckBoxBezonningZ;
        private bool _selectedCheckBoxBezonningZ1;
        private bool _selectedCheckBoxBezonningZhs;
        private bool _selectedCheckBoxBezonningZhs1;
        private bool _selectedCheckBoxBezonningZhss;
        private bool _selectedCheckBoxBezonningZs;
        private bool _selectedCheckBoxBijvriendelijk;
        private bool _selectedCheckBoxBladkleurBlauw;
        private bool _selectedCheckBoxBladkleurBruin;
        private bool _selectedCheckBoxBladkleurGeel;
        private bool _selectedCheckBoxBladkleurGrijs;
        private bool _selectedCheckBoxBladkleurGroen;
        private bool _selectedCheckBoxBladkleurLila;
        private bool _selectedCheckBoxBladkleurOnbekend;
        private bool _selectedCheckBoxBladkleurOnbekend1;
        private bool _selectedCheckBoxBladkleurOnbekend2;
        private bool _selectedCheckBoxBladkleurOranje;
        private bool _selectedCheckBoxBladkleurPaars;
        private bool _selectedCheckBoxBladkleurPaars1;
        private bool _selectedCheckBoxBladkleurRood;
        private bool _selectedCheckBoxBladkleurRosé;
        private bool _selectedCheckBoxBladkleurWit;
        private bool _selectedCheckBoxBladkleurZwart;
        private bool _selectedCheckBoxBladvormenVorm1;
        private bool _selectedCheckBoxBladvormenVorm2;
        private bool _selectedCheckBoxBladvormenVorm3;
        private bool _selectedCheckBoxBladvormenVorm4;
        private bool _selectedCheckBoxBladvormenVorm5;
        private bool _selectedCheckBoxBladvormenVorm6;
        private bool _selectedCheckBoxBladvormenVorm7;
        private bool _selectedCheckBoxBladvormenVorm8;
        private bool _selectedCheckBoxBladvormenVorm9;
        private bool _selectedCheckBoxBloeiHoogteApr;
        private bool _selectedCheckBoxBloeiHoogteAug;
        private bool _selectedCheckBoxBloeiHoogteDec;
        private bool _selectedCheckBoxBloeiHoogteFeb;
        private bool _selectedCheckBoxBloeiHoogteJan;
        private bool _selectedCheckBoxBloeiHoogteJul;
        private bool _selectedCheckBoxBloeiHoogteJun;
        private bool _selectedCheckBoxBloeiHoogteMar;
        private bool _selectedCheckBoxBloeiHoogteMay;
        private bool _selectedCheckBoxBloeiHoogteNov;
        private bool _selectedCheckBoxBloeiHoogteOct;
        private bool _selectedCheckBoxBloeiHoogteOnbekend;
        private bool _selectedCheckBoxBloeiHoogteSep;
        private bool _selectedCheckBoxBloeikleurBlauw;
        private bool _selectedCheckBoxBloeikleurBruin;
        private bool _selectedCheckBoxBloeikleurGeel;
        private bool _selectedCheckBoxBloeikleurGrijs;
        private bool _selectedCheckBoxBloeikleurGroen;
        private bool _selectedCheckBoxBloeikleurLila;
        private bool _selectedCheckBoxBloeikleurOnbekend;
        private bool _selectedCheckBoxBloeikleurOranje;
        private bool _selectedCheckBoxBloeikleurPaars;
        private bool _selectedCheckBoxBloeikleurRood;
        private bool _selectedCheckBoxBloeikleurRosé;
        private bool _selectedCheckBoxBloeikleurViolet;
        private bool _selectedCheckBoxBloeikleurWit;
        private bool _selectedCheckBoxBloeikleurZwart;
        private bool _selectedCheckBoxBloeitInApr;
        private bool _selectedCheckBoxBloeitInAug;
        private bool _selectedCheckBoxBloeitInDec;
        private bool _selectedCheckBoxBloeitInFeb;
        private bool _selectedCheckBoxBloeitInJan;
        private bool _selectedCheckBoxBloeitInJul;
        private bool _selectedCheckBoxBloeitInJun;
        private bool _selectedCheckBoxBloeitInMar;
        private bool _selectedCheckBoxBloeitInMay;
        private bool _selectedCheckBoxBloeitInNov;
        private bool _selectedCheckBoxBloeitInOct;
        private bool _selectedCheckBoxBloeitInOnbekend;
        private bool _selectedCheckBoxBloeitInSep;
        private bool _selectedCheckBoxBloeiwijzeVorm1;
        private bool _selectedCheckBoxBloeiwijzeVorm2;
        private bool _selectedCheckBoxBloeiwijzeVorm3;
        private bool _selectedCheckBoxBloeiwijzeVorm4;
        private bool _selectedCheckBoxBloeiwijzeVorm5;
        private bool _selectedCheckBoxBloeiwijzeVorm6;
        private bool _selectedCheckBoxBloeiwijzeVormOnbekend;
        private bool _selectedCheckBoxDec;
        private bool _selectedCheckBoxEetbaarKruidbaar;
        private bool _selectedCheckBoxFeb;
        private bool _selectedCheckBoxGeurend;
        private bool _selectedCheckBoxGrondSoortK;
        private bool _selectedCheckBoxGrondSoortL;
        private bool _selectedCheckBoxGrondSoortLk;
        private bool _selectedCheckBoxGrondSoortOnbekend;
        private bool _selectedCheckBoxGrondSoortZ;
        private bool _selectedCheckBoxGrondSoortZl;
        private bool _selectedCheckBoxGrondSoortZlk;
        private bool _selectedCheckBoxGrootte100;
        private bool _selectedCheckBoxGrootte10;
        private bool _selectedCheckBoxGrootte150;
        private bool _selectedCheckBoxGrootte20;
        private bool _selectedCheckBoxGrootte50;
        private bool _selectedCheckBoxGrootte5;
        private bool _selectedCheckBoxGrootteOnbekend;
        private bool _selectedCheckBoxGrootteOnbekend1;
        private bool _selectedCheckBoxHabitatA1;
        private bool _selectedCheckBoxHabitatA2;
        private bool _selectedCheckBoxHabitatB1;
        private bool _selectedCheckBoxHabitatB2;
        private bool _selectedCheckBoxHabitatB3;
        private bool _selectedCheckBoxHabitatBr1;
        private bool _selectedCheckBoxHabitatBr2;
        private bool _selectedCheckBoxHabitatBr3;
        private bool _selectedCheckBoxHabitatGb1;
        private bool _selectedCheckBoxHabitatGb2;
        private bool _selectedCheckBoxHabitatGb3;
        private bool _selectedCheckBoxHabitatGr1;
        private bool _selectedCheckBoxHabitatGr2;
        private bool _selectedCheckBoxHabitatH1;
        private bool _selectedCheckBoxHabitatH2;
        private bool _selectedCheckBoxHabitatM1;
        private bool _selectedCheckBoxHabitatM2;
        private bool _selectedCheckBoxHabitatO4;
        private bool _selectedCheckBoxHabitatO5;
        private bool _selectedCheckBoxHabitatOb1;
        private bool _selectedCheckBoxHabitatOb2;
        private bool _selectedCheckBoxHabitatOnbekend;
        private bool _selectedCheckBoxHabitatOp1B;
        private bool _selectedCheckBoxHabitatOp1;
        private bool _selectedCheckBoxHabitatOp2B;
        private bool _selectedCheckBoxHabitatOp2;
        private bool _selectedCheckBoxHabitatOp3B;
        private bool _selectedCheckBoxHabitatOp3;
        private bool _selectedCheckBoxHabitatSh1;
        private bool _selectedCheckBoxHabitatSh2;
        private bool _selectedCheckBoxHabitatSt1;
        private bool _selectedCheckBoxHabitatSt2;
        private bool _selectedCheckBoxHabitatSv1;
        private bool _selectedCheckBoxHabitatSv2;
        private bool _selectedCheckBoxHabitatSv3;
        private bool _selectedCheckBoxJan;
        private bool _selectedCheckBoxJul;
        private bool _selectedCheckBoxJun;
        private bool _selectedCheckBoxLevensvormenVorm1;
        private bool _selectedCheckBoxLevensvormenVorm2;
        private bool _selectedCheckBoxLevensvormenVorm3;
        private bool _selectedCheckBoxLevensvormenVorm4;
        private bool _selectedCheckBoxLevensvormenVorm5;
        private bool _selectedCheckBoxLevensvormenVorm6;
        private bool _selectedCheckBoxLevensvormenVorm7;
        private bool _selectedCheckBoxLevensvormenVorm8;
        private bool _selectedCheckBoxLevensvormenVorm9;
        private bool _selectedCheckBoxMar;
        private bool _selectedCheckBoxMay;
        private bool _selectedCheckBoxNov;
        private bool _selectedCheckBoxOct;
        private bool _selectedCheckBoxRatioAster;
        private bool _selectedCheckBoxRatioGeranium;
        private bool _selectedCheckBoxRatioOnbekend;
        private bool _selectedCheckBoxRatioPachysandra;
        private bool _selectedCheckBoxRatioSalvia;
        private bool _selectedCheckBoxSep;
        private bool _selectedCheckBoxSociabiliteitI;
        private bool _selectedCheckBoxSociabiliteitIi;
        private bool _selectedCheckBoxSociabiliteitIii;
        private bool _selectedCheckBoxSociabiliteitIv;
        private bool _selectedCheckBoxSociabiliteitV;
        private bool _selectedCheckBoxSpruitAltijdGroen;
        private bool _selectedCheckBoxSpruitVoorjaarsgroen;
        private bool _selectedCheckBoxSpruitZomergroen;
        private bool _selectedCheckBoxStengelvormenVorm1;
        private bool _selectedCheckBoxStengelvormenVorm2;
        private bool _selectedCheckBoxStengelvormenVorm3;
        private bool _selectedCheckBoxStengelvormenVorm4;
        private bool _selectedCheckBoxStengelvormenVorm5;
        private bool _selectedCheckBoxStengelvormenVorm6;
        private bool _selectedCheckBoxStrategieC;
        private bool _selectedCheckBoxStrategieCr;
        private bool _selectedCheckBoxStrategieCsr;
        private bool _selectedCheckBoxStrategieR;
        private bool _selectedCheckBoxStrategieRs;
        private bool _selectedCheckBoxStrategieSc;
        private bool _selectedCheckBoxStrategieS;
        private bool _selectedCheckBoxVlindervriendelijk;
        private bool _selectedCheckBoxVochtbehoefteDroogFris;
        private bool _selectedCheckBoxVochtbehoefteDroog;
        private bool _selectedCheckBoxVochtbehoefteFris;
        private bool _selectedCheckBoxVochtbehoefteFrisVochtig;
        private bool _selectedCheckBoxVochtbehoefteNat;
        private bool _selectedCheckBoxVochtbehoefteOnbekend;
        private bool _selectedCheckBoxVochtbehoefteVochtig;
        private bool _selectedCheckBoxVochtbehoefteVochtigNat;
        private bool _selectedCheckBoxVoedingsbehoefteArm;
        private bool _selectedCheckBoxVoedingsbehoefteArmMatig;
        private bool _selectedCheckBoxVoedingsbehoefteIndifferent;
        private bool _selectedCheckBoxVoedingsbehoefteMatig;
        private bool _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk;
        private bool _selectedCheckBoxVoedingsbehoefteOnbekend;
        private bool _selectedCheckBoxVoedingsbehoefteVoedselrijk;
        private bool _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent;
        private bool _selectedCheckBoxVorstgevoelig;

        //Defining variables for Textboxes
        private string _typeInput;
        private string _familieInput;
        private string _geslachtInput;
        private string _naamInput;
        public string _soortInput;
        public string _variantInput;
        public string _ratioBladBloeiInput;
        public string _opmerkingInput;
        public string _frequenciePerJaarInput;

        private string _errorMessage;





        //public ViewModelAddPlant() {
        //    cancelCommand = new RelayCommand(cancelButton);
        //}

        public RelayCommand cancelCommand
        {
            get => _cancelCommand;
            set => _cancelCommand = value;
        }

        //Annuleren button closes the current window and reopens the main window
        public void cancelButton()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0]?.Close();
        }

        //Bindings CheckBoxes
        public bool SelectCheckBoxSpruitWintergroen
        {
            get => _selectCheckBoxSpruitWintergroen;
            set
            {
                _selectCheckBoxSpruitWintergroen = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox1
        {
            get => _selectedCheckBox1;
            set
            {
                _selectedCheckBox1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox2
        {
            get => _selectedCheckBox2;
            set
            {
                _selectedCheckBox2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox3
        {
            get => _selectedCheckBox3;
            set
            {
                _selectedCheckBox3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox4
        {
            get => _selectedCheckBox4;
            set
            {
                _selectedCheckBox4 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox5
        {
            get => _selectedCheckBox5;
            set
            {
                _selectedCheckBox5 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox6
        {
            get => _selectedCheckBox6;
            set
            {
                _selectedCheckBox6 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox7
        {
            get => _selectedCheckBox7;
            set
            {
                _selectedCheckBox7 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox8
        {
            get => _selectedCheckBox8;
            set
            {
                _selectedCheckBox8 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBox9
        {
            get => _selectedCheckBox9;
            set
            {
                _selectedCheckBox9 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxAntagonischGeenInvloed
        {
            get => _selectedCheckBoxAntagonischGeenInvloed;
            set
            {
                _selectedCheckBoxAntagonischGeenInvloed = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxAntagonischGereduceerd
        {
            get => _selectedCheckBoxAntagonischGereduceerd;
            set
            {
                _selectedCheckBoxAntagonischGereduceerd = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxAntagonischGroei
        {
            get => _selectedCheckBoxAntagonischGroei;
            set
            {
                _selectedCheckBoxAntagonischGroei = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxAntagonischOnbekend
        {
            get => _selectedCheckBoxAntagonischOnbekend;
            set
            {
                _selectedCheckBoxAntagonischOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxAntagonischTerugDringen
        {
            get => _selectedCheckBoxAntagonischTerugDringen;
            set
            {
                _selectedCheckBoxAntagonischTerugDringen = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxApr
        {
            get => _selectedCheckBoxApr;
            set
            {
                _selectedCheckBoxApr = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxAug
        {
            get => _selectedCheckBoxAug;
            set
            {
                _selectedCheckBoxAug = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBezonningHS
        {
            get => _selectedCheckBoxBezonningHs1;
            set
            {
                _selectedCheckBoxBezonningHs1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBezonningHSS
        {
            get => _selectedCheckBoxBezonningHss;
            set
            {
                _selectedCheckBoxBezonningHss = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBezonningOnbekend
        {
            get => _selectedCheckBoxBezonningOnbekend;
            set
            {
                _selectedCheckBoxBezonningOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBezonningS
        {
            get => _selectedCheckBoxBezonningS;
            set
            {
                _selectedCheckBoxBezonningS = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBezonningZ
        {
            get => _selectedCheckBoxBezonningZ1;
            set
            {
                _selectedCheckBoxBezonningZ1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBezonningZHS
        {
            get => _selectedCheckBoxBezonningZhs1;
            set
            {
                _selectedCheckBoxBezonningZhs1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBezonningZHSS
        {
            get => _selectedCheckBoxBezonningZhss;
            set
            {
                _selectedCheckBoxBezonningZhss = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBezonningZS
        {
            get => _selectedCheckBoxBezonningZs;
            set
            {
                _selectedCheckBoxBezonningZs = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBijvriendelijk
        {
            get => _selectedCheckBoxBijvriendelijk;
            set
            {
                _selectedCheckBoxBijvriendelijk = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurBlauw
        {
            get => _selectedCheckBoxBladkleurBlauw;
            set
            {
                _selectedCheckBoxBladkleurBlauw = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurBruin
        {
            get => _selectedCheckBoxBladkleurBruin;
            set
            {
                _selectedCheckBoxBladkleurBruin = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurGeel
        {
            get => _selectedCheckBoxBladkleurGeel;
            set
            {
                _selectedCheckBoxBladkleurGeel = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurGrijs
        {
            get => _selectedCheckBoxBladkleurGrijs;
            set
            {
                _selectedCheckBoxBladkleurGrijs = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurGroen
        {
            get => _selectedCheckBoxBladkleurGroen;
            set
            {
                _selectedCheckBoxBladkleurGroen = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurLila
        {
            get => _selectedCheckBoxBladkleurLila;
            set
            {
                _selectedCheckBoxBladkleurLila = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurOnbekend
        {
            get => _selectedCheckBoxBladkleurOnbekend2;
            set
            {
                _selectedCheckBoxBladkleurOnbekend2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurOranje
        {
            get => _selectedCheckBoxBladkleurOranje;
            set
            {
                _selectedCheckBoxBladkleurOranje = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurPaars
        {
            get => _selectedCheckBoxBladkleurPaars1;
            set
            {
                _selectedCheckBoxBladkleurPaars1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurRood
        {
            get => _selectedCheckBoxBladkleurRood;
            set
            {
                _selectedCheckBoxBladkleurRood = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurRosé
        {
            get => _selectedCheckBoxBladkleurRosé;
            set
            {
                _selectedCheckBoxBladkleurRosé = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurWit
        {
            get => _selectedCheckBoxBladkleurWit;
            set
            {
                _selectedCheckBoxBladkleurWit = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladkleurZwart
        {
            get => _selectedCheckBoxBladkleurZwart;
            set
            {
                _selectedCheckBoxBladkleurZwart = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm1
        {
            get => _selectedCheckBoxBladvormenVorm1;
            set
            {
                _selectedCheckBoxBladvormenVorm1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm2
        {
            get => _selectedCheckBoxBladvormenVorm2;
            set
            {
                _selectedCheckBoxBladvormenVorm2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm3
        {
            get => _selectedCheckBoxBladvormenVorm3;
            set
            {
                _selectedCheckBoxBladvormenVorm3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm4
        {
            get => _selectedCheckBoxBladvormenVorm4;
            set
            {
                _selectedCheckBoxBladvormenVorm4 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm5
        {
            get => _selectedCheckBoxBladvormenVorm5;
            set
            {
                _selectedCheckBoxBladvormenVorm5 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm6
        {
            get => _selectedCheckBoxBladvormenVorm6;
            set
            {
                _selectedCheckBoxBladvormenVorm6 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm7
        {
            get => _selectedCheckBoxBladvormenVorm7;
            set
            {
                _selectedCheckBoxBladvormenVorm7 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm8
        {
            get => _selectedCheckBoxBladvormenVorm8;
            set
            {
                _selectedCheckBoxBladvormenVorm8 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBladvormenVorm9
        {
            get => _selectedCheckBoxBladvormenVorm9;
            set
            {
                _selectedCheckBoxBladvormenVorm9 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteApr
        {
            get => _selectedCheckBoxBloeiHoogteApr;
            set
            {
                _selectedCheckBoxBloeiHoogteApr = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteAug
        {
            get => _selectedCheckBoxBloeiHoogteAug;
            set
            {
                _selectedCheckBoxBloeiHoogteAug = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteDec
        {
            get => _selectedCheckBoxBloeiHoogteDec;
            set
            {
                _selectedCheckBoxBloeiHoogteDec = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteFeb
        {
            get => _selectedCheckBoxBloeiHoogteFeb;
            set
            {
                _selectedCheckBoxBloeiHoogteFeb = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteJan
        {
            get => _selectedCheckBoxBloeiHoogteJan;
            set
            {
                _selectedCheckBoxBloeiHoogteJan = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteJul
        {
            get => _selectedCheckBoxBloeiHoogteJul;
            set
            {
                _selectedCheckBoxBloeiHoogteJul = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteJun
        {
            get => _selectedCheckBoxBloeiHoogteJun;
            set
            {
                _selectedCheckBoxBloeiHoogteJun = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteMar
        {
            get => _selectedCheckBoxBloeiHoogteMar;
            set
            {
                _selectedCheckBoxBloeiHoogteMar = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteMay
        {
            get => _selectedCheckBoxBloeiHoogteMay;
            set
            {
                _selectedCheckBoxBloeiHoogteMay = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteNov
        {
            get => _selectedCheckBoxBloeiHoogteNov;
            set
            {
                _selectedCheckBoxBloeiHoogteNov = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteOct
        {
            get => _selectedCheckBoxBloeiHoogteOct;
            set
            {
                _selectedCheckBoxBloeiHoogteOct = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteOnbekend
        {
            get => _selectedCheckBoxBloeiHoogteOnbekend;
            set
            {
                _selectedCheckBoxBloeiHoogteOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiHoogteSep
        {
            get => _selectedCheckBoxBloeiHoogteSep;
            set
            {
                _selectedCheckBoxBloeiHoogteSep = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurBlauw
        {
            get => _selectedCheckBoxBloeikleurBlauw;
            set
            {
                _selectedCheckBoxBloeikleurBlauw = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurBruin
        {
            get => _selectedCheckBoxBloeikleurBruin;
            set
            {
                _selectedCheckBoxBloeikleurBruin = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurGeel
        {
            get => _selectedCheckBoxBloeikleurGeel;
            set
            {
                _selectedCheckBoxBloeikleurGeel = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurGrijs
        {
            get => _selectedCheckBoxBloeikleurGrijs;
            set
            {
                _selectedCheckBoxBloeikleurGrijs = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurGroen
        {
            get => _selectedCheckBoxBloeikleurGroen;
            set
            {
                _selectedCheckBoxBloeikleurGroen = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurLila
        {
            get => _selectedCheckBoxBloeikleurLila;
            set
            {
                _selectedCheckBoxBloeikleurLila = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurOnbekend
        {
            get => _selectedCheckBoxBloeikleurOnbekend;
            set
            {
                _selectedCheckBoxBloeikleurOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurOranje
        {
            get => _selectedCheckBoxBloeikleurOranje;
            set
            {
                _selectedCheckBoxBloeikleurOranje = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurPaars
        {
            get => _selectedCheckBoxBloeikleurPaars;
            set
            {
                _selectedCheckBoxBloeikleurPaars = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurRood
        {
            get => _selectedCheckBoxBloeikleurRood;
            set
            {
                _selectedCheckBoxBloeikleurRood = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurRosé
        {
            get => _selectedCheckBoxBloeikleurRosé;
            set
            {
                _selectedCheckBoxBloeikleurRosé = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurViolet
        {
            get => _selectedCheckBoxBloeikleurViolet;
            set
            {
                _selectedCheckBoxBloeikleurViolet = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurWit
        {
            get => _selectedCheckBoxBloeikleurWit;
            set
            {
                _selectedCheckBoxBloeikleurWit = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeikleurZwart
        {
            get => _selectedCheckBoxBloeikleurZwart;
            set
            {
                _selectedCheckBoxBloeikleurZwart = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInApr
        {
            get => _selectedCheckBoxBloeitInApr;
            set
            {
                _selectedCheckBoxBloeitInApr = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInAug
        {
            get => _selectedCheckBoxBloeitInAug;
            set
            {
                _selectedCheckBoxBloeitInAug = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInDec
        {
            get => _selectedCheckBoxBloeitInDec;
            set
            {
                _selectedCheckBoxBloeitInDec = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInFeb
        {
            get => _selectedCheckBoxBloeitInFeb;
            set
            {
                _selectedCheckBoxBloeitInFeb = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInJan
        {
            get => _selectedCheckBoxBloeitInJan;
            set
            {
                _selectedCheckBoxBloeitInJan = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInJul
        {
            get => _selectedCheckBoxBloeitInJul;
            set
            {
                _selectedCheckBoxBloeitInJul = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInJun
        {
            get => _selectedCheckBoxBloeitInJun;
            set
            {
                _selectedCheckBoxBloeitInJun = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInMar
        {
            get => _selectedCheckBoxBloeitInMar;
            set
            {
                _selectedCheckBoxBloeitInMar = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInMay
        {
            get => _selectedCheckBoxBloeitInMay;
            set
            {
                _selectedCheckBoxBloeitInMay = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInNov
        {
            get => _selectedCheckBoxBloeitInNov;
            set
            {
                _selectedCheckBoxBloeitInNov = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInOct
        {
            get => _selectedCheckBoxBloeitInOct;
            set
            {
                _selectedCheckBoxBloeitInOct = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInOnbekend
        {
            get => _selectedCheckBoxBloeitInOnbekend;
            set
            {
                _selectedCheckBoxBloeitInOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeitInSep
        {
            get => _selectedCheckBoxBloeitInSep;
            set
            {
                _selectedCheckBoxBloeitInSep = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiwijzeVorm1
        {
            get => _selectedCheckBoxBloeiwijzeVorm1;
            set
            {
                _selectedCheckBoxBloeiwijzeVorm1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiwijzeVorm2
        {
            get => _selectedCheckBoxBloeiwijzeVorm2;
            set
            {
                _selectedCheckBoxBloeiwijzeVorm2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiwijzeVorm3
        {
            get => _selectedCheckBoxBloeiwijzeVorm3;
            set
            {
                _selectedCheckBoxBloeiwijzeVorm3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiwijzeVorm4
        {
            get => _selectedCheckBoxBloeiwijzeVorm4;
            set
            {
                _selectedCheckBoxBloeiwijzeVorm4 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiwijzeVorm5
        {
            get => _selectedCheckBoxBloeiwijzeVorm5;
            set
            {
                _selectedCheckBoxBloeiwijzeVorm5 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiwijzeVorm6
        {
            get => _selectedCheckBoxBloeiwijzeVorm6;
            set
            {
                _selectedCheckBoxBloeiwijzeVorm6 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxBloeiwijzeVormOnbekend
        {
            get => _selectedCheckBoxBloeiwijzeVormOnbekend;
            set
            {
                _selectedCheckBoxBloeiwijzeVormOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxDec
        {
            get => _selectedCheckBoxDec;
            set
            {
                _selectedCheckBoxDec = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxEetbaarKruidbaar
        {
            get => _selectedCheckBoxEetbaarKruidbaar;
            set
            {
                _selectedCheckBoxEetbaarKruidbaar = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxFeb
        {
            get => _selectedCheckBoxFeb;
            set
            {
                _selectedCheckBoxFeb = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGeurend
        {
            get => _selectedCheckBoxGeurend;
            set
            {
                _selectedCheckBoxGeurend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrondSoortK
        {
            get => _selectedCheckBoxGrondSoortK;
            set
            {
                _selectedCheckBoxGrondSoortK = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrondSoortL
        {
            get => _selectedCheckBoxGrondSoortL;
            set
            {
                _selectedCheckBoxGrondSoortL = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrondSoortLK
        {
            get => _selectedCheckBoxGrondSoortLk;
            set
            {
                _selectedCheckBoxGrondSoortLk = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrondSoortOnbekend
        {
            get => _selectedCheckBoxGrondSoortOnbekend;
            set
            {
                _selectedCheckBoxGrondSoortOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrondSoortZ
        {
            get => _selectedCheckBoxGrondSoortZ;
            set
            {
                _selectedCheckBoxGrondSoortZ = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrondSoortZL
        {
            get => _selectedCheckBoxGrondSoortZl;
            set
            {
                _selectedCheckBoxGrondSoortZl = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrondSoortZLK
        {
            get => _selectedCheckBoxGrondSoortZlk;
            set
            {
                _selectedCheckBoxGrondSoortZlk = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrootte100
        {
            get => _selectedCheckBoxGrootte100;
            set
            {
                _selectedCheckBoxGrootte100 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrootte10
        {
            get => _selectedCheckBoxGrootte10;
            set
            {
                _selectedCheckBoxGrootte10 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrootte150
        {
            get => _selectedCheckBoxGrootte150;
            set
            {
                _selectedCheckBoxGrootte150 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrootte20
        {
            get => _selectedCheckBoxGrootte20;
            set
            {
                _selectedCheckBoxGrootte20 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrootte50
        {
            get => _selectedCheckBoxGrootte50;
            set
            {
                _selectedCheckBoxGrootte50 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrootte5
        {
            get => _selectedCheckBoxGrootte5;
            set
            {
                _selectedCheckBoxGrootte5 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxGrootteOnbekend
        {
            get => _selectedCheckBoxGrootteOnbekend1;
            set
            {
                _selectedCheckBoxGrootteOnbekend1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatA1
        {
            get => _selectedCheckBoxHabitatA1;
            set
            {
                _selectedCheckBoxHabitatA1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatA2
        {
            get => _selectedCheckBoxHabitatA2;
            set
            {
                _selectedCheckBoxHabitatA2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatB1
        {
            get => _selectedCheckBoxHabitatB1;
            set
            {
                _selectedCheckBoxHabitatB1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatB2
        {
            get => _selectedCheckBoxHabitatB2;
            set
            {
                _selectedCheckBoxHabitatB2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatB3
        {
            get => _selectedCheckBoxHabitatB3;
            set
            {
                _selectedCheckBoxHabitatB3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatBR1
        {
            get => _selectedCheckBoxHabitatBr1;
            set
            {
                _selectedCheckBoxHabitatBr1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatBR2
        {
            get => _selectedCheckBoxHabitatBr2;
            set
            {
                _selectedCheckBoxHabitatBr2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatBR3
        {
            get => _selectedCheckBoxHabitatBr3;
            set
            {
                _selectedCheckBoxHabitatBr3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatGB1
        {
            get => _selectedCheckBoxHabitatGb1;
            set
            {
                _selectedCheckBoxHabitatGb1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatGB2
        {
            get => _selectedCheckBoxHabitatGb2;
            set
            {
                _selectedCheckBoxHabitatGb2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatGB3
        {
            get => _selectedCheckBoxHabitatGb3;
            set
            {
                _selectedCheckBoxHabitatGb3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatGR1
        {
            get => _selectedCheckBoxHabitatGr1;
            set
            {
                _selectedCheckBoxHabitatGr1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatGR2
        {
            get => _selectedCheckBoxHabitatGr2;
            set
            {
                _selectedCheckBoxHabitatGr2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatH1
        {
            get => _selectedCheckBoxHabitatH1;
            set
            {
                _selectedCheckBoxHabitatH1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatH2
        {
            get => _selectedCheckBoxHabitatH2;
            set
            {
                _selectedCheckBoxHabitatH2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatM1
        {
            get => _selectedCheckBoxHabitatM1;
            set
            {
                _selectedCheckBoxHabitatM1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatM2
        {
            get => _selectedCheckBoxHabitatM2;
            set
            {
                _selectedCheckBoxHabitatM2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatO4
        {
            get => _selectedCheckBoxHabitatO4;
            set
            {
                _selectedCheckBoxHabitatO4 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatO5
        {
            get => _selectedCheckBoxHabitatO5;
            set
            {
                _selectedCheckBoxHabitatO5 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOB1
        {
            get => _selectedCheckBoxHabitatOb1;
            set
            {
                _selectedCheckBoxHabitatOb1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOB2
        {
            get => _selectedCheckBoxHabitatOb2;
            set
            {
                _selectedCheckBoxHabitatOb2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOnbekend
        {
            get => _selectedCheckBoxHabitatOnbekend;
            set
            {
                _selectedCheckBoxHabitatOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOP1B
        {
            get => _selectedCheckBoxHabitatOp1B;
            set
            {
                _selectedCheckBoxHabitatOp1B = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOP1
        {
            get => _selectedCheckBoxHabitatOp1;
            set
            {
                _selectedCheckBoxHabitatOp1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOP2B
        {
            get => _selectedCheckBoxHabitatOp2B;
            set
            {
                _selectedCheckBoxHabitatOp2B = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOP2
        {
            get => _selectedCheckBoxHabitatOp2;
            set
            {
                _selectedCheckBoxHabitatOp2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOP3B
        {
            get => _selectedCheckBoxHabitatOp3B;
            set
            {
                _selectedCheckBoxHabitatOp3B = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatOP3
        {
            get => _selectedCheckBoxHabitatOp3;
            set
            {
                _selectedCheckBoxHabitatOp3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatSH1
        {
            get => _selectedCheckBoxHabitatSh1;
            set
            {
                _selectedCheckBoxHabitatSh1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatSH2
        {
            get => _selectedCheckBoxHabitatSh2;
            set
            {
                _selectedCheckBoxHabitatSh2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatST1
        {
            get => _selectedCheckBoxHabitatSt1;
            set
            {
                _selectedCheckBoxHabitatSt1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatST2
        {
            get => _selectedCheckBoxHabitatSt2;
            set
            {
                _selectedCheckBoxHabitatSt2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatSV1
        {
            get => _selectedCheckBoxHabitatSv1;
            set
            {
                _selectedCheckBoxHabitatSv1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatSV2
        {
            get => _selectedCheckBoxHabitatSv2;
            set
            {
                _selectedCheckBoxHabitatSv2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxHabitatSV3
        {
            get => _selectedCheckBoxHabitatSv3;
            set
            {
                _selectedCheckBoxHabitatSv3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxJan
        {
            get => _selectedCheckBoxJan;
            set
            {
                _selectedCheckBoxJan = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxJul
        {
            get => _selectedCheckBoxJul;
            set
            {
                _selectedCheckBoxJul = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxJun
        {
            get => _selectedCheckBoxJun;
            set
            {
                _selectedCheckBoxJun = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm1
        {
            get => _selectedCheckBoxLevensvormenVorm1;
            set
            {
                _selectedCheckBoxLevensvormenVorm1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm2
        {
            get => _selectedCheckBoxLevensvormenVorm2;
            set
            {
                _selectedCheckBoxLevensvormenVorm2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm3
        {
            get => _selectedCheckBoxLevensvormenVorm3;
            set
            {
                _selectedCheckBoxLevensvormenVorm3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm4
        {
            get => _selectedCheckBoxLevensvormenVorm4;
            set
            {
                _selectedCheckBoxLevensvormenVorm4 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm5
        {
            get => _selectedCheckBoxLevensvormenVorm5;
            set
            {
                _selectedCheckBoxLevensvormenVorm5 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm6
        {
            get => _selectedCheckBoxLevensvormenVorm6;
            set
            {
                _selectedCheckBoxLevensvormenVorm6 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm7
        {
            get => _selectedCheckBoxLevensvormenVorm7;
            set
            {
                _selectedCheckBoxLevensvormenVorm7 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm8
        {
            get => _selectedCheckBoxLevensvormenVorm8;
            set
            {
                _selectedCheckBoxLevensvormenVorm8 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxLevensvormenVorm9
        {
            get => _selectedCheckBoxLevensvormenVorm9;
            set
            {
                _selectedCheckBoxLevensvormenVorm9 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxMar
        {
            get => _selectedCheckBoxMar;
            set
            {
                _selectedCheckBoxMar = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxMay
        {
            get => _selectedCheckBoxMay;
            set
            {
                _selectedCheckBoxMay = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxNov
        {
            get => _selectedCheckBoxNov;
            set
            {
                _selectedCheckBoxNov = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxOct
        {
            get => _selectedCheckBoxOct;
            set
            {
                _selectedCheckBoxOct = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxRatioAster
        {
            get => _selectedCheckBoxRatioAster;
            set
            {
                _selectedCheckBoxRatioAster = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxRatioGeranium
        {
            get => _selectedCheckBoxRatioGeranium;
            set
            {
                _selectedCheckBoxRatioGeranium = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxRatioOnbekend
        {
            get => _selectedCheckBoxRatioOnbekend;
            set
            {
                _selectedCheckBoxRatioOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxRatioPachysandra
        {
            get => _selectedCheckBoxRatioPachysandra;
            set
            {
                _selectedCheckBoxRatioPachysandra = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxRatioSalvia
        {
            get => _selectedCheckBoxRatioSalvia;
            set
            {
                _selectedCheckBoxRatioSalvia = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSep
        {
            get => _selectedCheckBoxSep;
            set
            {
                _selectedCheckBoxSep = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSociabiliteitI
        {
            get => _selectedCheckBoxSociabiliteitI;
            set
            {
                _selectedCheckBoxSociabiliteitI = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSociabiliteitII
        {
            get => _selectedCheckBoxSociabiliteitIi;
            set
            {
                _selectedCheckBoxSociabiliteitIi = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSociabiliteitIII
        {
            get => _selectedCheckBoxSociabiliteitIii;
            set
            {
                _selectedCheckBoxSociabiliteitIii = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSociabiliteitIV
        {
            get => _selectedCheckBoxSociabiliteitIv;
            set
            {
                _selectedCheckBoxSociabiliteitIv = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSociabiliteitV
        {
            get => _selectedCheckBoxSociabiliteitV;
            set
            {
                _selectedCheckBoxSociabiliteitV = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSpruitAltijdGroen
        {
            get => _selectedCheckBoxSpruitAltijdGroen;
            set
            {
                _selectedCheckBoxSpruitAltijdGroen = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSpruitVoorjaarsgroen
        {
            get => _selectedCheckBoxSpruitVoorjaarsgroen;
            set
            {
                _selectedCheckBoxSpruitVoorjaarsgroen = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxSpruitZomergroen
        {
            get => _selectedCheckBoxSpruitZomergroen;
            set
            {
                _selectedCheckBoxSpruitZomergroen = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStengelvormenVorm1
        {
            get => _selectedCheckBoxStengelvormenVorm1;
            set
            {
                _selectedCheckBoxStengelvormenVorm1 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStengelvormenVorm2
        {
            get => _selectedCheckBoxStengelvormenVorm2;
            set
            {
                _selectedCheckBoxStengelvormenVorm2 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStengelvormenVorm3
        {
            get => _selectedCheckBoxStengelvormenVorm3;
            set
            {
                _selectedCheckBoxStengelvormenVorm3 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStengelvormenVorm4
        {
            get => _selectedCheckBoxStengelvormenVorm4;
            set
            {
                _selectedCheckBoxStengelvormenVorm4 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStengelvormenVorm5
        {
            get => _selectedCheckBoxStengelvormenVorm5;
            set
            {
                _selectedCheckBoxStengelvormenVorm5 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStengelvormenVorm6
        {
            get => _selectedCheckBoxStengelvormenVorm6;
            set
            {
                _selectedCheckBoxStengelvormenVorm6 = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStrategieC
        {
            get => _selectedCheckBoxStrategieC;
            set
            {
                _selectedCheckBoxStrategieC = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStrategieCR
        {
            get => _selectedCheckBoxStrategieCr;
            set
            {
                _selectedCheckBoxStrategieCr = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStrategieCSR
        {
            get => _selectedCheckBoxStrategieCsr;
            set
            {
                _selectedCheckBoxStrategieCsr = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStrategieR
        {
            get => _selectedCheckBoxStrategieR;
            set
            {
                _selectedCheckBoxStrategieR = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStrategieRS
        {
            get => _selectedCheckBoxStrategieRs;
            set
            {
                _selectedCheckBoxStrategieRs = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStrategieSC
        {
            get => _selectedCheckBoxStrategieSc;
            set
            {
                _selectedCheckBoxStrategieSc = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxStrategieS
        {
            get => _selectedCheckBoxStrategieS;
            set
            {
                _selectedCheckBoxStrategieS = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVlindervriendelijk
        {
            get => _selectedCheckBoxVlindervriendelijk;
            set
            {
                _selectedCheckBoxVlindervriendelijk = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVochtbehoefteDroogFris
        {
            get => _selectedCheckBoxVochtbehoefteDroogFris;
            set
            {
                _selectedCheckBoxVochtbehoefteDroogFris = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVochtbehoefteDroog
        {
            get => _selectedCheckBoxVochtbehoefteDroog;
            set
            {
                _selectedCheckBoxVochtbehoefteDroog = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVochtbehoefteFris
        {
            get => _selectedCheckBoxVochtbehoefteFris;
            set
            {
                _selectedCheckBoxVochtbehoefteFris = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVochtbehoefteFrisVochtig
        {
            get => _selectedCheckBoxVochtbehoefteFrisVochtig;
            set
            {
                _selectedCheckBoxVochtbehoefteFrisVochtig = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVochtbehoefteNat
        {
            get => _selectedCheckBoxVochtbehoefteNat;
            set
            {
                _selectedCheckBoxVochtbehoefteNat = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVochtbehoefteOnbekend
        {
            get => _selectedCheckBoxVochtbehoefteOnbekend;
            set
            {
                _selectedCheckBoxVochtbehoefteOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVochtbehoefteVochtig
        {
            get => _selectedCheckBoxVochtbehoefteVochtig;
            set
            {
                _selectedCheckBoxVochtbehoefteVochtig = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVochtbehoefteVochtigNat
        {
            get => _selectedCheckBoxVochtbehoefteVochtigNat;
            set
            {
                _selectedCheckBoxVochtbehoefteVochtigNat = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVoedingsbehoefteArm
        {
            get => _selectedCheckBoxVoedingsbehoefteArm;
            set
            {
                _selectedCheckBoxVoedingsbehoefteArm = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVoedingsbehoefteArmMatig
        {
            get => _selectedCheckBoxVoedingsbehoefteArmMatig;
            set
            {
                _selectedCheckBoxVoedingsbehoefteArmMatig = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVoedingsbehoefteIndifferent
        {
            get => _selectedCheckBoxVoedingsbehoefteIndifferent;
            set
            {
                _selectedCheckBoxVoedingsbehoefteIndifferent = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVoedingsbehoefteMatig
        {
            get => _selectedCheckBoxVoedingsbehoefteMatig;
            set
            {
                _selectedCheckBoxVoedingsbehoefteMatig = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVoedingsbehoefteMatigVoedselrijk
        {
            get => _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk;
            set
            {
                _selectedCheckBoxVoedingsbehoefteMatigVoedselrijk = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVoedingsbehoefteOnbekend
        {
            get => _selectedCheckBoxVoedingsbehoefteOnbekend;
            set
            {
                _selectedCheckBoxVoedingsbehoefteOnbekend = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVoedingsbehoefteVoedselrijk
        {
            get => _selectedCheckBoxVoedingsbehoefteVoedselrijk;
            set
            {
                _selectedCheckBoxVoedingsbehoefteVoedselrijk = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent
        {
            get => _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent;
            set
            {
                _selectedCheckBoxVoedingsbehoefteVoedselrijkIndifferent = value;
                OnPropertyChanged();
            }
        }

        public bool SelectedCheckBoxVorstgevoelig
        {
            get => _selectedCheckBoxVorstgevoelig;
            set
            {
                _selectedCheckBoxVorstgevoelig = value;
                OnPropertyChanged();
            }
        }
    }
}
//End----------------------------------------------------------------------------------------------