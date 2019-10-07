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

        private NinjaViewModel _selectedNinja;
        public NinjaViewModel SelectedNinja
        {
            get { return _selectedNinja; }
            set { _selectedNinja = value; RaisePropertyChanged();}
        }

        //windows
        private EditNinjaWindow _editNinjaWindow;
        //commands 
        public ICommand ShowEditNinjaCommand { get; set; }
        public MainViewModel()
        {
            _ninjas = new ObservableCollection<NinjaViewModel>();
            getAllNinjas();

            ShowEditNinjaCommand = new RelayCommand(ShowEditNinja);
        }


        #region EDIT ninja
        private void ShowEditNinja()
        {
            _editNinjaWindow = new EditNinjaWindow();
            _editNinjaWindow.Show();
        }

        public void CloseEditNinja()
        {
            _editNinjaWindow.Close();
        }
        #endregion
        private void getAllNinjas()
        {
            using (var context = new NinjaDBEntities())
            {
                context.Ninjas.ToList().ForEach(n => Ninjas.Add(new NinjaViewModel(n)));
            }
        }
    }
}