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
    class BrillenBeherenViewModel : BasisViewModel
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

        public string SelecteerdeBril { get; set; }
        public string NaamText { get; set; }
        public string ModelText { get; set; }
        public string SterkteText { get; set; }
        public string KortingText { get; set; }
        public bool Beschikbaarheid { get; set; }
        public Window window { get; set; }
        public ObservableCollection<Model> Modellen { get; set; }
        public ObservableCollection<Bril> Brillen { get; set; }
        public ObservableCollection<Bril> AlleBrillen { get; set; }

        private Model modelfilter;
        public Model Modelfilter
        {
            get => modelfilter;
            set
            {
                modelfilter = value;
                Filterveranderen();
            }
        }

        private Bril selectedBrilClass;
        public Bril SelectedBrilClass
        {
            get => selectedBrilClass;
            set
            {
                selectedBrilClass = value;
                if (selectedBrilClass == null)
                {
                    SelecteerdeBril = "geen geselecteerd";
                    NaamText = null;
                    ModelText = null;
                    SterkteText = null;
                    KortingText = null;
                    Beschikbaarheid = false;
                }
                else
                {
                    SelecteerdeBril = selectedBrilClass.Naam;
                    NaamText = selectedBrilClass.Naam;
                    ModelText = selectedBrilClass.Model.Naam;
                    SterkteText = selectedBrilClass.Sterkte.sterkte;
                    KortingText = selectedBrilClass.Korting.ToString();
                    Beschikbaarheid = selectedBrilClass.Beschikbaar;
                }
            }
        }
        public BrillenBeherenViewModel(Window Mainview)
        {
            window = Mainview;
            AlleBrillen = new ObservableCollection<Bril>(unitOfWork.BrilRepo.Ophalen(x => x.Model, x => x.Sterkte));
            Modellen = new ObservableCollection<Model>(unitOfWork.ModelRepo.Ophalen());
            Brillen = AlleBrillen;
            SelecteerdeBril = "geen geselecteerd";


            Model geenSelectieModel = new Model();
            geenSelectieModel.ModelID = -1;
            geenSelectieModel.Naam = "";
            Modellen.Insert(0, geenSelectieModel);

            Modelfilter = geenSelectieModel;
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
                case "BrilAanpassen":
                    if (selectedBrilClass != null)
                    {
                        if (NaamText != "" && NaamText != null && int.TryParse(KortingText, out int result))
                        {

                            SelectedBrilClass.Naam = NaamText;
                            SelectedBrilClass.Korting = result;
                            SelectedBrilClass.Beschikbaar = Beschikbaarheid;

                            if (SelectedBrilClass.IsGeldig())
                            {
                                if (SelectedBrilClass.ModelID > 0)
                                {
                                    unitOfWork.BrilRepo.Aanpassen(SelectedBrilClass);
                                }
                                else
                                {
                                    unitOfWork.BrilRepo.Toevoegen(SelectedBrilClass);
                                }
                                int ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    AlleBrillen = new ObservableCollection<Bril>(unitOfWork.BrilRepo.Ophalen(x => x.Model, x => x.Sterkte));
                                    Filterveranderen();
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
                        MessageBox.Show("U moet eerst een bril uit de lijst selecteren voordat je het kan bewerken.");
                    }
                    break;

            }
        }
        private void Filterveranderen()
        {
            if (Modelfilter.ModelID == -1)
            {
                Brillen = AlleBrillen;
            }
            else
            {
                Brillen = new ObservableCollection<Bril>(AlleBrillen.Where(x => x.ModelID == Modelfilter.ModelID));
            }
        }
    }
}
