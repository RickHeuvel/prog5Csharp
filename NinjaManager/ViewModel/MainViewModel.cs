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
        private ObservableCollection<NinjaViewModel> _ninjas;

        public ObservableCollection<NinjaViewModel> Ninjas
        {
            get { return _ninjas; }
            set { _ninjas = value; RaisePropertyChanged(); }
        }

        private NinjaViewModel _selectedNinja;
        public NinjaViewModel SelectedNinja
        {
            get { return _selectedNinja; }
            set { _selectedNinja = value; RaisePropertyChanged();}
        }

        private ObservableCollection<EquipmentViewModel> _equipment;

        public ObservableCollection<EquipmentViewModel> Equipment
        {
            get { return _equipment; }
            set { _equipment = value; RaisePropertyChanged(); }
        }

        private EquipmentViewModel _selectedEquipment;

        public EquipmentViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value; RaisePropertyChanged(); }
        }


        //windows
        private EditNinjaWindow _editNinjaWindow;
        private AddNinjaWindow _addNinjaWindow;
        private NinjaOverviewWindow _ninjaOverviewWindow;
        private AddEquipmentWindow _addEquipmentWindow;

        //commands 
        public ICommand ShowEditNinjaCommand { get; set; }
        public ICommand ShowAddNinjaCommand { get; set; }
        public ICommand ShowNinjaOverviewCommand { get; set; }
        public ICommand DeleteNinjaCommand { get; set; }
        public ICommand ShowAddEquipmentCommand { get; set; }
        public ICommand DeleteEquipmentCommand { get; set; }
        public MainViewModel()
        {
            _ninjas = new ObservableCollection<NinjaViewModel>();
            getAllNinjas();

            _equipment = new ObservableCollection<EquipmentViewModel>();
            getAllEquipment();

            ShowEditNinjaCommand = new RelayCommand(ShowEditNinja);
            ShowAddNinjaCommand = new RelayCommand(ShowAddNinja);
            ShowNinjaOverviewCommand = new RelayCommand(ShowNinjaOverview);
            DeleteNinjaCommand = new RelayCommand(DeleteNinja);

            ShowAddEquipmentCommand = new RelayCommand(ShowAddEquipment);
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

        #region Add Ninja
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

        #region Delete Ninja
        private void DeleteEquipment()
        {
            using (var context = new NinjaDBEntities())
            {
                var equipment = context.Equipments.ToList().Find(e => e.Id == SelectedEquipment.Id);
                context.Equipments.Remove(equipment);
                context.SaveChanges();

                Equipment.Remove(equipment.ToPoCo());
                RaisePropertyChanged("Equipment");
            }
        }
        #endregion
        #endregion

    }
}