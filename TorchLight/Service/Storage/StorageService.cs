using Storage;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorchLight.Service.Storage
{
    internal class StorageService:IStorageService
    {
        private readonly Dictionary<string, List<Action>> _storageChangeCallbacks;

        public StorageService()
        {
            _storageChangeCallbacks = new Dictionary<string, List<Action>>();
        }

        #region IStorageService Members

        public void StoreSetting<T>(string settingName, T settingsParameter)
        {
            IsolatedStorageSettings.ApplicationSettings[settingName] = settingsParameter;
            InvokeObservers(settingName);
        }

        private void InvokeObservers(string settingName)
        {
            if (!_storageChangeCallbacks.ContainsKey(settingName)) return;

            foreach (var observer in _storageChangeCallbacks[settingName])
            {
                observer.Invoke();
            }
        }

        public T LoadSetting<T>(string settingName)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(settingName)) return default(T);

            return (T) IsolatedStorageSettings.ApplicationSettings[settingName];
        }

        public bool HasSetting(string settingName)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(settingName);
        }

        public void RegisterForStorageChange(string settingName, Action callback)
        {
            if (!_storageChangeCallbacks.ContainsKey(settingName))
            {
                _storageChangeCallbacks.Add(settingName, new List<Action> { callback });
		return;
            }

            _storageChangeCallbacks[settingName].Add(callback);
        }

        public void UnregisterForStorageChange(string settingName, Action callback)
        {
            _storageChangeCallbacks[settingName].Remove(callback);
        }

        #endregion
    }
}
