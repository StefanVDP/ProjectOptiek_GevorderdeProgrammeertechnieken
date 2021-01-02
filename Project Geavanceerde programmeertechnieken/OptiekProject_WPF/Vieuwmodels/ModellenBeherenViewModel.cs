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
    class ModellenBeherenViewModel : BasisViewModel
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

        public string SelecteerdeModel { get; set; }
        public string NaamText { get; set; }
        public string prijsText { get; set; }
        public bool SelectieIsTrue { get; set; }
        public bool SelectieIsFalse { get; set; }
        public Visibility SelectieIsTrueVisibility { get; set; }
        public Window window { get; set; }
        public ObservableCollection<Model> Modellen { get; set; }
        public ObservableCollection<Merk> Merken { get; set; }
        public ObservableCollection<Kleur> kleuren { get; set; }
        public ObservableCollection<Sterkte> sterktes { get; set; }
        public Merk GeselecteerdeMerk { get; set; }
        public ObservableCollection<BrilType> Briltypes { get; set; }
        public BrilType GeselecteerdeBriltype { get; set; }
        public ObservableCollection<Model> AlleModellen { get; set; }
        private Model selectedModelClass;
        public Model SelectedModelClass
        {
            get => selectedModelClass;
            set
            {
                selectedModelClass = value;
                if (selectedModelClass == null)
                {
                    SelecteerdeModel = "geen geselecteerd";
                    SelectieIsTrueVisibility = Visibility.Collapsed;
                    SelectieIsTrue = false;
                    SelectieIsFalse = true;
                    NaamText = null;
                    prijsText = null;
                }
                else
                {
                    SelecteerdeModel = selectedModelClass.Naam;
                    SelectieIsTrueVisibility = Visibility.Visible;
                    SelectieIsTrue = true;
                    SelectieIsFalse = false;
                    NaamText = selectedModelClass.Naam;
                    prijsText = selectedModelClass.Prijs.ToString();
                }
            }
        }
        public ModellenBeherenViewModel(Window Mainview)
        {
            window = Mainview;
            SelectieIsTrueVisibility = Visibility.Collapsed;
            SelectieIsTrue = false;
            SelectieIsFalse = true;
            AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen(x => x.Merk, x => x.BrilTypes));

            Merken = new ObservableCollection<Merk>(unitOfWork.MerkRepo.Ophalen());
            Merk geenSelectie = new Merk();
            geenSelectie.MerkID = -1;
            geenSelectie.Naam = "";
            Merken.Insert(0, geenSelectie);

            Briltypes = new ObservableCollection<BrilType>(unitOfWork.BrilTypeRepo.Ophalen());
            BrilType geenSelectietype = new BrilType();
            geenSelectietype.BriltypeID = -1;
            geenSelectietype.Naam = "";
            Briltypes.Insert(0, geenSelectietype);

            Modellen = AlleModellen;
            SelecteerdeModel = "geen geselecteerd";
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
                case "Terug":
                    AdminMainWindow AdminView = new AdminMainWindow();
                    AdminMainWindowViewModel AdminViewModel = new AdminMainWindowViewModel(AdminView);
                    AdminView.DataContext = AdminViewModel;
                    AdminView.Show(); 
                    CloseWindow(window);
                    break;
                case "ModelAanmaken":
                    if (SelectedModelClass == null)
                    {
                        if (NaamText != "" && NaamText != null && decimal.TryParse(prijsText, out decimal result) && GeselecteerdeMerk.MerkID > 0 && GeselecteerdeBriltype.BriltypeID > 0)
                        {
                            Model nieuweModel = new Model()
                            {
                                Naam = NaamText,
                                Prijs = result,
                                Merk = GeselecteerdeMerk,
                                BrilTypes = GeselecteerdeBriltype,
                                Omschrijving = NaamText + " gemaakt door " + GeselecteerdeMerk.Naam + "."

                            };

                            if (nieuweModel.IsGeldig())
                            {

                                unitOfWork.ModelRepo.Toevoegen(nieuweModel);
                                int ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen(x => x.Merk, x => x.BrilTypes));
                                    Modellen = AlleModellen;
                                }
                                //Alle kleuren worden automatisch verbonden aan een nieuw model (dit is hoe ik besloten heb om het probleem op te lossen dat ik de lijst checkboxen van de schetsen niet heb kunnen nabouwen) en brillen worden ook automatisch aangemaakt.
                                kleuren = new ObservableCollection<Kleur>(unitOfWork.KleurRepo.Ophalen());
                                foreach (Kleur kleur in kleuren)
                                {
                                    Kleurcombinatie combo = new Kleurcombinatie()
                                    {
                                        Model = nieuweModel,
                                        Kleur = kleur

                                    };
                                    if (combo.IsGeldig())
                                    {
                                        unitOfWork.KleurcombinatieRepo.Toevoegen(combo);
                                        ok = unitOfWork.Save();
                                        if (ok > 0)
                                        {

                                        }
                                    }
                                        
                                }
                                sterktes = new ObservableCollection<Sterkte>(unitOfWork.SterkteRepo.Ophalen());
                                foreach (Sterkte sterkte in sterktes)
                                {
                                    Bril newbril = new Bril()
                                    {
                                        Model = nieuweModel,
                                        Sterkte = sterkte,
                                        Korting = 0,
                                        Beschikbaar = true,
                                        Naam = nieuweModel.Naam + "op sterkte " + sterkte.sterkte

                                    };
                                    if (newbril.IsGeldig())
                                    {
                                        unitOfWork.BrilRepo.Toevoegen(newbril);
                                        ok = unitOfWork.Save();
                                        if (ok > 0)
                                        {

                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("De gegevens die u hebt ingegeven zijn niet geldig");
                        }
                    }
                    else
                    {
                        MessageBox.Show("U kunt geen nieuwe modellen aanmaken terwijl u bezig bent met een bestaande model te bewerken.");
                    }
                    break;
                case "ModelEdit":
                    if (SelectedModelClass != null)
                    {
                        if (NaamText != "" && NaamText != null && decimal.TryParse(prijsText, out decimal result) && GeselecteerdeMerk.MerkID > 0 && GeselecteerdeBriltype.BriltypeID > 0)
                        {

                            SelectedModelClass.Naam = NaamText;
                            SelectedModelClass.Prijs = result;
                            SelectedModelClass.Merk = GeselecteerdeMerk;
                            SelectedModelClass.BrilTypes = GeselecteerdeBriltype;
                            SelectedModelClass.Omschrijving = NaamText + " gemaakt door " + GeselecteerdeMerk.Naam + ".";

                            if (SelectedModelClass.IsGeldig())
                            {
                                if (SelectedModelClass.ModelID > 0)
                                {
                                    unitOfWork.ModelRepo.Aanpassen(SelectedModelClass);
                                }
                                else
                                {
                                    unitOfWork.ModelRepo.Toevoegen(SelectedModelClass);
                                }
                                int ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen(x => x.Merk, x => x.BrilTypes));
                                    Modellen = AlleModellen;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("De gegevens die u hebt ingegeven zijn niet geldig");
                        }
                    }
                    else
                    {
                        MessageBox.Show("U moet eerst een model uit de lijst selecteren voordat je het kan bewerken.");
                    }
                    break;
                case "SelectieCancel":
                    SelectedModelClass = null;
                    break;
                case "ModelDeleten":
                    if (SelectedModelClass == null)
                    {
                        MessageBox.Show("U moet eerst een model uit de lijst selecteren voordat je het kan verwijderen.");
                    }
                    else
                    {
                        MessageBoxResult MessageBoxResult = (MessageBoxResult)MessageBox.Show("Bent u zeker dat u dit model wil verwijderen?", "Verwijder model", MessageBoxButton.YesNo);
                        if (MessageBoxResult == System.Windows.MessageBoxResult.Yes)
                        {
                            int ok;
                            ObservableCollection<Kleurcombinatie> teverwijderenKleurcombinaties = new ObservableCollection<Kleurcombinatie>(unitOfWork.KleurcombinatieRepo.Ophalen(x => x.ModelID == SelectedModelClass.ModelID));
                            foreach (Kleurcombinatie kleur in teverwijderenKleurcombinaties)
                            {
                                unitOfWork.KleurcombinatieRepo.Verwijderen(kleur.KleurCombinatieID);
                                ok = unitOfWork.Save();
                                if (ok > 0)
                                {

                                }
                            }
                            ObservableCollection<Bril> teverwijderenBrillen = new ObservableCollection<Bril>(unitOfWork.BrilRepo.Ophalen(x => x.ModelID == SelectedModelClass.ModelID));
                            foreach (Bril bril in teverwijderenBrillen)
                            {
                                unitOfWork.BrilRepo.Verwijderen(bril.BrilID);
                                ok = unitOfWork.Save();
                                if (ok > 0)
                                {

                                }
                            }
                            unitOfWork.ModelRepo.Verwijderen(SelectedModelClass.ModelID);
                            ok = unitOfWork.Save();
                            if (ok > 0)
                            {
                                AlleModellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen(x => x.Merk, x => x.BrilTypes));
                                Modellen = AlleModellen;
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
    }
}
