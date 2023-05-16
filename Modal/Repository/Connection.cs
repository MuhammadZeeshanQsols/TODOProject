using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;
using TODOProject.Modal.Interfaces;

namespace TODOProject.Modal.Repository
{
    public class Connection : IConnection
    {
        public Database Dev_GetDataBase()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                Database database;
                if (!container.IsRegistered<Database>())
                {
                    IConfigurationSource source = ConfigurationSourceFactory.Create();
                    DatabaseProviderFactory factory = new DatabaseProviderFactory(source);
                    database = factory.Create(EnvironmentSettings.Dev_Connection());
                    container.RegisterType<Database>(new InjectionFactory(c => database));
                }
                else
                {
                    database = container.Resolve<Database>();
                }
                return database;
            }
        }

        public Database QA_GetDatabase()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                Database database;
                if (!container.IsRegistered<Database>())
                {
                    IConfigurationSource source = ConfigurationSourceFactory.Create();
                    DatabaseProviderFactory factory = new DatabaseProviderFactory(source);
                    database = factory.Create(EnvironmentSettings.QA_Connection());
                    container.RegisterType<Database>(new InjectionFactory(c => database));
                }
                else
                {
                    database = container.Resolve<Database>();
                }
                return database;
            }
        }
        public Database Stagging_GetDataBase()
        {
            using (IUnityContainer container = new UnityContainer())
            {
                Database database;
                if (!container.IsRegistered<Database>())
                {
                    IConfigurationSource source = ConfigurationSourceFactory.Create();
                    DatabaseProviderFactory factory = new DatabaseProviderFactory(source);
                    database = factory.Create(EnvironmentSettings.Stagging_Connection());
                    container.RegisterType<Database>(new InjectionFactory(c => database));
                }
                else
                {
                    database = container.Resolve<Database>();
                }
                return database;
            }
        }

       
    }
}