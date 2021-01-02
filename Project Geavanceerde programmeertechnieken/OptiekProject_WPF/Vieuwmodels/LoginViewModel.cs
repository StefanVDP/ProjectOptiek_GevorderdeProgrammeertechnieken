using OptiekProject_DAL;
using OptiekProject_DAL.Data;
using OptiekProject_DAL.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace OptiekProject_WPF.VieuwModels
{
    class LoginViewModel : BasisViewModel
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
        public ObservableCollection<Gebruiker> Gebruikers { get; set; }
        public string inlogmail { get; set; }
        public string inlogwachtwoord { get; set; }
        public Gebruiker gebruiker { get; set; }
        public LoginViewModel(Window Mainview)
        {
            window = Mainview;
            Gebruikers = new ObservableCollection<Gebruiker>(unitOfWork.GebruikerRepo.Ophalen());
        }
        public override void Execute(object parameter)
        {
            //Ik weet dat deze oplossing voor de login niet is zoals in het voorbeeld. Ik heb voor deze optie gekozen omdat de methode van het voorbeeld ervoor zorgde dat mijn hele project ophield met werken.
            var passwordBox = parameter as PasswordBox;
            inlogwachtwoord = passwordBox.Password;
            gebruiker = Gebruikers.Where(x => x.Wachtwoord == inlogwachtwoord && x.Mail == inlogmail).SingleOrDefault();
            if (gebruiker == null)
            {
                MessageBox.Show("De inloggegevens zijn ongeldig.");
            }
            else
            {
                MainWindow mainView = new MainWindow();
                MainWindowViewModel mainViewModel = new MainWindowViewModel(mainView);
                mainView.DataContext = mainViewModel;
                mainView.Show();

                if (gebruiker.GebruikerTypeID == 1)
                {
                    AdminMainWindow AdminView = new AdminMainWindow();
                    AdminMainWindowViewModel AdminViewModel = new AdminMainWindowViewModel(AdminView);
                    AdminView.DataContext = AdminViewModel;
                    AdminView.Show();
                }
                CloseWindow(window);
            }
        }
    }
}
