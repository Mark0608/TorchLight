using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storage
{
    public interface IStorageService
    {
        void StoreSetting<T>(string settingName, T torchLightMode);
        T LoadSetting<T>(string settingName);
    }
}
