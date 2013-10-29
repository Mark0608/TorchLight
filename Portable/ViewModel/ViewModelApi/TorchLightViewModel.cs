﻿using Constants;
using FlashLightApi;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Storage;
using System;

namespace ViewModelApi
{
    public class TorchLightViewModel:ViewModelBase
    {
        private readonly IFlashLightService _flashLightService;
        private readonly IStorageService _storageService;
        private bool _isBusy;
        private TorchLightMode _torchLightMode;

        public TorchLightViewModel(IFlashLightService flashLightService, IStorageService storageService)
        {
            if (flashLightService == null) throw new ArgumentNullException("flashLightService");
            if (storageService == null) throw new ArgumentNullException("storageService");

            _flashLightService = flashLightService;
            _storageService = storageService;

            IsBusy = !_flashLightService.IsInitialized;
            _flashLightService.FinishedInitialization += FlashLightServiceOnFinishedInitialization;

            TorchButtonPushed = new RelayCommand(ToggleFlash);

            _torchLightMode = _storageService.LoadSetting<TorchLightMode>(Consts.TorchLightModeSettingsName);
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

        public string SwitchLabel
        {
            get { return _flashLightService.IsFlashOn ? "Turn torch off":"Turn torch on"; }
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

        #endregion

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

        public bool IsTorchModeEnabled()
        {
            return _flashLightService.IsFlashSupported();
        }
    }
}
