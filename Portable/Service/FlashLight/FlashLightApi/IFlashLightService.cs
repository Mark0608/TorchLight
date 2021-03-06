﻿using System;
using System.Threading.Tasks;

namespace FlashLightApi
{
    public interface IFlashLightService
    {
        bool IsFlashOn { get; set; }
        bool IsInitialized { get; set; }

        event EventHandler<bool> FinishedInitialization;

        Task AwaitableInit();
        void Init();


        bool IsFlashSupported();
        void TurnFlashOn();
        void TurnFlashOff();
    }
}
