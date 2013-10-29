﻿using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DependencyInjectorApi;
using FlashLightApi;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using TorchLight.Service.FlashLight.Impl;
using ViewModelApi;
using TorchLight.Service.Storage;
using Storage;

namespace TorchLight.IoC
{
    public class Locator:ILocator
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
            SimpleIoc.Default.Register<ILocator>(() => this);
            SimpleIoc.Default.Register<SplashScreenViewModel>();
            SimpleIoc.Default.Register<TorchLightViewModel>();
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

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

    }
}
