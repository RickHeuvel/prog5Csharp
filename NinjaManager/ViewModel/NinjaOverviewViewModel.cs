using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaManager.ViewModel
{
    public class NinjaOverviewViewModel: ViewModelBase
    {
        private MainViewModel _mainModel;

        private NinjaViewModel _selectedNinja;
        public NinjaViewModel SelectedNinja
        {
            get { return _selectedNinja; }
            set { _selectedNinja = value; RaisePropertyChanged(); }
        }

        public NinjaOverviewViewModel(MainViewModel main)
        {
            _mainModel = main;
            SelectedNinja = _mainModel.SelectedNinja;
        }
    }
}
