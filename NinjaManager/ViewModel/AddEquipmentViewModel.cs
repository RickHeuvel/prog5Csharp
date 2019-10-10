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
    public class AddEquipmentViewModel
    {
        private MainViewModel _mainModel;

        public List<EquipmentCategoryViewModel> Categories { get; set; }

        public string Name { get; set; }
        public int Strenght { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int Price { get; set; }
        public EquipmentCategoryViewModel SelectedCategory { get; set; }

        public ICommand AddEquipmentCommand { get; set; }


        public AddEquipmentViewModel(MainViewModel main)
        {
            _mainModel = main;
        
            getCategories();

            AddEquipmentCommand = new RelayCommand(AddEquipment, canAddEquipment);
        }

        private void getCategories()
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
                    Strength = Strenght,
                    Intelligence = Intelligence,
                    Agility = Agility,
                    CategoryId = SelectedCategory.CategoryId,
                    Price = Price
                };

                context.Equipments.Add(e);
                context.SaveChanges();
                _mainModel.Equipment.Add(e.ToPoCo());
            }

            _mainModel.CloseAddEquipment();
        }

        private bool canAddEquipment()
        {
            return true;
        }
    }
}
