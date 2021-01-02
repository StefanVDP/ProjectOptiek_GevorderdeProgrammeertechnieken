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
    class KleurenBeherenViewModel : BasisViewModel
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
        public string SelecteerdeKleur { get; set; }
        public string KleurText { get; set; }
        public bool SelectieIsTrue { get; set; }
        public bool SelectieIsFalse { get; set; }
        public Visibility SelectieIsTrueVisibility { get; set; }
        private Kleur selectedKleurClass;
        public ObservableCollection<Kleur> Kleuren { get; set; }
        public ObservableCollection<Kleur> AlleKleuren{ get; set; }
        public Kleur SelectedKleurClass
        {
            get => selectedKleurClass;
            set
            {
                selectedKleurClass = value;
                if (selectedKleurClass == null)
                {
                    SelecteerdeKleur = "geen geselecteerd";
                    SelectieIsTrueVisibility = Visibility.Collapsed;
                    SelectieIsTrue = false;
                    SelectieIsFalse = true;
                    KleurText = null;
                }
                else
                {
                    SelecteerdeKleur = selectedKleurClass.Naam;
                    SelectieIsTrueVisibility = Visibility.Visible;
                    SelectieIsTrue = true;
                    SelectieIsFalse = false;
                    KleurText = selectedKleurClass.Naam;
                }
            }
        }
        public KleurenBeherenViewModel()
        {
            SelectieIsTrueVisibility = Visibility.Collapsed;
            SelectieIsTrue = false;
            SelectieIsFalse = true;
            AlleKleuren = new ObservableCollection<Kleur>(unitOfWork.KleurRepo.Ophalen());
            Kleuren = AlleKleuren;
            SelecteerdeKleur = "geen geselecteerd";
        }
        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Aanmaken":
                    if (SelectedKleurClass == null)
                    {
                        if (KleurText != "" && KleurText != null)
                        {
                            Kleur nieuweKleur = new Kleur()
                            {
                                Naam = KleurText
                            };

                            if (nieuweKleur.IsGeldig())
                            {

                                unitOfWork.KleurRepo.Toevoegen(nieuweKleur);
                                int ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    AlleKleuren = new ObservableCollection<Kleur>(unitOfWork.KleurRepo.Ophalen());
                                    Kleuren = AlleKleuren;
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
                        MessageBox.Show("U kunt geen nieuwe kleur aanmaken terwijl u bezig bent met een bestaande kleur te bewerken.");
                    }
                    break;
                case "KleurEdit":
                    if (SelectedKleurClass != null)
                    {
                        if (KleurText != "" && KleurText != null)
                        {

                            SelectedKleurClass.Naam = KleurText;

                            if (SelectedKleurClass.IsGeldig())
                            {
                                if (SelectedKleurClass.KleurID > 0)
                                {
                                    unitOfWork.KleurRepo.Aanpassen(SelectedKleurClass);
                                }
                                else
                                {
                                    unitOfWork.KleurRepo.Toevoegen(SelectedKleurClass);
                                }
                                int ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    AlleKleuren = new ObservableCollection<Kleur>(unitOfWork.KleurRepo.Ophalen());
                                    Kleuren = AlleKleuren;
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
                        MessageBox.Show("U moet eerst een kleur uit de lijst selecteren voordat je het kan bewerken.");
                    }
                    break;
                case "SelectieCancel":
                    SelectedKleurClass = null;
                    break;
                case "KleurDeleten":
                    if (SelectedKleurClass == null)
                    {
                        MessageBox.Show("U moet eerst een kleur uit de lijst selecteren voordat je het kan verwijderen.");
                    }
                    else
                    {
                        MessageBoxResult MessageBoxResult = (MessageBoxResult)MessageBox.Show("Bent u zeker dat u deze kleur wil verwijderen?", "Verwijder kleur", MessageBoxButton.YesNo);
                        if (MessageBoxResult == System.Windows.MessageBoxResult.Yes)
                        {
                            int ok;
                            ObservableCollection<Kleurcombinatie> teverwijderenKleurcombinatie = new ObservableCollection<Kleurcombinatie>(unitOfWork.KleurcombinatieRepo.Ophalen(x => x.KleurID == SelectedKleurClass.KleurID));
                            foreach (Kleurcombinatie combinatie in teverwijderenKleurcombinatie)
                            {
                                unitOfWork.KleurcombinatieRepo.Verwijderen(combinatie.KleurCombinatieID);
                                ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                   
                                }
                            }
                            unitOfWork.KleurRepo.Verwijderen(SelectedKleurClass.KleurID);
                            ok = unitOfWork.Save();
                            if (ok > 0)
                            {
                                AlleKleuren = new ObservableCollection<Kleur>(unitOfWork.KleurRepo.Ophalen());
                                Kleuren = AlleKleuren;
                            }
                            else
                            {
                                MessageBox.Show("Er is iets misgegaan bij het verwijderen van het geselecteerde kleur");
                            }
                        }
                    }
                    break;

            }
        }
    }
}
