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
        public ObservableCollection<NinjaViewModel> Ninjas { get; set; }
        public NinjaViewModel SelectedNinja { get; set; }

        public ObservableCollection<EquipmentViewModel> Equipment { get; set; }


        //  public ObservableCollection<EquipmentViewModel> Equipment { get; set; }

        //     public EquipmentViewModel SelectedEquipment { get; set; }

        private EquipmentViewModel _selectedEquipment;

        public EquipmentViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value;
                RaisePropertyChanged("DeleteEquipmentCommand");
            }
        }



        //windows
        private ManageNinjasWindow _manageNinjasWindow;
        private ManageEquipmentWindow _manageEquipmentWindow;

        private EditNinjaWindow _editNinjaWindow;
        private AddNinjaWindow _addNinjaWindow;
        private NinjaOverviewWindow _ninjaOverviewWindow;
        private AddEquipmentWindow _addEquipmentWindow;
        private EditEquipmentWindow _editEquipmentWindow;

        //commands 
        public RelayCommand ShowManageNinjasCommand { get { return new RelayCommand(ShowManageNinjas); } }
        public RelayCommand ShowManageEquipmentCommand { get { return new RelayCommand(ShowManageEquipment); } }

        public ICommand ShowEditNinjaCommand { get; set; }
        public ICommand ShowAddNinjaCommand { get; set; }
        public ICommand ShowNinjaOverviewCommand { get; set; }
        public ICommand DeleteNinjaCommand { get; set; }
        public ICommand ShowAddEquipmentCommand { get; set; }
        public RelayCommand<int> DeleteEquipmentCommand { get { return new RelayCommand<int>(DeleteEquipment, CanDeleteEquipment); } }
        public ICommand ShowEditEquipmentCommand { get; set; }
        public MainViewModel()
        {
            Ninjas = new ObservableCollection<NinjaViewModel>();
            getAllNinjas();

            Equipment = new ObservableCollection<EquipmentViewModel>();
            getAllEquipment();

            ShowEditNinjaCommand = new RelayCommand(ShowEditNinja);
            ShowAddNinjaCommand = new RelayCommand(ShowAddNinja);
            ShowNinjaOverviewCommand = new RelayCommand(ShowNinjaOverview);
            DeleteNinjaCommand = new RelayCommand(DeleteNinja);
            ShowAddEquipmentCommand = new RelayCommand(ShowAddEquipment);
            ShowEditEquipmentCommand = new RelayCommand(ShowEditEquipment);
      //      DeleteEquipmentCommand = new RelayCommand<int>(DeleteEquipment, CanDeleteEquipment);

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

        private void getAllNinjas()
        {
            using (var context = new NinjaDBEntities())
            {
                context.Ninjas.ToList().ForEach(n => Ninjas.Add(new NinjaViewModel(n)));
            }
        }

        private void getAllEquipment()
        {
            using (var context = new NinjaDBEntities())
            {
                context.Equipments.ToList().ForEach(e => Equipment.Add(new EquipmentViewModel(e)));
            }
        }

        #region NINJA
        #region Edit ninja
        private void ShowEditNinja()
        {
            _editNinjaWindow = new EditNinjaWindow();
            _editNinjaWindow.Show();
        }

        public void CloseEditNinja()
        {
            _editNinjaWindow.Close();
        }
        #endregion
        #region Add ninja
        private void ShowAddNinja()
        {
            _addNinjaWindow = new AddNinjaWindow();
            _addNinjaWindow.Show();
        }

        public void CloseAddNinja()
        {
            _addNinjaWindow.Close();
        }
        #endregion
        #region Delete ninja
        private void DeleteNinja()
        {
            using (var context = new NinjaDBEntities())
            {
                var ninja = context.Ninjas.ToList().Find(n => n.Id == SelectedNinja.Id);
                ninja.Equipments.Clear();
                context.Ninjas.Remove(ninja);
                context.SaveChanges();

                Ninjas.Remove(Ninjas.ToList().Find(n => n.Id == ninja.Id));
            }

        }
        #endregion
        #region Show ninja
        private void ShowNinjaOverview()
        {
            _ninjaOverviewWindow = new NinjaOverviewWindow();
            _ninjaOverviewWindow.Show();
        }
        #endregion


        #endregion

        #region Equipment

        #region Add Equipment
        private void ShowAddEquipment()
        {
            _addEquipmentWindow = new AddEquipmentWindow();
            _addEquipmentWindow.Show();
        }

        public void CloseAddEquipment()
        {
            _addEquipmentWindow.Close();
        }
        #endregion

        #region Delete Equipment

        // check if any ninja has the equipment i want to delete if so return false
        private bool CanDeleteEquipment(int id)
        {
            using (var context = new NinjaDBEntities())
            {
                var equipment = context.Equipments.ToList().Find(e => e.Id == id);

                if (equipment != null && equipment.Ninjas.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }
        private void DeleteEquipment(int id)
        {
            using (var context = new NinjaDBEntities())
            {
                var equipment = context.Equipments.ToList().Find(e => e.Id == id);
                context.Equipments.Remove(equipment);
                context.SaveChanges();

                Equipment.Remove(Equipment.ToList().Find(e => e.Id == equipment.Id));
            }
        }
        #endregion

        #region Edit Equipment
        private void ShowEditEquipment()
        {
            _editEquipmentWindow = new EditEquipmentWindow();
            _editEquipmentWindow.Show();
        }

        public void CloseEditEquipment()
        {
            _editEquipmentWindow.Close();
        }
        #endregion
        #endregion

    }
}