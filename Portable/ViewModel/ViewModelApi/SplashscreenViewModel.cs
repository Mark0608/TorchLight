using System;
using FlashLightApi;
using Constants;
using Storage;

namespace ViewModelApi
{
    public class SplashScreenViewModel
    {
        private readonly IFlashLightService _flashLightService;
        private readonly IStorageService _storageService;

        public SplashScreenViewModel(IFlashLightService flashLightService, IStorageService storageService)
        {
            if (flashLightService == null) throw new ArgumentNullException("flashLightService");
            if (storageService == null) throw new ArgumentNullException("storageService");

            _flashLightService = flashLightService;
            _storageService = storageService;

            // Ensure that the callback is always set
            InitDoneCallback = () => { };
        }

        public Action InitDoneCallback { get; set; }

        public async void Init()
        {
            await _flashLightService.AwaitableInit();

            if (!_storageService.HasSetting(Consts.TurnOnTorchAfterStartup))
            {
                _storageService.StoreSetting<bool>(Consts.TurnOnTorchAfterStartup, false);
            }

            InitDoneCallback();
        }

        public TorchLightMode GetCurrentTorchLightMode()
        {
            if (!_flashLightService.IsFlashSupported()) return TorchLightMode.Screen;

            return _storageService.LoadSetting<TorchLightMode>(Consts.TorchLightModeSettingsName);
        }
    }
}