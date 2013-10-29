using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ViewModelApi;
using Storage;
using Constants;

namespace TorchLight.View
{
    public partial class SplashScreenPage : PhoneApplicationPage
    {
        private readonly IStorageService _storageService;
        public SplashScreenPage()
        {
            InitializeComponent();
        }

        private void OnSplashScreenLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = ((SplashScreenViewModel) DataContext);

            viewModel.InitDoneCallback = InitDoneCallback;

            viewModel.Init();
        }

        private void InitDoneCallback()
        {
            var currentFlashLightMode = ((SplashScreenViewModel) DataContext).GetCurrentTorchLightMode();

            if (currentFlashLightMode == TorchLightMode.BackLight)
            {
                NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/View/ScreenTorchPage.xaml", UriKind.Relative));
            }

            //NavigationService.RemoveBackEntry();
        }
    }
}