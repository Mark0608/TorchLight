using FlashLightApi;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using TorchLight.Service.FlashLight.Impl;
using ViewModelApi;

namespace TorchLight.IoC
{
    public class Locator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public Locator()
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

            SimpleIoc.Default.Register<IFlashLightService,FlashLightServiceImpl>();
            SimpleIoc.Default.Register<TorchLightViewModel>();

            Init();
        }

        private void Init()
        {
            ServiceLocator.Current.GetInstance<IFlashLightService>().Init();
        }

        public TorchLightViewModel TorchLightViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TorchLightViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

    }
}
