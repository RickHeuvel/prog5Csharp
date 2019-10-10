using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaManager.ViewModel
{
    public class EquipmentCategoryViewModel: ViewModelBase
    {
        private EquipmentCategory _equipmentCategory;

        public EquipmentCategoryViewModel(EquipmentCategory equipmentCategory)
        {
            _equipmentCategory = equipmentCategory;
        }

        public int CategoryId 
        { 
            get { return _equipmentCategory.CategoryId; }
            set { _equipmentCategory.CategoryId = value; RaisePropertyChanged("CategoryId"); }
        }

        public string Name 
        {
            get { return _equipmentCategory.Name; }
            set { _equipmentCategory.Name = value; RaisePropertyChanged("Name"); } 
        }

    }
}
