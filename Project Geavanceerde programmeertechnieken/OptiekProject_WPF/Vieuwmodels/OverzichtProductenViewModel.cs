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
    class OverzichtProductenViewModel : BasisViewModel
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

        private Model selectedModel;
        public Model SelectedModel
        {
            get => selectedModel;
            set
            {
                selectedModel = value;
                OpenProductWindow();
            }
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

        public Window window { get; set; }
        public BrilType BrilType { get; set; }
        public int IDBriltype { get; set; }
        public ObservableCollection<Model> Modellen { get; set; }
        public ObservableCollection<Merk> Merken { get; set; }
        public ObservableCollection<Model> AlleModellen { get; set; }
        public OverzichtProductenViewModel(Window Mainview, int BriltypeID)
        {
            window = Mainview;
            BrilType = unitOfWork.BrilTypeRepo.Ophalen(x => x.BriltypeID == BriltypeID).SingleOrDefault();
            Merken = new ObservableCollection<Merk>(unitOfWork.MerkRepo.Ophalen());
            Merk geenSelectie = new Merk();
            geenSelectie.MerkID = -1;
            geenSelectie.Naam = "";
            Merken.Insert(0, geenSelectie);
            AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen(x => x.BriltypeID == BriltypeID));
            Modellen = AlleModellen;
            Merkfilter = geenSelectie;
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
                    CloseWindow(window);
                    break;
                case "Sportbril":
                    OverzichtSportbril SportbrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel SportbrillenViewModel = new OverzichtProductenViewModel(SportbrillenWindow,1);
                    SportbrillenWindow.DataContext = SportbrillenViewModel;
                    SportbrillenWindow.Show();
                    CloseWindow(window);
                    break;
                case "Leesbril":
                    OverzichtSportbril LeesbrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel LeesbrillenViewModel = new OverzichtProductenViewModel(LeesbrillenWindow,2);
                    LeesbrillenWindow.DataContext = LeesbrillenViewModel;
                    LeesbrillenWindow.Show();
                    CloseWindow(window);
                    break;
                case "Schietbril":
                    OverzichtSportbril SchietbrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel SchietbrillenViewModel = new OverzichtProductenViewModel(SchietbrillenWindow,3);
                    SchietbrillenWindow.DataContext = SchietbrillenViewModel;
                    SchietbrillenWindow.Show();
                    CloseWindow(window);
                    break;
                case "Zonnebril":
                    OverzichtSportbril ZonnebrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel ZonnebrillenViewModel = new OverzichtProductenViewModel(ZonnebrillenWindow,4);
                    ZonnebrillenWindow.DataContext = ZonnebrillenViewModel;
                    ZonnebrillenWindow.Show();
                    CloseWindow(window);
                    break;

            }
        }
        private void OpenProductWindow()
        {
            ProductVenster Product = new ProductVenster();
            ProductViewModel reeksViewModel = new ProductViewModel(Product, selectedModel.ModelID);
            Product.DataContext = reeksViewModel;
            Product.Show();
            CloseWindow(window);
        }
        private void Filterveranderen()
        {
            if ((Textfilter == null || Textfilter == "") && Merkfilter.MerkID == -1)
            {
                Modellen = AlleModellen;
            }
            else if ((Textfilter == null || Textfilter == "") && Merkfilter.MerkID != -1)
            {
                Modellen = new ObservableCollection<Model>(AlleModellen.Where(x => x.MerkID == Merkfilter.MerkID));
            }
            else if ((Textfilter != null || Textfilter != "") && Merkfilter.MerkID == -1)
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
