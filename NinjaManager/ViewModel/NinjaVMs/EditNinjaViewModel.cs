using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.ViewModel.NinjaVMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class EditNinjaViewModel : ViewModelBase
    {
        private ManageNinjasViewModel _manageNinjasModel;

        public NinjaViewModel SelectedNinja { get; set; }

        public string Name
        {
            get { return SelectedNinja.Name; }
            set { SelectedNinja.Name = value; RaisePropertyChanged("EditNinjaCommand"); }
        }

        private string _gold;
        public string Gold 
        { 
            get { return _gold;}
            set { _gold = value; RaisePropertyChanged("EditNinjaCommand"); }
        }

        //commands
        public RelayCommand EditNinjaCommand { get { return new RelayCommand(EditNinja, CanEditNinja); } }
        
        public EditNinjaViewModel(ManageNinjasViewModel manageNinjas)
        {
            _manageNinjasModel = manageNinjas;
            SelectedNinja = _manageNinjasModel.SelectedNinja;

            Gold = SelectedNinja.Gold.ToString();
        }

        private bool CanEditNinja()
        {
            if (int.TryParse(Gold, out _))
            {
                SelectedNinja.Gold = int.Parse(Gold);

                if (Name.Length > 0 && !Name.StartsWith(" ") && SelectedNinja.Gold > 0)
                {
                    return true;
                }
            }
            return false;

        }

        private void EditNinja()
        {
            using (var context = new NinjaDBEntities())
            {
                var ninja = context.Ninjas.Single(n => n.Id == SelectedNinja.Id);

                if (ninja != null)
                {
                    ninja.Name = SelectedNinja.Name;
                    ninja.Gold = SelectedNinja.Gold;
                    context.SaveChanges();
                }
            }
            _manageNinjasModel.CloseEditNinja();
        }
    }
}
