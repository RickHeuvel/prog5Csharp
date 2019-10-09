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
        private AddNinjaWindow _addNinjaWindow;
        private NinjaOverviewWindow _ninjaOverviewWindow;

        //commands 
        public ICommand ShowEditNinjaCommand { get; set; }
        public ICommand ShowAddNinjaCommand { get; set; }

        public ICommand ShowNinjaOverviewCommand { get; set; }
        public ICommand DeleteNinjaCommand { get; set; }
        public MainViewModel()
        {
            _ninjas = new ObservableCollection<NinjaViewModel>();
            getAllNinjas();

            ShowEditNinjaCommand = new RelayCommand(ShowEditNinja);
            ShowAddNinjaCommand = new RelayCommand(ShowAddNinja);
            ShowNinjaOverviewCommand = new RelayCommand(ShowNinjaOverview);
            DeleteNinjaCommand = new RelayCommand(DeleteNinja);
        }

        private void getAllNinjas()
        {
            Ninjas.Clear();
            using (var context = new NinjaDBEntities())
            {
                context.Ninjas.ToList().ForEach(n => Ninjas.Add(new NinjaViewModel(n)));
            }
        }

        #region NINJA
        #region Edit ninja
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
        #region Add ninja
        private void ShowAddNinja()
        {
            _addNinjaWindow = new AddNinjaWindow();
            _addNinjaWindow.Show();
        }

        public void CloseAddNinja()
        {
            _addNinjaWindow.Close();
        }
        #endregion
        #region Delete ninja
        private void DeleteNinja()
        {
            using (var context = new NinjaDBEntities())
            {
                var ninja = context.Ninjas.ToList().Find(n => n.Id == SelectedNinja.Id);
                context.Ninjas.Remove(ninja);
                context.SaveChanges();

                Ninjas.Remove(ninja.toPoCo());
            }

        }
        #endregion
        #region Show ninja
        private void ShowNinjaOverview()
        {
            _ninjaOverviewWindow = new NinjaOverviewWindow();
            _ninjaOverviewWindow.Show();
        }
        #endregion

    
        #endregion

    }
}