using Constants;
using FlashLightApi;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ViewModelApi
{
    public class TorchLightViewModel:ViewModelBase
    {
        private readonly IFlashLightService _flashLightService;
        private bool _isBusy;

        public TorchLightViewModel(IFlashLightService flashLightService)
        {
            _flashLightService = flashLightService;
            IsBusy = !_flashLightService.IsInitialized;
            _flashLightService.FinishedInitialization += FlashLightServiceOnFinishedInitialization;

            TorchButtonPushed = new RelayCommand(ToggleFlash);
        }

        #region properties

        public RelayCommand TorchButtonPushed { get; set; }

        public TorchLightMode Mode { get; set; }

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

            if (Mode == TorchLightMode.BackLight)
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
