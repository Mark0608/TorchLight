using FlashLightApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Storage;
using System;
using ViewModelApi;

namespace ViewModelTest
{
    [TestClass]
    public class SettingsViewModelTest
    {
        private Mock<IStorageService> _settingsServiceMock;
        private SettingsViewModel _settingsViewModel;

	[TestInitialize]
        public void Init()
        {
            _settingsServiceMock = new Mock<IStorageService>();
            _settingsServiceMock.Setup(mock => mock.LoadSetting<bool>(It.IsAny<string>())).Returns(true);

            _settingsViewModel = new SettingsViewModel(_settingsServiceMock.Object);
        }

        [TestMethod]
        public void IsBackgroundExecutionEnabled_WhenCalled_ReturnsTheValueFromTheLocalStorage()
        {
            Assert.IsTrue(_settingsViewModel.IsBackgroundExecutionEnabled);
        }

        [TestMethod]
        public void IsBackgroundExecutionEnabled_WhenSetWithANewValue_ItStoresItToTheStorageService()
        {

            _settingsViewModel.IsBackgroundExecutionEnabled = false;

	    _settingsServiceMock.Verify(m => m.StoreSetting(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
