using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class AddNinjaViewModel
    {
        private MainViewModel _mainModel;

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _gold;

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }


        public ICommand AddNinjaCommand { get; set; }

        public AddNinjaViewModel(MainViewModel main)
        {
            _mainModel = main;

            AddNinjaCommand = new RelayCommand(AddNinja, CanAddNinja);
        }

        private bool CanAddNinja()
        {
            return true;
        }

        private void AddNinja()
        {
            using (var context = new NinjaDBEntities())
            {
                Ninja n = new Ninja { Name = Name, Gold = Gold };

                context.Ninjas.Add(n);
                context.SaveChanges();
                _mainModel.Ninjas.Add(n.toPoCo());
           
            }

            _mainModel.CloseAddNinja();
        }
    }
}
