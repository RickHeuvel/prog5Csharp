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

        public string Name { get; set; }

        public int Gold { get; set; }


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
                _mainModel.Ninjas.Add(n.ToPoCo());
           
            }

            _mainModel.CloseAddNinja();
        }
    }
}
