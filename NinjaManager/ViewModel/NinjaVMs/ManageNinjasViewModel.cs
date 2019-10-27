using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel.NinjaVMs
{
    public class ManageNinjasViewModel: ViewModelBase
    {
        public ObservableCollection<NinjaViewModel> Ninjas { get; set; }

        private NinjaViewModel _selectedNinja;

        public NinjaViewModel SelectedNinja
        {
            get { return _selectedNinja; }
            set { _selectedNinja = value; RaisePropertyChanged(); }
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
  
        public ManageNinjasViewModel()
        {
            Ninjas = new ObservableCollection<NinjaViewModel>();
            GetAllNinjas();

            ShowEditNinjaCommand = new RelayCommand(ShowEditNinja);
            ShowAddNinjaCommand = new RelayCommand(ShowAddNinja);
            ShowNinjaOverviewCommand = new RelayCommand(ShowNinjaOverview);
            DeleteNinjaCommand = new RelayCommand(DeleteNinja);

        }


        private void GetAllNinjas()
        {
            using (var context = new NinjaDBEntities())
            {
                context.Ninjas.ToList().ForEach(n => Ninjas.Add(new NinjaViewModel(n)));
            }
        }

        private void ShowEditNinja()
        {
            _editNinjaWindow = new EditNinjaWindow();
            _editNinjaWindow.Show();
        }

        public void CloseEditNinja()
        {
            _editNinjaWindow.Close();
        }

        private void ShowAddNinja()
        {
            _addNinjaWindow = new AddNinjaWindow();
            _addNinjaWindow.Show();
        }

        public void CloseAddNinja()
        {
            _addNinjaWindow.Close();
        }
  
        private void DeleteNinja()
        {
            if (_ninjaOverviewWindow.Title == SelectedNinja.Name)
            {
                _ninjaOverviewWindow.Close();
            }

            using (var context = new NinjaDBEntities())
            {
                var ninja = context.Ninjas.ToList().Find(n => n.Id == SelectedNinja.Id);
                ninja.Equipments.Clear();
                context.Ninjas.Remove(ninja);
                context.SaveChanges();

                Ninjas.Remove(Ninjas.ToList().Find(n => n.Id == ninja.Id));
            }

          
         
          
        }
     
        private void ShowNinjaOverview()
        {
            _ninjaOverviewWindow = new NinjaOverviewWindow();
            _ninjaOverviewWindow.Show();
        }


    }
}
