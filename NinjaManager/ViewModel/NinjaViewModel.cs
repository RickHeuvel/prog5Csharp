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
