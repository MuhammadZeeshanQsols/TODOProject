using System.Configuration;

namespace TODOProject.Modal
{
    public static class ConfigurationSetting
    {
        public static ConnectionStringSettings SetConfiguration(string ConnectionName) => new ConnectionStringSettings(ConnectionName, ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString);
    }
}