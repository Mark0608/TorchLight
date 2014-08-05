using ApplicationInitializerApi;
using Constants;
using Constants.Messages;
using FlashLightApi;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Storage;
using System;

namespace ViewModelApi
{
    public class TorchLightViewModel:ViewModelBase
    {
        private readonly IFlashLightService _flashLightService;
        private readonly IStorageService _storageService;
        private readonly IApplicationInitializerService _applicationInitializerService;
        private bool _isBusy;
        private TorchLightMode _torchLightMode;
        private bool _isFirstInit;

        public TorchLightViewModel(IFlashLightService flashLightService, IStorageService storageService, IApplicationInitializerService applicationInitializerService)
        {
            if (flashLightService == null) throw new ArgumentNullException("flashLightService");
            if (storageService == null) throw new ArgumentNullException("storageService");
            if (applicationInitializerService == null) throw new ArgumentNullException("applicationInitializerService");

            _flashLightService = flashLightService;
            _storageService = storageService;
            _applicationInitializerService = applicationInitializerService;

            _flashLightService.FinishedInitialization += FlashLightServiceOnFinishedInitialization;
            TorchButtonPushed = new RelayCommand(ToggleFlash);

            IsBusy = !_flashLightService.IsInitialized;

            _torchLightMode = _storageService.LoadSetting<TorchLightMode>(Consts.TorchLightModeSettingsName);

            _isFirstInit = true;

            Messenger.Default.Register<AppResumedMessage>(this, message => Init());
            Messenger.Default.Register<AppDeactivatedMessage>(this, message => _flashLightService.TurnFlashOff());
        }

        #region properties

        public RelayCommand TorchButtonPushed { get; set; }

        public TorchLightMode TorchLightMode
        {
            get { return _torchLightMode; }
            set
            {
                _torchLightMode = value;
                _storageService.StoreSetting(Consts.TorchLightModeSettingsName, _torchLightMode);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                _isBusy = value;
                RaisePropertyChanged("");
            }
        }

        public bool IsTorchOn
        {
            get { return _flashLightService.IsFlashOn; }
        }

        public bool LightScreen { get; set; }
        public bool IsTorchModeAvailable { get { return _storageService.LoadSetting<bool>(Consts.PhoneHasFlashLight); }}

        #endregion

        public bool IsTorchModeEnabled()
        {
            return _flashLightService.IsFlashSupported();
        }

        public void Init()
        {
            if (!_storageService.HasSetting(Consts.FirstStartup))
            {
                _applicationInitializerService.Init();
            }

            if (_storageService.LoadSetting<bool>(Consts.TurnOnTorchAfterStartup) && _isFirstInit)
            {
                _isFirstInit = false;
                ToggleFlash();
            }
        }

        private void FlashLightServiceOnFinishedInitialization(object sender, bool b)
        {
            IsBusy = !_flashLightService.IsInitialized;
        }

        private void ToggleFlash()
        {
            IsBusy = true;

            if (TorchLightMode == TorchLightMode.BackLight)
            {
                ToggleBacklight();
            }
            else
            {
                ToggleScreen();
            }
            IsBusy = false;
        }

        private void ToggleScreen()
        {
            LightScreen = !LightScreen;
        }

        private void ToggleBacklight()
        {
            if (_flashLightService.IsFlashOn)
            {
                _flashLightService.TurnFlashOff();
            }
            else
            {
                _flashLightService.TurnFlashOn();
            }
        }
    }
}
