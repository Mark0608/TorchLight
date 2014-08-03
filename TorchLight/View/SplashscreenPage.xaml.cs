using System;
using System.Windows;
using Microsoft.Phone.Controls;
using ViewModelApi;
using Constants;

namespace TorchLight.View
{
    public partial class SplashScreenPage : PhoneApplicationPage
    {
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
        }
    }
}