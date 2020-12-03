using OptiekProject_DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OptiekProject_WPF.VieuwModels
{
    class ModellenBeherenViewModel : BasisViewModel
    {
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
        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Home":
                    MainWindow mainwindow = new MainWindow();
                    MainWindowViewModel MainViewModel = new MainWindowViewModel();
                    mainwindow.DataContext = MainViewModel;
                    mainwindow.Show();
                    break;
                case "Sportbril":
                    OverzichtSportbril SportbrillenWindow = new OverzichtSportbril();
                    MainWindowViewModel SportbrillenViewModel = new MainWindowViewModel();
                    SportbrillenWindow.DataContext = SportbrillenViewModel;
                    SportbrillenWindow.Show();
                    break;
                case "Leesbril":
                    OverzichtSportbril LeesbrillenWindow = new OverzichtSportbril();
                    MainWindowViewModel LeesbrillenViewModel = new MainWindowViewModel();
                    LeesbrillenWindow.DataContext = LeesbrillenViewModel;
                    LeesbrillenWindow.Show();
                    break;
                case "Schietbril":
                    OverzichtSportbril SchietbrillenWindow = new OverzichtSportbril();
                    MainWindowViewModel SchietbrillenViewModel = new MainWindowViewModel();
                    SchietbrillenWindow.DataContext = SchietbrillenViewModel;
                    SchietbrillenWindow.Show();
                    break;
                case "Zonnebril":
                    OverzichtSportbril ZonnebrillenWindow = new OverzichtSportbril();
                    MainWindowViewModel ZonnebrillenViewModel = new MainWindowViewModel();
                    ZonnebrillenWindow.DataContext = ZonnebrillenViewModel;
                    ZonnebrillenWindow.Show();
                    break;


                case "BrilAanmaken":
                    BrillenBeheren BrillenbeherenWindow = new BrillenBeheren();
                    BrillenBeherenViewModel BrillenbeherenViewModel = new BrillenBeherenViewModel();
                    BrillenbeherenWindow.DataContext = BrillenbeherenViewModel;
                    BrillenbeherenWindow.Show();
                    break;

            }
        }
    }
}
