using System;
using System.Threading.Tasks;
using DependencyInjectorApi;

namespace ViewModelApi
{
    public class SplashScreenViewModel
    {
        private readonly ILocator _locator;

        public Action InitDoneCallback { get; set; }

        public SplashScreenViewModel(ILocator locator)
        {
            if (locator == null) throw new ArgumentNullException("locator");

            _locator = locator;

            // Ensure that the callback is always set
            InitDoneCallback = () => { };
        }

        public async void Init()
        {
            await _locator.Init();
            InitDoneCallback();
        }
    }
}