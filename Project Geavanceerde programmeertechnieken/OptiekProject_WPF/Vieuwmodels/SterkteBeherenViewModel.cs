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
    class SterkteBeherenViewModel : BasisViewModel
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
                case "SterkteAanmaken":
                    //Nieuwe sterkte aanmaken
                    break;
            }
        }
    }
}
