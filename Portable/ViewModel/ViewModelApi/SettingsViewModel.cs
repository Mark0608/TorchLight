using Constants;
using Storage;
using System;

namespace ViewModelApi
{
    public class SettingsViewModel
    {
        private readonly IStorageService _storageService;

        public SettingsViewModel(IStorageService storageService)
        {
            if (storageService == null) throw new ArgumentNullException("storageService");

            _storageService = storageService;
        }

        public bool IsBackgroundExecutionEnabled
        {
            get { return _storageService.LoadSetting<bool>(Consts.BackgroundExecutionEnabled); }
            set 
            {
                if (value == _storageService.LoadSetting<bool>(Consts.BackgroundExecutionEnabled)) return;

                _storageService.StoreSetting<bool>(Consts.BackgroundExecutionEnabled, value);
            }
        }

        public bool IsSwitchedOnAfterLaunchEnabled
        {
            get { return _storageService.LoadSetting<bool>(Consts.TurnOnTorchAfterStartup); }
            set
            {
                if (value == _storageService.LoadSetting<bool>(Consts.TurnOnTorchAfterStartup)) return;

                _storageService.StoreSetting<bool>(Consts.TurnOnTorchAfterStartup, value);
            }
        }

        public bool IsTorchModeAvailable { get { return _storageService.LoadSetting<bool>(Consts.PhoneHasFlashLight); }}
    }
}
