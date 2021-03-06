﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Phone.Media.Capture;
using Constants.Messages;
using FlashLightApi;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Shell;

namespace TorchLight.Service.FlashLight.Impl
{
    internal class FlashLightServiceImpl:IFlashLightService
    {
        private AudioVideoCaptureDevice _avDevice;
        public bool IsFlashOn { get; set; }
        public bool IsInitialized { get; set; }

        public event EventHandler<bool> FinishedInitialization;

        const CameraSensorLocation SensorLocation = CameraSensorLocation.Back;

        public FlashLightServiceImpl()
        {
            //Messenger.Default.Register<AppResumedMessage>(this, message => Init());
            Messenger.Default.Register<AppDeactivatedMessage>(this, message => CleanUp());
        }

        private void CleanUp()
        {
            IsInitialized = false;
        }

        #region Init 
        public async void Init()
        {
            await AwaitableInit();
        }

        public async Task AwaitableInit()
        {
            if (IsInitialized) return;

            _avDevice = await GetCameraDevice();
            IsInitialized = true;

            if (FinishedInitialization != null)
            {
                FinishedInitialization.Invoke(this, IsInitialized);
            }
        }

        private static async Task<AudioVideoCaptureDevice> GetCameraDevice()
        {
            var avDevice = await AudioVideoCaptureDevice.OpenAsync(SensorLocation,
                AudioVideoCaptureDevice.GetAvailableCaptureResolutions(SensorLocation).First());
            return avDevice;
        }
        #endregion

        #region Turn Flash On
        public void TurnFlashOn()
        {
            if (!IsFlashSupported()) return;

            ChangeTorchMode(VideoTorchMode.On);
            SetFlashLightToMaxIntensity();

            IsFlashOn = true;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
        }

        private void SetFlashLightToMaxIntensity()
        {
            _avDevice.SetProperty(KnownCameraAudioVideoProperties.VideoTorchPower,
                AudioVideoCaptureDevice.GetSupportedPropertyRange(SensorLocation,
                    KnownCameraAudioVideoProperties.VideoTorchPower).Max);
        }
        #endregion

        #region Turn Flash Off
        public void TurnFlashOff()
        {
            if (!IsFlashSupported()) return;

            ChangeTorchMode(VideoTorchMode.Off);
            SetFlashLightToMaxIntensity();

            IsFlashOn = false;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
        }
        
        private void ChangeTorchMode(VideoTorchMode newTorchMode)
        {
            _avDevice.SetProperty(KnownCameraAudioVideoProperties.VideoTorchMode, newTorchMode);
        }
        #endregion

        public bool IsFlashSupported()
        {
            var supportedCameraModes = AudioVideoCaptureDevice
                .GetSupportedPropertyValues(SensorLocation, KnownCameraAudioVideoProperties.VideoTorchMode);
            var flashSupported = supportedCameraModes.ToList().Contains((UInt32)VideoTorchMode.On);
            return flashSupported;
        }
    }
}