using System.Configuration;
using GateManagement.Domain;

namespace DoorManagementClient
{
    public interface IAppConfig
    {
        string GetBaseServiceUrl();
    }

    public class AppConfig : IAppConfig
    {
        public string GetBaseServiceUrl()
        => ConfigurationSettings.AppSettings[Constants.Config.SERVICE_BASE_URI];
        
    }
}
