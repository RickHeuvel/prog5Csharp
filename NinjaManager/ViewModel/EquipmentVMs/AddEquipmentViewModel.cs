using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.ViewModel;
using NinjaManager.ViewModel.EquipmentVMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace NinjaManager.ViewModel
{
    public class AddEquipmentViewModel : ViewModelBase
    {
        private ManageEquipmentViewModel _manageEquipment;

        public List<EquipmentCategoryViewModel> Categories { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("AddEquipmentCommand"); }
        }

        private string _strenght;
        public string Strength 
        { 
            get { return _strenght; }
            set { _strenght = value; RaisePropertyChanged("AddEquipmentCommand"); } 
        }

        private string _intelligence;
        public string Intelligence 
        { 
            get { return _intelligence; } 
            set { _intelligence = value; RaisePropertyChanged("AddEquipmentCommand"); } 
        }

        private string _agility;
        public string Agility 
        { 
            get { return _agility; } 
            set { _agility = value; RaisePropertyChanged("AddEquipmentCommand"); }
        }

        private string _price;
        public string Price
        {
            get { return _price; } 
            set { _price = value; RaisePropertyChanged("AddEquipmentCommand"); } 
        }

        private EquipmentCategoryViewModel _selectedCategory;
        public EquipmentCategoryViewModel SelectedCategory
        { 
            get { return _selectedCategory; } 
            set { _selectedCategory = value; RaisePropertyChanged("AddEquipmentCommand"); } 
        }

        public RelayCommand AddEquipmentCommand { get { return new RelayCommand(AddEquipment, CanAddEquipment); } }


        public AddEquipmentViewModel(ManageEquipmentViewModel manageEquipment)
        {
            _manageEquipment = manageEquipment;

            GetCategories();
        }

        private void GetCategories()
        {
            Categories = new List<EquipmentCategoryViewModel>();
            using (var context = new NinjaDBEntities())
            {
                context.EquipmentCategories.ToList().ForEach(c => Categories.Add(c.ToPoCo()));
            }
            SelectedCategory = Categories[0];
        }

        private void AddEquipment()
        {
            using (var context = new NinjaDBEntities())
            {
                Equipment e = new Equipment
                {
                    Name = Name,
                    Strength = int.Parse(Strength),
                    Intelligence = int.Parse(Intelligence),
                    Agility = int.Parse(Agility),
                    CategoryId = SelectedCategory.CategoryId,
                    Price = int.Parse(Price),
                };

                context.Equipments.Add(e);
                context.SaveChanges();
                _manageEquipment.Equipment.Add(e.ToPoCo());
            }

            _manageEquipment.CloseAddEquipment();
        }

        private bool CanAddEquipment()
        {
            if (Name == null || Name.Length < 1 || Name.StartsWith(" "))
            {
                return false;
            }
            else if (!int.TryParse(Strength, out _))
            {
                return false;
            }
            else if (!int.TryParse(Intelligence, out _))
            {
                return false;
            }
            else if (!int.TryParse(Agility, out _))
            {
                return false;
            }
            else if (!int.TryParse(Price, out int result))
            {
                return false;
            }
            else if (result < 1)
            {
                return false;
            }
            return true;

        }
    }
}
