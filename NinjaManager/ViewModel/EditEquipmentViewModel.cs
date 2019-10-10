/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:NinjaManager"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class EditEquipmentViewModel : ViewModelBase
    {
        private MainViewModel _mainModel;

        private EquipmentViewModel _selectedEquipment;

        public List<EquipmentCategoryViewModel> Categories { get; set; }
        public EquipmentViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value; RaisePropertyChanged(); }
        }

        public EquipmentCategoryViewModel SelectedCategory { get; set; }


        public ICommand EditEquipmentCommand {get; set; }

        public EditEquipmentViewModel(MainViewModel main)
        {
            _mainModel = main;
            SelectedEquipment = _mainModel.SelectedEquipment;

            getCategories();

            EditEquipmentCommand = new RelayCommand(EditEquipment);
        }

        private void getCategories()
        {
            Categories = new List<EquipmentCategoryViewModel>();
            using (var context = new NinjaDBEntities())
            {
                context.EquipmentCategories.ToList().ForEach(c => Categories.Add(c.ToPoCo()));

                SelectedCategory = new EquipmentCategoryViewModel(context.Equipments
                    .Single(e => e.Id == SelectedEquipment.Id).EquipmentCategory);
            }
         
        }
        private void EditEquipment()
        {

            _selectedEquipment.Category = SelectedCategory;

            using (var context = new NinjaDBEntities())
            {
                var equipment = context.Equipments.Single(e => e.Id == SelectedEquipment.Id);

                if (equipment != null)
                {
                    equipment.Name = SelectedEquipment.Name;
                    equipment.Strength = SelectedEquipment.Strength;
                    equipment.Intelligence = SelectedEquipment.Intelligence;
                    equipment.Agility = SelectedEquipment.Agility;
                    equipment.CategoryId = SelectedCategory.CategoryId;
                    equipment.Price = SelectedEquipment.Price;

                    context.SaveChanges();
                }
            }
        }
    }
}