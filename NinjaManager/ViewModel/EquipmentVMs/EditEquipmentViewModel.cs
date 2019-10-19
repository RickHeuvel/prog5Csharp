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
using NinjaManager.ViewModel.EquipmentVMs;
using NinjaManager.ViewModel.NinjaVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class EditEquipmentViewModel : ViewModelBase
    {
        private ManageEquipmentViewModel _manageEquipment;
        private ManageNinjasViewModel _manageNinjas;

        public List<EquipmentCategoryViewModel> Categories { get; set; }
        public EquipmentViewModel SelectedEquipment { get; set; }

        public EquipmentCategoryViewModel SelectedCategory { get; set; }


        public RelayCommand EditEquipmentCommand { get { return new RelayCommand(EditEquipment, CanEditEquipment); } }

        public EditEquipmentViewModel(ManageEquipmentViewModel manageEquipment, ManageNinjasViewModel manageNinjas)
        {
            _manageEquipment = manageEquipment;
            _manageNinjas = manageNinjas;
            SelectedEquipment = _manageEquipment.SelectedEquipment;

            GetCategories();

        }

        private void GetCategories()
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

            SelectedEquipment.Category = SelectedCategory;

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
            _manageEquipment.CloseEditEquipment();
        }

        private bool CanEditEquipment()
        {
            return true;
        }
    }
}