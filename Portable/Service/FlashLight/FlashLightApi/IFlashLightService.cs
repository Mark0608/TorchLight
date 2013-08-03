using System;
using System.Threading.Tasks;

namespace FlashLightApi
{
    public interface IFlashLightService
    {
        bool IsFlashOn { get; set; }
        bool IsInitialized { get; set; }

        event EventHandler<bool> FinishedInitialization;

        void Init();

        Task TurnFlashOn();
        Task TurnFlashOff();
    }
}
