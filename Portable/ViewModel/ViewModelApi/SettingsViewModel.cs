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
        private IStorageService _storageService;
	private bool _isBackgroundExecutionEnabled;

        public SettingsViewModel(IStorageService storageService)
        {
            if (storageService == null) throw new ArgumentNullException("storageService");

            _storageService = storageService;
            _isBackgroundExecutionEnabled = _storageService.LoadSetting<bool>(Consts.BackgroundExecutionSettingsLabel);
        }

        public bool IsBackgroundExecutionEnabled
        {
            get { return _isBackgroundExecutionEnabled; }
            set 
            {
                if (value == _isBackgroundExecutionEnabled) return;

                _isBackgroundExecutionEnabled = value;

                _storageService.StoreSetting<bool>(Consts.BackgroundExecutionSettingsLabel, value);
            }
        }
    }
}
