using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using OptiekProject_WPF.VieuwModels;
// using OptiekProject_WPF.Vieuws;

namespace OptiekProject_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainView = new MainWindow();
            MainWindowViewModel mainViewModel = new MainWindowViewModel();
            mainView.DataContext = mainViewModel;
            mainView.Show();
        }
    }
}
