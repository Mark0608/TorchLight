using Constants;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchLight.Service.Storage;

namespace TorchLightTest.Service.Storage
{
    [TestClass]
    public class StorageServiceTest
    {
        private int _testCallbackMethodInvokedCount;
        private StorageService _storageService;

	[TestInitialize]
        public void Init()
        {
            _testCallbackMethodInvokedCount = 0;

             _storageService = new StorageService();

            _storageService.RegisterForStorageChange(Consts.BackgroundExecutionEnabled, TestCallbackMethod);
        }

        [TestMethod]
        public void RegisterForStorageChange_WhenRegisteredAndTheSettingChanges_TheCallbackFunctionWillBeCalledOnce()
        {
            _storageService.StoreSetting<bool>(Consts.BackgroundExecutionEnabled, true);

            Assert.AreEqual(1, _testCallbackMethodInvokedCount);
        }

        [TestMethod]
        public void RegisterForStorageChange_WhenUnregisteredAndTheSettingChanges_TheCallbackFunctionWillNeverBeCalled()
        {
            _storageService.UnregisterForStorageChange(Consts.BackgroundExecutionEnabled, TestCallbackMethod);
            _storageService.StoreSetting<bool>(Consts.BackgroundExecutionEnabled, true);

            Assert.AreEqual(0, _testCallbackMethodInvokedCount);
        }


        private void TestCallbackMethod()
        {
            _testCallbackMethodInvokedCount += 1;
        }
    }
}
