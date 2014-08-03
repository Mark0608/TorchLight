using System.Threading.Tasks;
using FlashLightApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewModelApi;
using Storage;

namespace ViewModelTest
{
    [TestClass]
    public class SplashScreenViewModelTest
    {
        [TestMethod]
        public void Init_WhenCalled_ItCallsAndAwaitsTheInitMethodOfTheLocator()
        {
            var storageMock = new Mock<IStorageService>();
            var flashLightServiceMock = new Mock<IFlashLightService>();
            flashLightServiceMock.Setup(l => l.AwaitableInit()).Returns(Task.Delay(0));
            var splashscreenViewModel = new SplashScreenViewModel(flashLightServiceMock.Object, storageMock.Object);

            splashscreenViewModel.Init();

            flashLightServiceMock.Verify(locator => locator.AwaitableInit(), Times.Once());
        }
    }
}
