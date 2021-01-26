using Autofac;
using Serilog;
using System.Reflection;

namespace DoorManagementClient
{
    public class Bootstrap
    {
        public static IContainer Container;
        public static void Init()
        {
            InitAutofaccontainer();
            InitLogger();

            Serilog.Log.Information("App initialized");
        }

        private static void InitAutofaccontainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DoorManagementRequestHandler>().As<IDoorManagementRequestHandler>();
            builder.RegisterType<AppConfig>().As<IAppConfig>();
            builder.RegisterType<DoorManagementServiceIntegration>().As<IDoorManagementServiceIntegration>();
            Container = builder.Build();
           
        }

        /// <summary>
        /// Initialize logger for the application
        /// </summary>
        private static void InitLogger()
        {
            var path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = System.IO.Path.Combine(path, "logs", "DoorManagemenrtAppLog-{Date}.txt");
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.RollingFile(path)
               .CreateLogger();
        }
    }
}
