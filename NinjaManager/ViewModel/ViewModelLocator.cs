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

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using NinjaManager.ViewModel.EquipmentVMs;
using NinjaManager.ViewModel.NinjaVMs;

namespace NinjaManager.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ManageNinjasViewModel>();
            SimpleIoc.Default.Register<ManageEquipmentViewModel>();

        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public ManageNinjasViewModel ManageNinjas
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ManageNinjasViewModel>();
            }
        }

        public ManageEquipmentViewModel ManageEquipment 
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<ManageEquipmentViewModel>();
            }
        }
        public EditNinjaViewModel EditNinja
        {
            get
            {
                return new EditNinjaViewModel(ManageNinjas);
            }
        }

        public AddNinjaViewModel AddNinja
        {
            get
            {
                return new AddNinjaViewModel(ManageNinjas);
            }
        }

        public NinjaOverviewViewModel NinjaOverview
        {
            get
            {
                return new NinjaOverviewViewModel(ManageNinjas);
            }
        }

        public AddEquipmentViewModel AddEquipment
        {
            get
            {
                return new AddEquipmentViewModel(ManageEquipment);
            }
        }

        public EditEquipmentViewModel EditEquipment
        {
            get
            {
                return new EditEquipmentViewModel(ManageEquipment);
            }
        }

        public ShopViewModel Shop
        {
            get
            {
                return new ShopViewModel(ManageNinjas, ManageEquipment);
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}