using Constants;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TorchLight.Resources;
using ViewModelApi;

namespace TorchLight
{
    public partial class ScreenTorchPage : PhoneApplicationPage
    {
        public ScreenTorchPage()
        {
            InitializeComponent();

            BuildLocalizedApplicationBar();
        }

        private void BuildLocalizedApplicationBar()
        {

            if (!((TorchLightViewModel)DataContext).IsTorchModeEnabled()) return;

            ApplicationBar = new ApplicationBar();
            ApplicationBar.Opacity = 0.5;

            ApplicationBarIconButton changeTorchModeButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/switch.png", UriKind.Relative));

            changeTorchModeButton.Text = AppResources.BacklightFlashLightLabel;
            changeTorchModeButton.Click += SwitchToFlashLightMode;

            ApplicationBar.Buttons.Add(changeTorchModeButton);
        }

        private void SwitchToFlashLightMode(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationService.RemoveBackEntry();

            ((TorchLightViewModel)DataContext).TorchLightMode = TorchLightMode.Screen;
        }
    }
}