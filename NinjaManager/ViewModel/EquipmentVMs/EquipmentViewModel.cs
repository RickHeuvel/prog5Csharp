using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaManager.ViewModel
{
    public class EquipmentViewModel : ViewModelBase
    {
        private NinjaManager.Equipment _equipment;

        public int Id
        {
            get { return _equipment.Id; }
            set { _equipment.Id = value; RaisePropertyChanged("Id"); }
        }

        public string Name
        {
            get { return _equipment.Name; }
            set { _equipment.Name = value; RaisePropertyChanged("Name");}
        }

        public int Strength
        {
            get { return _equipment.Strength; }
            set { _equipment.Strength = value; RaisePropertyChanged("Strength"); }
        }

        public int Intelligence
        {
            get { return _equipment.Intelligence; }
            set { _equipment.Intelligence = value; RaisePropertyChanged("Intelligence"); }
        }

        public int Agility
        {
            get { return _equipment.Agility; }
            set { _equipment.Agility = value; RaisePropertyChanged("Agility"); }
        }

        public int CategoryId
        {
            get { return _equipment.CategoryId; }
            set { _equipment.CategoryId = value; RaisePropertyChanged("CategoryId"); }
        }

        public EquipmentCategoryViewModel Category
        {
            get {
                    using (var context = new NinjaDBEntities())
                    {
                        return new EquipmentCategoryViewModel(context.Equipments.ToList().Where(e => e.Id == _equipment.Id).First().EquipmentCategory);
                    }
                }
            set
            {
                using (var context = new NinjaDBEntities())
                {
                    _equipment.EquipmentCategory = context.EquipmentCategories.Single(e => e.CategoryId == value.CategoryId);
                    RaisePropertyChanged("Category");
                }
                
            }
        }

        public int Price 
        {
            get { return _equipment.Price; }
            set { _equipment.Price = value; RaisePropertyChanged("Price"); } 
        }

        public EquipmentViewModel(Equipment equipment)
        {
            _equipment = equipment;
        }
    }
}
