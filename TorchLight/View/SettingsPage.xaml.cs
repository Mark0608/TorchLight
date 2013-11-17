using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TorchLight.Resources;

namespace TorchLight.View
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            PageTitle.Text = AppResources.SettingsLabel;
            RunInBackgroundSetting.Content = AppResources.EnabledLabel;
            RunInBackgroundSetting.IsEnabledChanged += BackgroundRunSettingsChanged;
        }

        private void BackgroundRunSettingsChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RunInBackgroundSetting.Content = RunInBackgroundSetting.IsEnabled ? AppResources.EnabledLabel : AppResources.DisabledLabel;
        }
    }
}