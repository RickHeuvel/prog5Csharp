using GalaSoft.MvvmLight;
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
    public class ShopViewModel: ViewModelBase
    {
        private MainViewModel _mainModel;

        public ObservableCollection<EquipmentViewModel> Equipment { get; set; }

        private String _selectedCategory;

        public String SelectedCategory
        {
            get { return _selectedCategory; }
            set 
            {
                _selectedCategory = value; RaisePropertyChanged("SelectedCategory");
                ShowSelectedCategory(value);
            }
        }

        public ObservableCollection<EquipmentViewModel> SelectedEquipment { get; set; }

        public ICommand BtnSelectCommand { get; set; }

        public ShopViewModel(MainViewModel main)
        {
            _mainModel = main;
            SelectedEquipment = new ObservableCollection<EquipmentViewModel>();
            Equipment = _mainModel.Equipment;
           
            SelectedCategory = "Head";
           
            BtnSelectCommand = new RelayCommand<string>(BtnSelectClick);
          
        }

        private void BtnSelectClick(string cat)
        {
            SelectedCategory = cat;
        }

      

        private void ShowSelectedCategory(string cat)
        {
            SelectedEquipment.Clear(); 
            Equipment.ToList().FindAll(e => e.Category.Name == cat).ForEach(e => SelectedEquipment.Add(e));
        }


    }
}
