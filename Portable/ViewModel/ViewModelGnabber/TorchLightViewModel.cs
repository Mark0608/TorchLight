using FlashLightApi;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ViewModel
{
    public class TorchLightViewModel:ViewModelBase
    {
        private readonly IFlashLightService _flashLightService;
        private bool _isBusy;

        public RelayCommand TorchButtonPushed { get; set; }

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

        public TorchLightViewModel(IFlashLightService flashLightService)
        {
            _flashLightService = flashLightService;
            IsBusy = !_flashLightService.IsInitialized;
            _flashLightService.FinishedInitialization += FlashLightServiceOnFinishedInitialization;

            TorchButtonPushed = new RelayCommand(ToggleFlash);
        }

        private void FlashLightServiceOnFinishedInitialization(object sender, bool b)
        {
            IsBusy = !_flashLightService.IsInitialized;
        }

        private async void ToggleFlash()
        {
            IsBusy = true;
            if (_flashLightService.IsFlashOn)
            {
                await _flashLightService.TurnFlashOff();
            }
            else
            {
                await _flashLightService.TurnFlashOn();
            }
            IsBusy = false;
        }
    }
}
