using System;
using System.Windows;
using ApplicationInitializerApi;
using Constants;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Storage;
using TorchLight.Resources;

namespace TorchLight.Service.ApplicationInitializer.Impl
{
    class ApplicationInitializerService:IApplicationInitializerService
    {
        private readonly IStorageService _storageService;

        public ApplicationInitializerService(IStorageService storageService)
        {
            if (storageService == null) throw new ArgumentNullException("storageService");

            _storageService = storageService;
            _storageService.RegisterForStorageChange(Consts.BackgroundExecutionEnabled, UpdateRunInBackground);
        }

        public void Init()
        {
            if (EnsureRunningInBackgroundSettingNeedsToBeSet()) return;

            var runUnderLockscreenMessage = new CustomMessageBox
            {
                Caption = AppResources.BackgroundExecutionMessageBoxCaption,
                Message = AppResources.BackgroundExecutionMessageBoxMessage,
                LeftButtonContent = AppResources.BackgroundExecutionMessageBoxLeftButton,
                RightButtonContent = AppResources.BackgroundExecutionMessageBoxRightButton,
            };

            runUnderLockscreenMessage.Dismissed += RunUnderLockscreenMessageOnDismissed;

            runUnderLockscreenMessage.Show();
        }

        private bool EnsureRunningInBackgroundSettingNeedsToBeSet()
        {
            if (_storageService.HasSetting(Consts.BackgroundExecutionEnabled)) return true;


            if (_storageService.HasSetting(Consts.PhoneHasFlashLight) &&
                !_storageService.LoadSetting<bool>(Consts.PhoneHasFlashLight))
            {
                _storageService.StoreSetting<bool>(Consts.BackgroundExecutionEnabled, false);
                _storageService.StoreSetting<bool>(Consts.FirstStartup, false);
                return true;
            }
            return false;
        }

        private void RunUnderLockscreenMessageOnDismissed(object sender, DismissedEventArgs dismissedEventArgs)
        {
            switch (dismissedEventArgs.Result)
            {
                case CustomMessageBoxResult.RightButton:
                    _storageService.StoreSetting<bool>(Consts.BackgroundExecutionEnabled, true);
                    break;
                case CustomMessageBoxResult.LeftButton:
                    _storageService.StoreSetting<bool>(Consts.BackgroundExecutionEnabled, false);
                    break;
                case CustomMessageBoxResult.None:
                    _storageService.StoreSetting<bool>(Consts.BackgroundExecutionEnabled, false);
                    break;
            }

            _storageService.StoreSetting<bool>(Consts.FirstStartup, false);
        }

        private void UpdateRunInBackground()
        {
            try
            {
                if (_storageService.LoadSetting<bool>(Consts.BackgroundExecutionEnabled))
                {
                    PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                }
                else
                {
                    PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Enabled;
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(AppResources.NeedToRestartApplication);
            }
        }

    }
}
