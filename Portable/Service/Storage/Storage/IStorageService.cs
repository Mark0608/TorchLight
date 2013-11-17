using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage
{
    public interface IStorageService
    {
        void StoreSetting<T>(string settingName, T value);
        T LoadSetting<T>(string settingName);

        bool HasSetting(string p);

	void RegisterForStorageChange(string settingName, Action callback);
	void UnregisterForStorageChange(string settingName, Action callback);
    }
}
