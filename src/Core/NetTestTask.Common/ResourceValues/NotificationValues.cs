using NetTestTask.Common.Constants;
using System.Resources;

namespace NetTestTask.Common.ResourceValues
{
    public static class NotificationValues
    {
        static ResourceManager _resourceManager = null;

        
        public static string DataNotFound => GetValues(NotificationMessageNames.DataNotFound);

        public static string GetValues(string key)
        {
            _resourceManager = new ResourceManager(typeof(Properties.Notifications));

            return _resourceManager.GetString(key);
        }
    }
}
