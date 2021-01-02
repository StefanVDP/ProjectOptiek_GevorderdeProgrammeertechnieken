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
    class ProductViewModel : BasisViewModel
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

        public Window window { get; set; }
        public Model model { get; set; }
        public bool AangepasteSterkteBool { get; set; }
        public bool BeschikbaarBool { get; set; }
        public decimal prijs { get; set; }
        public string Productnaam { get; set; }
        public string ProductOmschrijving { get; set; }
        private Bril geselecteerdebril;
        public Bril Geselecteerdebril
        {
            get => geselecteerdebril;
            set
            {
                geselecteerdebril = value;
                BrilVeranderen();
            }
        }
        public ObservableCollection<Bril> Sterktes { get; set; }
        public ObservableCollection<Kleurcombinatie> Kleuren { get; set; }


        public ProductViewModel(Window Mainview, int ModelID)
        {
            
            AangepasteSterkteBool = false;
            window = Mainview;
            model = unitOfWork.ModelRepo.Ophalen(x => x.ModelID == ModelID, x => x.Kleurcombinaties).SingleOrDefault();
            Sterktes = new ObservableCollection<Bril>(unitOfWork.BrilRepo.Ophalen(x => x.ModelID == ModelID, x => x.Sterkte));
            Geselecteerdebril = Sterktes.Where(x => x.SterkteID == 1).SingleOrDefault();
            Kleuren = new ObservableCollection<Kleurcombinatie>(unitOfWork.KleurcombinatieRepo.Ophalen(x => x.ModelID == ModelID, x => x.Kleur));

            ProductOmschrijving = model.Omschrijving + " Beschikbare kleuren: ";
            
            foreach (Kleurcombinatie kleur in Kleuren)
            {
                ProductOmschrijving = ProductOmschrijving + kleur.Kleur.Naam + ", ";
            }
            ProductOmschrijving = ProductOmschrijving.Remove(ProductOmschrijving.Length - 2) + ".";
            
            Productnaam = model.Naam;
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
                    OverzichtProductenViewModel SportbrillenViewModel = new OverzichtProductenViewModel(SportbrillenWindow, 1);
                    SportbrillenWindow.DataContext = SportbrillenViewModel;
                    SportbrillenWindow.Show();
                    CloseWindow(window);
                    break;
                case "Leesbril":
                    OverzichtSportbril LeesbrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel LeesbrillenViewModel = new OverzichtProductenViewModel(LeesbrillenWindow, 2);
                    LeesbrillenWindow.DataContext = LeesbrillenViewModel;
                    LeesbrillenWindow.Show();
                    CloseWindow(window);
                    break;
                case "Schietbril":
                    OverzichtSportbril SchietbrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel SchietbrillenViewModel = new OverzichtProductenViewModel(SchietbrillenWindow, 3);
                    SchietbrillenWindow.DataContext = SchietbrillenViewModel;
                    SchietbrillenWindow.Show();
                    CloseWindow(window);
                    break;
                case "Zonnebril":
                    OverzichtSportbril ZonnebrillenWindow = new OverzichtSportbril();
                    OverzichtProductenViewModel ZonnebrillenViewModel = new OverzichtProductenViewModel(ZonnebrillenWindow, 4);
                    ZonnebrillenWindow.DataContext = ZonnebrillenViewModel;
                    ZonnebrillenWindow.Show();
                    CloseWindow(window);
                    break;
            }
        }
        private void BrilVeranderen()
        {
            prijs = (Geselecteerdebril.Model.Prijs + Geselecteerdebril.Sterkte.Prijs) * (100 - Geselecteerdebril.Korting)/100;
        }
    }
}
