using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using NinjaManager.View;
using System.Collections.Generic;
using NinjaManager.View.Ninja;
using NinjaManager.View.Equipment;

namespace NinjaManager.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {


        //windows
        private ManageNinjasWindow _manageNinjasWindow;
        private ManageEquipmentWindow _manageEquipmentWindow;

        //commands 
        public RelayCommand ShowManageNinjasCommand { get { return new RelayCommand(ShowManageNinjas); } }
        public RelayCommand ShowManageEquipmentCommand { get { return new RelayCommand(ShowManageEquipment); } }

   
        public MainViewModel()
        {

        }

        private void ShowManageNinjas()
        {
            _manageNinjasWindow = new ManageNinjasWindow();
            _manageNinjasWindow.Show();
        }

        private void ShowManageEquipment()
        {
            _manageEquipmentWindow = new ManageEquipmentWindow();
            _manageEquipmentWindow.Show();
        }

    }
}