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

namespace TorchLight.View
{
    public partial class SplashScreenPage : PhoneApplicationPage
    {
        public SplashScreenPage()
        {
            InitializeComponent();
        }

        private async void OnSplashScreenLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = ((SplashScreenViewModel) DataContext);

            viewModel.InitDoneCallback = InitDoneCallback;

            viewModel.Init();
        }

        private void InitDoneCallback()
        {
            NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));
            NavigationService.RemoveBackEntry();
        }
    }
}