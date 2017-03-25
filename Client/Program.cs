using Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Extensions.Configuration;

namespace Client
{
    public class Program
    {
        private static  IServiceProvider _provider;
        public static IConfigurationRoot Configuration;


        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<ICustomErrorLogger, CustomErrorLogger>();
            services.AddScoped<TestClass, TestClass>();
            services.AddLogging();
            _provider = services.BuildServiceProvider();


            Configuration = GetConfiguration();
            


            ILoggerFactory loggerFactory = _provider.GetRequiredService<ILoggerFactory>();
            ICustomErrorLogger customErrorLogger = _provider.GetRequiredService<ICustomErrorLogger>();


            loggerFactory.AddWebJob(customErrorLogger, Microsoft.Extensions.Logging.LogLevel.Error);





            TestClass tc = _provider.GetRequiredService<TestClass>();
            tc.LogEvent("Helllo there").Wait();

            Console.ReadLine();
        }

        private static IConfigurationRoot GetConfiguration()
        {
            IConfigurationBuilder configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true);

            return configuration.Build();
        }
    }


    public class TestClass
    {


        private ILogger<TestClass> _logger;

        public TestClass(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TestClass>();
        }

        public async Task LogEvent(string message)
        {
            _logger.LogError(message);
        }
    }
}
