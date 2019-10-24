using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel.EquipmentVMs
{
    public class ManageEquipmentViewModel: ViewModelBase
    {
        public ObservableCollection<EquipmentViewModel> Equipment { get; set; }

        private EquipmentViewModel _selectedEquipment;

        public EquipmentViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                RaisePropertyChanged("DeleteEquipmentCommand");
            }
        }



        //windows
        private AddEquipmentWindow _addEquipmentWindow;
        private EditEquipmentWindow _editEquipmentWindow;

        //commands 
        public ICommand ShowAddEquipmentCommand { get; set; }
        public RelayCommand<int> DeleteEquipmentCommand { get { return new RelayCommand<int>(DeleteEquipment, CanDeleteEquipment); } }
        public ICommand ShowEditEquipmentCommand { get; set; }
       
        public ManageEquipmentViewModel()
        {
            Equipment = new ObservableCollection<EquipmentViewModel>();
            getAllEquipment();

            ShowAddEquipmentCommand = new RelayCommand(ShowAddEquipment);
            ShowEditEquipmentCommand = new RelayCommand(ShowEditEquipment);

        }

        private void getAllEquipment()
        {
            using (var context = new NinjaDBEntities())
            {
                context.Equipments.ToList().ForEach(e => Equipment.Add(new EquipmentViewModel(e)));
            }
        }

 
        private void ShowAddEquipment()
        {
            _addEquipmentWindow = new AddEquipmentWindow();
            _addEquipmentWindow.Show();
        }

        public void CloseAddEquipment()
        {
            _addEquipmentWindow.Close();
        }

        // check if any ninja has the equipment i want to delete if so return false
        private bool CanDeleteEquipment(int id)
        {
            return true;
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
                equipment.Ninjas.ToList().ForEach(n => n.Equipments.Remove(equipment));
                context.Equipments.Remove(equipment);
                context.SaveChanges();

                Equipment.Remove(Equipment.ToList().Find(e => e.Id == equipment.Id));
            }
        }
        private void ShowEditEquipment()
        {
            _editEquipmentWindow = new EditEquipmentWindow();
            _editEquipmentWindow.Show();
        }

        public void CloseEditEquipment()
        {
            _editEquipmentWindow.Close();
        }

    }
}
