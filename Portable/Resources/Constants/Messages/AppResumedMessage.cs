using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Constants.Messages
{
    public class AppResumedMessage
    {
        private static AppResumedMessage _getInstance;

        private AppResumedMessage() { }

        public static AppResumedMessage GetInstance
        {
            get { return _getInstance ?? (_getInstance = new AppResumedMessage()); }
        }
    }
}
