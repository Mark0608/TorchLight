using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ApplicationInitializerApi;
using FlashLightApi;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using TorchLight.Service.ApplicationInitializer.Impl;
using TorchLight.Service.FlashLight.Impl;
using ViewModelApi;
using TorchLight.Service.Storage;
using Storage;
using System;

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
            SimpleIoc.Default.Register<IStorageService,StorageService>();
            SimpleIoc.Default.Register<IApplicationInitializerService,ApplicationInitializerService>();
            //SimpleIoc.Default.Register<ILocator>(() => this);
            SimpleIoc.Default.Register<SplashScreenViewModel>();
            SimpleIoc.Default.Register<TorchLightViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        //public async Task Init()
        //{
        //    await ServiceLocator.Current.GetInstance<IFlashLightService>().AwaitableInit();
        //}

        public TorchLightViewModel TorchLightViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TorchLightViewModel>();
            }
        }

        public SplashScreenViewModel SplashScreenViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SplashScreenViewModel>();
            }
        }

        public SettingsViewModel SettingsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

    }
}
