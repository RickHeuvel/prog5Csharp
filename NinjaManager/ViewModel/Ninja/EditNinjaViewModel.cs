﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        private MainViewModel _mainModel;

        public NinjaViewModel SelectedNinja { get; set; }

        public string Name
        {
            get { return SelectedNinja.Name; }
            set { SelectedNinja.Name = value; RaisePropertyChanged("EditNinjaCommand"); }
        }

        public int Gold 
        { 
            get { return SelectedNinja.Gold; }
            set { SelectedNinja.Gold = value; RaisePropertyChanged("EditNinjaCommand"); }
        }
        //commands
        public RelayCommand EditNinjaCommand { get { return new RelayCommand(EditNinja, CanEditNinja); } }
        
        public EditNinjaViewModel(MainViewModel main)
        {
            _mainModel = main;
            SelectedNinja = _mainModel.SelectedNinja;
        }

        private bool CanEditNinja()
        {
            if (Name.Length > 0 && !Name.StartsWith(" ") && Gold > 0)
            {
                return true;
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
            _mainModel.CloseEditNinja();
        }
    }
}
