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
    class SterkteBeherenViewModel : BasisViewModel
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

        public string SelecteerdeSterkte { get; set; }
        public string sterkteText { get; set; }
        public string prijsText { get; set; }
        public bool SelectieIsTrue { get; set; }
        public bool SelectieIsFalse { get; set; }
        public Visibility SelectieIsTrueVisibility { get; set; }
        private Sterkte selectedSterkteClass;
        public ObservableCollection<Sterkte> Sterktes { get; set; }
        public ObservableCollection<Sterkte> AlleSterktes { get; set; }
        public Sterkte SelectedSterkteClass
        {
            get => selectedSterkteClass;
            set
            {
                selectedSterkteClass = value;
                if (selectedSterkteClass == null)
                {
                    SelecteerdeSterkte = "geen geselecteerd";
                    SelectieIsTrueVisibility = Visibility.Collapsed;
                    SelectieIsTrue = false;
                    SelectieIsFalse = true;
                    sterkteText = null;
                    prijsText = null;
                }
                else
                {
                    SelecteerdeSterkte = selectedSterkteClass.sterkte;
                    SelectieIsTrueVisibility = Visibility.Visible;
                    SelectieIsTrue = true;
                    SelectieIsFalse = false;
                    sterkteText = selectedSterkteClass.sterkte;
                    prijsText = selectedSterkteClass.Prijs.ToString();
                }
            }
        }
        public SterkteBeherenViewModel()
        {
            SelectieIsTrueVisibility = Visibility.Collapsed;
            SelectieIsTrue = false;
            SelectieIsFalse = true;
            AlleSterktes = new ObservableCollection<Sterkte>(unitOfWork.SterkteRepo.Ophalen());
            Sterktes = AlleSterktes;
            SelecteerdeSterkte = "geen geselecteerd";
        }
        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "SterkteAanmaken":
                    if (SelectedSterkteClass == null)
                    {
                        if (sterkteText != "" && sterkteText != null && decimal.TryParse(prijsText, out decimal result))
                        {
                            Sterkte nieuweSterkte = new Sterkte()
                            {
                                sterkte = sterkteText,
                                Prijs = result
                            };

                            if (nieuweSterkte.IsGeldig())
                            {

                                unitOfWork.SterkteRepo.Toevoegen(nieuweSterkte);
                                int ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    AlleSterktes = new ObservableCollection<Sterkte>(unitOfWork.SterkteRepo.Ophalen());
                                    Sterktes = AlleSterktes;
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
                        MessageBox.Show("U kunt geen nieuwe stertke aanmaken terwijl u bezig bent met een bestaande sterkte te bewerken.");
                    }
                    break;
                case "SterkteEdit":
                    if (SelectedSterkteClass != null)
                    {
                        if (sterkteText != "" && sterkteText != null && decimal.TryParse(prijsText, out decimal result))
                        {

                            SelectedSterkteClass.sterkte = sterkteText;
                            SelectedSterkteClass.Prijs = result;

                            if (SelectedSterkteClass.IsGeldig())
                            {
                                if (SelectedSterkteClass.SterkteID > 0)
                                {
                                    unitOfWork.SterkteRepo.Aanpassen(SelectedSterkteClass);
                                }
                                else
                                {
                                    unitOfWork.SterkteRepo.Toevoegen(SelectedSterkteClass);
                                }
                                int ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    AlleSterktes = new ObservableCollection<Sterkte>(unitOfWork.SterkteRepo.Ophalen());
                                    Sterktes = AlleSterktes;
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
                        MessageBox.Show("U moet eerst een sterkte uit de lijst selecteren voordat je het kan bewerken.");
                    }
                    break;
                case "SelectieCancel":
                    SelectedSterkteClass = null;
                    break;
                case "SterkteDeleten":
                    if (SelectedSterkteClass == null)
                    {
                        MessageBox.Show("U moet eerst een sterkte uit de lijst selecteren voordat je het kan verwijderen.");
                    }
                    else
                    {
                        MessageBoxResult MessageBoxResult = (MessageBoxResult)MessageBox.Show("Bent u zeker dat u deze sterkte wil verwijderen?", "Verwijder sterkte", MessageBoxButton.YesNo);
                        if (MessageBoxResult == System.Windows.MessageBoxResult.Yes)
                        {
                            int ok;
                            ObservableCollection<Bril> teverwijderenBrillen = new ObservableCollection<Bril>(unitOfWork.BrilRepo.Ophalen(x => x.SterkteID == SelectedSterkteClass.SterkteID));
                            foreach (Bril bril in teverwijderenBrillen)
                            {
                                unitOfWork.BrilRepo.Verwijderen(bril.BrilID);
                                ok = unitOfWork.Save();
                                if (ok > 0)
                                {
                                    //AlleSterktes = new ObservableCollection<Sterkte>(unitOfWork.SterkteRepo.Ophalen());
                                }
                            }
                            unitOfWork.SterkteRepo.Verwijderen(SelectedSterkteClass.SterkteID);
                            ok = unitOfWork.Save();
                            if (ok > 0)
                            {
                                AlleSterktes = new ObservableCollection<Sterkte>(unitOfWork.SterkteRepo.Ophalen());
                                Sterktes = AlleSterktes;
                            }
                            else
                            {
                                MessageBox.Show("Er is iets misgegaan bij het verwijderen van de geselecteerde sterkte");
                            }
                        }
                    }
                    break;
            }
        }
    }
}
