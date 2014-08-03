namespace Constants.Messages
{
    public class AppDeactivatedMessage
    {
        private static AppDeactivatedMessage _getInstance;

        private AppDeactivatedMessage() { }

        public static AppDeactivatedMessage GetInstance
        {
            get { return _getInstance ?? (_getInstance = new AppDeactivatedMessage()); }
        }
    }
}