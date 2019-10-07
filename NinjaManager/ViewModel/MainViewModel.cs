using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using NinjaManager.View;

namespace NinjaManager.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<NinjaViewModel> _ninjas;

        public ObservableCollection<NinjaViewModel> Ninjas
        {
            get { return _ninjas; }
            set { _ninjas = value; RaisePropertyChanged(); }
        }

        //windows
        private ManageNinjasWindow _manageNinjasWindow;
        //commands 
        public ICommand ShowManageNinjasCommand { get; set; }
        public MainViewModel()
        {
            _ninjas = new ObservableCollection<NinjaViewModel>();
            getAllNinjas();

            ShowManageNinjasCommand = new RelayCommand(ShowManageNinjas);
        }

        private void ShowManageNinjas()
        {
            _manageNinjasWindow = new ManageNinjasWindow();
            _manageNinjasWindow.Show();
        }

        private void getAllNinjas()
        {
            using (var context = new NinjaDBEntities())
            {
                context.Ninjas.ToList().ForEach(n => Ninjas.Add(new NinjaViewModel(n)));
            }
        }
    }
}