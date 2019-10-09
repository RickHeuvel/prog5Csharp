using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaManager.ViewModel
{
    public class NinjaViewModel: ViewModelBase
    {

        private Ninja _ninja;
        public int Id
        {
            get { return _ninja.Id; }
            set { _ninja.Id = value; RaisePropertyChanged("Id"); }
        }

        public string Name
        {
            get { return _ninja.Name; }
            set { _ninja.Name = value; RaisePropertyChanged("Name"); }
        }

        public int Gold
        {
            get { return _ninja.Gold; }
            set { _ninja.Gold = value; RaisePropertyChanged("Gold"); }
        }

        public int Strenght 
        {
            get { return _ninja.Strenght; }
            set { _ninja.Strenght = value; RaisePropertyChanged("Strenght") ; } 
        }

        public int Intelligence
        { 
            get { return _ninja.Intelligence; }
            set { _ninja.Intelligence = value; RaisePropertyChanged("Intelligence"); } 
        }

        public int Agility
        {
            get { return _ninja.Agility; }
            set { _ninja.Agility = value; RaisePropertyChanged("Agility"); }
        }

        public NinjaViewModel()
        {
            _ninja = new Ninja();
        }

        public NinjaViewModel(Ninja ninja)
        {
            _ninja = ninja;
        }

    }
}
