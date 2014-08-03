using System.IO;
using System.Windows;
using Constants;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Navigation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using TorchLight.Resources;
using ViewModelApi;

namespace TorchLight.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly SoundEffect _effect;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();

            const string soundFile = "Assets/Sounds/click.wav";
            Stream stream = TitleContainer.OpenStream(soundFile);

            _effect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Opacity = 0.5;

            ApplicationBarIconButton changeTorchModeButton = new ApplicationBarIconButton(new Uri("/Assets/Icons/switch.png", UriKind.Relative));
            changeTorchModeButton.Text = AppResources.ScreenFlashLightLabel;
            changeTorchModeButton.Click += SwitchToFlashLightMode;

            ApplicationBarMenuItem settings = new ApplicationBarMenuItem();
            settings.Text = AppResources.SettingsLabel;
            settings.Click += SettingsSelected;

            ApplicationBar.Buttons.Add(changeTorchModeButton);
            ApplicationBar.MenuItems.Add(settings);
        }

        private void SettingsSelected(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SettingsPage.xaml", UriKind.Relative));
        }

        private void SwitchToFlashLightMode(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/ScreenTorchPage.xaml", UriKind.Relative));
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationService.RemoveBackEntry();

            ((TorchLightViewModel)DataContext).TorchLightMode = TorchLightMode.BackLight;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _effect.Play();
        }

        private void MainPageLoadedHandler(object sender, RoutedEventArgs e)
        {
            ((TorchLightViewModel)DataContext).Init();
        }
    }
}