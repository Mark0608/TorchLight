﻿using System.Threading.Tasks;
using DependencyInjectorApi;
using FlashLightApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModelApi;

namespace ViewModelTest
{
    [TestClass]
    public class SplashScreenViewModelTest
    {
        [TestMethod]
        public async Task Init_WhenCalled_ItCallsAndAwaitsTheInitMethodOfTheLocator()
        {

            var locatorMock = new Mock<IFlashLightService>();
            locatorMock.Setup(l => l.AwaitableInit()).Returns(Task.Delay(0));
            var splashscreenViewModel = new SplashScreenViewModel(locatorMock.Object);

            splashscreenViewModel.Init();

            locatorMock.Verify(locator => locator.Init(), Times.Once());
        }
    }
}