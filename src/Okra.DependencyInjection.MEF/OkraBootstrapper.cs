using Microsoft.Extensions.DependencyInjection;
using Okra.Lifetime;
using Okra.Builder;
using MEFExtensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Okra.DependencyInjection;

namespace Okra.DependencyInjection.MEF
{
    public abstract class OkraBootstrapper : IOkraBootstrapper
    {
        // *** Methods ***

        public void Initialize()
        {
            // Initialize MEF and compose the bootstrapper

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();


            // Get the IOkraAppBuilder and configure the application

            IOkraAppBuilder appBuilder = serviceProvider.GetRequiredService<IOkraAppBuilder>();
            Configure(appBuilder);
        }

        // *** Protected Methods ***

        protected virtual void Configure(IOkraAppBuilder app)
        {
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
