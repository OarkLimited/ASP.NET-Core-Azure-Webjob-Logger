using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webjob;

namespace Webjob
{
    public class Program
    {
        public static IConfigurationRoot Configuration;
        
        public static void Main()
        {
            Configuration = GetConfiguration();
            

            JobHostConfiguration config = new JobHostConfiguration()
            {
                JobActivator = new MyActivator(),
                NameResolver = new QueueNameResolver(),
                DashboardConnectionString = Configuration["Azure:ConnectionString"],
                StorageConnectionString = Configuration["Azure:ConnectionString"]
            };

            var host = new JobHost(config);

            host.RunAndBlock();
        }

        
        private static IConfigurationRoot GetConfiguration()
        {
            IConfigurationBuilder configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true);

            return configuration.Build();
        }
    }

    public class MyActivator : IJobActivator
    {
        private readonly IServiceProvider _provider;

        public MyActivator()
        {
            var services = new ServiceCollection();
            services.AddTransient<Fuctions, Fuctions>();
           



            _provider = services.BuildServiceProvider();

        }

        public T CreateInstance<T>()
        {
            return _provider.GetRequiredService<T>();
        }

        public IServiceProvider ServiceProvider
        {
            get
            {
                return _provider;
            }
        }
    }


    /// <summary>
    /// http://stackoverflow.com/a/36708310
    /// </summary>
    /// <seealso cref="Microsoft.Azure.WebJobs.INameResolver" />
    public class QueueNameResolver : INameResolver
    {
        public string Resolve(string practiceId)
        {
            //define in appsettings the queuename property
            return Program.Configuration[practiceId] ?? practiceId;
            //or some other service of your design
        }
    }
}
