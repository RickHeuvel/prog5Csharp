using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class AddNinjaViewModel: ViewModelBase
    {
        private MainViewModel _mainModel;

        private string _name;
        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged("AddNinjaCommand"); } }

        private int _gold;

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; RaisePropertyChanged("AddNinjaCommand"); }
        }



        public RelayCommand AddNinjaCommand { get { return new RelayCommand(AddNinja,CanAddNinja); }}


        public AddNinjaViewModel(MainViewModel main)
        {
            _mainModel = main;
            Name = "";
        }

        private bool CanAddNinja()
        {
            if (Name.Length > 0 && !Name.StartsWith(" ") && Gold > 0)
            {
                return true;
            }
            return false;
        }

        private void AddNinja()
        {
            using (var context = new NinjaDBEntities())
            {
                Ninja n = new Ninja { Name = Name, Gold = Gold };

                context.Ninjas.Add(n);
                context.SaveChanges();
                _mainModel.Ninjas.Add(n.ToPoCo());
           
            }

            _mainModel.CloseAddNinja();
        }
    }
}
