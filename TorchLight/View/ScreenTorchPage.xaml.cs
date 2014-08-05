using System.IO;
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using TorchLight.Resources;
using ViewModelApi;

namespace TorchLight
{
    public partial class ScreenTorchPage : PhoneApplicationPage
    {
        private readonly SoundEffect _effect;

        public ScreenTorchPage()
        {
            InitializeComponent();

            BuildLocalizedApplicationBar();

            const string soundFile = "Assets/Sounds/click.wav";
            Stream stream = TitleContainer.OpenStream(soundFile);

            _effect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
        }

        private void BuildLocalizedApplicationBar()
        {

            if (!((TorchLightViewModel)DataContext).IsTorchModeEnabled()) return;

            ApplicationBar = new ApplicationBar();
            ApplicationBar.Opacity = 0.5;

            if (!((TorchLightViewModel) DataContext).IsTorchModeAvailable)
            {
                ApplicationBarIconButton changeTorchModeButton =
                    new ApplicationBarIconButton(new Uri("/Assets/Icons/switch.png", UriKind.Relative));
                changeTorchModeButton.Text = AppResources.BacklightFlashLightLabel;
                changeTorchModeButton.Click += SwitchToFlashLightMode;
                ApplicationBar.Buttons.Add(changeTorchModeButton);
            }

            ApplicationBarMenuItem settings = new ApplicationBarMenuItem();
            settings.Text = AppResources.SettingsLabel;
            settings.Click += SettingsSelected;

            ApplicationBar.MenuItems.Add(settings);
        }

        private void SwitchToFlashLightMode(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/MainPage.xaml", UriKind.Relative));
        }

        private void SettingsSelected(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SettingsPage.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationService.RemoveBackEntry();

            ((TorchLightViewModel)DataContext).TorchLightMode = TorchLightMode.Screen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _effect.Play();
        }

        private void MainPageLoadedHandler(object sender, RoutedEventArgs e)
        {
            ((TorchLightViewModel)DataContext).Init();
        }
    }
}