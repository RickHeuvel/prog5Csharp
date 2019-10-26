using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaManager.ViewModel
{
    public class NinjaViewModel : ViewModelBase
    {

        private Ninja _ninja;

        private int _gearValue;
        public int Id
        {
            get { return _ninja.Id; }
            set { _ninja.Id = value; RaisePropertyChanged("Id"); }
        }

        public string Name
        {
            get { return _ninja.Name; }
            set { _ninja.Name = value; RaisePropertyChanged("Name"); }
        }

        public int Gold
        {
            get { return _ninja.Gold; }
            set { _ninja.Gold = value; RaisePropertyChanged("Gold"); }
        }

        public int Strenght
        {
            get { return _ninja.Strenght; }
            set { _ninja.Strenght = value; RaisePropertyChanged("Strenght"); }
        }

        public int Intelligence
        {
            get { return _ninja.Intelligence; }
            set { _ninja.Intelligence = value; RaisePropertyChanged("Intelligence"); }
        }

        public int Agility
        {
            get { return _ninja.Agility; }
            set { _ninja.Agility = value; RaisePropertyChanged("Agility"); }
        }


        private ObservableCollection<EquipmentViewModel> _equipment;

        public ObservableCollection<EquipmentViewModel> Equipments
        {

            get
            {
              return _equipment;
            }
            set
            {
                ObservableCollection<Equipment> collection = new ObservableCollection<Equipment>();
                value.ToList().ForEach(e => collection
                    .Add(new Equipment
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Strength = e.Strength,
                        Intelligence = e.Intelligence,
                        Agility = e.Agility,
                        CategoryId = e.CategoryId,
                        Price = e.Price
                    }
                    ));
                _ninja.Equipments = collection;
                Equipments = _ninja.EquipmentsToPoCo();
                RaisePropertyChanged("Equipments");
            }
        }

        //void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.NewItems != null)
        //    {
        //        foreach (EquipmentViewModel newEquipment in e.NewItems)
        //        {
        //            _ninja.Equipments.Add(new Equipment
        //            {
        //                Id = newEquipment.Id,
        //                Name = newEquipment.Name,
        //                Strength = newEquipment.Strength,
        //                Intelligence = newEquipment.Intelligence,
        //                Agility = newEquipment.Agility,
        //                CategoryId = newEquipment.CategoryId,
        //                Price = newEquipment.Price
        //            });
        //        }
                
                
        //    }

        //    if (e.OldItems != null)
        //    {
        //        foreach (EquipmentViewModel equipment in e.OldItems)
        //        {
        //            Equipment toRemove = _ninja.Equipments.Single(eq => eq.Id == equipment.Id);
        //            _ninja.Equipments.Remove(toRemove);
        //        }
        //    }
            
        //}

        public int GearValue 
        {
            get
            {
                int value = 0;

                using (var context = new NinjaDBEntities())
                {
                    context.Ninjas.Single(n => n.Id == _ninja.Id).Equipments.ToList().ForEach(e => value += e.Price);
                }
            
                return value;
            }
            set { _gearValue = value; RaisePropertyChanged("GearValue"); }
        }


 
        public NinjaViewModel(Ninja ninja)
        {
            _ninja = ninja;
            _equipment = _ninja.EquipmentsToPoCo();
          //  Equipments.CollectionChanged += OnCollectionChanged;
        }
    }
}
