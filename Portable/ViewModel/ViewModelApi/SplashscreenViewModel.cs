using System;
using System.Threading.Tasks;
using DependencyInjectorApi;
using FlashLightApi;
using JetBrains.Annotations;

namespace ViewModelApi
{
    public class SplashScreenViewModel
    {
        private readonly IFlashLightService _flashLightService;
        public Action InitDoneCallback { get; set; }

        public SplashScreenViewModel(IFlashLightService flashLightService)
        {
            if (flashLightService == null) throw new ArgumentNullException("flashLightService");
            _flashLightService = flashLightService;

            // Ensure that the callback is always set
            InitDoneCallback = () => { };
        }

        public async void Init()
        {
            await _flashLightService.AwaitableInit();
            InitDoneCallback();
        }
    }
}