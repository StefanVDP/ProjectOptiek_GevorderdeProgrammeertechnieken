using OptiekProject_DAL;
using OptiekProject_DAL.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OptiekProject_WPF.VieuwModels
{
    class AdminMainWindowViewModel : BasisViewModel
    {
        IUnitOfWork unitOfWork = new UnitOfWork(new OptiekDbContext());
        public override string this[string columnName]
        {
            get
            {
                return "";
            }
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        private string textfilter;
        public string Textfilter
        {
            get => textfilter;
            set
            {
                textfilter = value;
                Filterveranderen();
            }
        }

        private Merk merkfilter;
        public Merk Merkfilter
        {
            get => merkfilter;
            set
            {
                merkfilter = value;
                Filterveranderen();
            }
        }

        private Model selectedModel;
        public Model SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                if (selectedModel == null)
                {
                    SelecteerdeBril = "geen geselecteerd";
                }
                else
                {
                    SelecteerdeBril = selectedModel.Naam;
                }
            }
        }
        public Window window { get; set; }
        public ObservableCollection<Model> Modellen { get; set; }
        public ObservableCollection<Model> AlleModellen { get; set; }
        public ObservableCollection<Merk> Merken { get; set; }
        public string SelecteerdeBril { get; set; }
        public AdminMainWindowViewModel(Window Mainview)
        {
            window = Mainview;
            AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen());
            Modellen = AlleModellen;
            Merken = new ObservableCollection<Merk>(unitOfWork.MerkRepo.Ophalen());
            Merk geenSelectie = new Merk();
            geenSelectie.MerkID = -1;
            geenSelectie.Naam = "";
            Merken.Insert(0, geenSelectie);
            merkfilter = geenSelectie;
            SelecteerdeBril = "geen geselecteerd";
        }
        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Home":
                    MainWindow mainwindow = new MainWindow();
                    MainWindowViewModel MainViewModel = new MainWindowViewModel(mainwindow);
                    mainwindow.DataContext = MainViewModel;
                    mainwindow.Show();
                    break;
                case "Sportbril":
                    OverzichtSportbril SportbrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel SportbrillenViewModel = new OverzichtProductenViewModel(SportbrillenWindow, 1);
                    SportbrillenWindow.DataContext = SportbrillenViewModel;
                    SportbrillenWindow.Show();
                    break;
                case "Leesbril":
                    OverzichtSportbril LeesbrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel LeesbrillenViewModel = new OverzichtProductenViewModel(LeesbrillenWindow, 2);
                    LeesbrillenWindow.DataContext = LeesbrillenViewModel;
                    LeesbrillenWindow.Show();
                    break;
                case "Schietbril":
                    OverzichtSportbril SchietbrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel SchietbrillenViewModel = new OverzichtProductenViewModel(SchietbrillenWindow, 3);
                    SchietbrillenWindow.DataContext = SchietbrillenViewModel;
                    SchietbrillenWindow.Show();
                    break;
                case "Zonnebril":
                    OverzichtSportbril ZonnebrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel ZonnebrillenViewModel = new OverzichtProductenViewModel(ZonnebrillenWindow, 4);
                    ZonnebrillenWindow.DataContext = ZonnebrillenViewModel;
                    ZonnebrillenWindow.Show();
                    break;


                case "BrilAanmaken":
                    BrillenBeheren BrillenbeherenWindow = new BrillenBeheren();
                    BrillenBeherenViewModel BrillenbeherenViewModel = new BrillenBeherenViewModel(BrillenbeherenWindow);
                    BrillenbeherenWindow.DataContext = BrillenbeherenViewModel;
                    BrillenbeherenWindow.Show();
                    CloseWindow(window);
                    break;
                case "ModelAanmaken":
                    ModellenBeheren ModellenbeherebWindow = new ModellenBeheren();
                    ModellenBeherenViewModel ModellenbehereViewModel = new ModellenBeherenViewModel(ModellenbeherebWindow);
                    ModellenbeherebWindow.DataContext = ModellenbehereViewModel;
                    ModellenbeherebWindow.Show();
                    CloseWindow(window);
                    break;
                case "SterkteBeheren":
                    SterkteBeheren SterktebeherenWindow = new SterkteBeheren();
                    SterkteBeherenViewModel SterktebeherenViewModel = new SterkteBeherenViewModel();
                    SterktebeherenWindow.DataContext = SterktebeherenViewModel;
                    SterktebeherenWindow.Show();
                    break;
                case "KleurenBeheren":
                    KleurenBeheren KleurenbeherenWindow = new KleurenBeheren();
                    KleurenBeherenViewModel KleurenbeherenViewModel = new KleurenBeherenViewModel();
                    KleurenbeherenWindow.DataContext = KleurenbeherenViewModel;
                    KleurenbeherenWindow.Show();
                    break;
                case "ModelDeleten":
                    if (SelectedModel == null)
                    {
                        MessageBox.Show("U moet eerst een model uit de lijst selecteren voordat je het kan verwijderen.");
                    }
                    else
                    {
                        MessageBoxResult MessageBoxResult = (MessageBoxResult)MessageBox.Show("Bent u zeker dat u dit model wil verwijderen?", "Verwijder model", MessageBoxButton.YesNo);
                        if (MessageBoxResult == System.Windows.MessageBoxResult.Yes)
                        {
                            int ok;
                            ObservableCollection<Kleurcombinatie> teverwijderenKleurcombinaties = new ObservableCollection<Kleurcombinatie>(unitOfWork.KleurcombinatieRepo.Ophalen(x => x.ModelID == SelectedModel.ModelID));
                            foreach (Kleurcombinatie kleur in teverwijderenKleurcombinaties)
                            {
                                unitOfWork.KleurcombinatieRepo.Verwijderen(kleur.KleurCombinatieID);
                                ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    //AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen());
                                    //Filterveranderen();
                                }
                            }
                            ObservableCollection<Bril> teverwijderenBrillen = new ObservableCollection<Bril>(unitOfWork.BrilRepo.Ophalen(x => x.ModelID == SelectedModel.ModelID));
                            foreach (Bril bril in teverwijderenBrillen)
                            {
                                unitOfWork.BrilRepo.Verwijderen(bril.BrilID);
                                ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    //AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen());
                                    //Filterveranderen();
                                }
                            }
                            unitOfWork.ModelRepo.Verwijderen(SelectedModel);
                            ok = unitOfWork.Save();
                            if (ok > 0)
                            {
                                AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen());
                                Filterveranderen();
                            }
                            else
                            {
                                MessageBox.Show("Er is iets misgegaan bij het verwijderen van het geselecteerde model");
                            }
                        }
                    }
                    break;

                    
            }
        }
        private void Filterveranderen()
        {
            if (Textfilter == null || Textfilter == "" && Merkfilter.MerkID == -1)
            {
                Modellen = AlleModellen;
            }
            else if (Textfilter == null || Textfilter == "" && Merkfilter.MerkID != -1)
            {
                Modellen = new ObservableCollection<Model>(AlleModellen.Where(x => x.MerkID == Merkfilter.MerkID));
            }
            else if (Textfilter != null || Textfilter != "" && Merkfilter.MerkID != -1)
            {
                Modellen = new ObservableCollection<Model>(AlleModellen.Where(x => x.Naam.ToLower().Contains(Textfilter.ToLower())));
            }
            else
            {
                Modellen = new ObservableCollection<Model>(AlleModellen.Where(x => x.Naam.ToLower().Contains(Textfilter.ToLower()) && x.MerkID == Merkfilter.MerkID));
            }
        }
    }
}
