using Constants;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Navigation;
using TorchLight.Resources;
using ViewModelApi;

namespace TorchLight.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
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
            ClickSound.Play();
        }
    }
}