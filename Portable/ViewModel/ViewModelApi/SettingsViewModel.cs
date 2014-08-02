using Constants;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelApi
{
    public class SettingsViewModel
    {
        private readonly IStorageService _storageService;
        private bool _isSwitchedOnAfterLaunchEnabled;

        public SettingsViewModel(IStorageService storageService)
        {
            if (storageService == null) throw new ArgumentNullException("storageService");

            _storageService = storageService;
        }

        public bool IsBackgroundExecutionEnabled
        {
            get { return _storageService.LoadSetting<bool>(Consts.BackgroundExecutionSettingsLabel); }
            set 
            {
                if (value == _storageService.LoadSetting<bool>(Consts.BackgroundExecutionSettingsLabel)) return;

                _storageService.StoreSetting<bool>(Consts.BackgroundExecutionSettingsLabel, value);
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
    }
}
