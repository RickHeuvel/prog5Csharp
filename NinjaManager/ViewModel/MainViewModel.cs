using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using NinjaManager.View;

namespace NinjaManager.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<NinjaViewModel> Ninjas { get; set; }
        public NinjaViewModel SelectedNinja { get; set; }

        public ObservableCollection<EquipmentViewModel> Equipment { get; set; }

        public EquipmentViewModel SelectedEquipment { get; set; }


        //windows
        private EditNinjaWindow _editNinjaWindow;
        private AddNinjaWindow _addNinjaWindow;
        private NinjaOverviewWindow _ninjaOverviewWindow;
        private AddEquipmentWindow _addEquipmentWindow;
        private EditEquipmentWindow _editEquipmentWindow;

        //commands 
        public ICommand ShowEditNinjaCommand { get; set; }
        public ICommand ShowAddNinjaCommand { get; set; }
        public ICommand ShowNinjaOverviewCommand { get; set; }
        public ICommand DeleteNinjaCommand { get; set; }
        public ICommand ShowAddEquipmentCommand { get; set; }
        public ICommand DeleteEquipmentCommand { get; set; }
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
            DeleteEquipmentCommand = new RelayCommand(DeleteEquipment);
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
                context.Ninjas.Remove(ninja);
                context.SaveChanges();

                Ninjas.Remove(ninja.ToPoCo());
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
        private void DeleteEquipment()
        {
            using (var context = new NinjaDBEntities())
            {
                var equipment = context.Equipments.ToList().Find(e => e.Id == SelectedEquipment.Id);
                context.Equipments.Remove(equipment);
                context.SaveChanges();

                Equipment.Remove(equipment.ToPoCo());
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