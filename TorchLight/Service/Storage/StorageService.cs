using Storage;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorchLight.Service.Storage
{
    class StorageService:IStorageService
    {
        public void StoreSetting<T>(string settingName, T torchLightMode)
        {
            IsolatedStorageSettings.ApplicationSettings[settingName] = torchLightMode;
        }

        public T LoadSetting<T>(string settingName)
        {
	    if(!IsolatedStorageSettings.ApplicationSettings.Contains(settingName)) return default(T);

            return (T) IsolatedStorageSettings.ApplicationSettings[settingName];
        }
    }
}
