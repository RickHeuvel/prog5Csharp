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

        private NinjaViewModel _selectedNinja;
        public NinjaViewModel SelectedNinja
        { 
            get { return _selectedNinja; } 
            set { _selectedNinja = value; RaisePropertyChanged(); } 
        }


        //commands
        public ICommand EditNinjaCommand { get; set; }
        public EditNinjaViewModel(MainViewModel main)
        {
            _mainModel = main;
            SelectedNinja = _mainModel.SelectedNinja;

            EditNinjaCommand = new RelayCommand(EditNinja, CanEditNinja);
           
        }

        private bool CanEditNinja()
        {
           /* if (SelectedNinja != null)
            {
                return true;
            }
            return false;
*/
            return true;
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