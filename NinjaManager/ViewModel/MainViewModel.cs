using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;

namespace NinjaManager.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Ninja> _ninjas;

        public ObservableCollection<Ninja> Ninjas
        {
            get { return _ninjas; }
            set { _ninjas = value; }
        }


        public MainViewModel()
        {
            _ninjas = new ObservableCollection<Ninja>();
            getAllNinjas();
        }

        private void getAllNinjas()
        {
            using (var context = new NinjaDBEntities())
            {
                context.Ninjas.ToList().ForEach(n => Ninjas.Add(n));
            }
        }
    }
}