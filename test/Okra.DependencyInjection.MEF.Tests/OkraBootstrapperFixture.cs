using Microsoft.Extensions.DependencyInjection;
using Okra.Lifetime;
using Okra.Builder;
using MEFExtensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Okra.DependencyInjection;

namespace Okra.DependencyInjection.MEF.Tests
{
    public class OkraBootstrapperFixture
    {
        [Fact]
        public void Initialize_CallsConfigureServicesThenConfigure()
        {
            TestableBootstrapper bootstrapper = new TestableBootstrapper();

            bootstrapper.Initialize();

            Assert.Equal(2, bootstrapper.MethodCalls.Count);
            Assert.Equal("ConfigureServices", bootstrapper.MethodCalls[0].Item1);
            Assert.Equal("Configure", bootstrapper.MethodCalls[1].Item1);
        }

        [Fact]
        public void Initialize_CallsConfigureServices_WithValidServiceCollection()
        {
            TestableBootstrapper bootstrapper = new TestableBootstrapper();

            bootstrapper.Initialize();

            object[] args = bootstrapper.MethodCalls.Where(m => m.Item1 == "ConfigureServices").FirstOrDefault().Item2;
            IServiceCollection serviceCollection = args[0] as IServiceCollection;

            Assert.NotNull(serviceCollection);
        }

        [Fact]
        public void Initialize_CallsConfigure_WithOkraAppBuilder()
        {
            TestableBootstrapper bootstrapper = new TestableBootstrapper();

            bootstrapper.Initialize();

            object[] args = bootstrapper.MethodCalls.Where(m => m.Item1 == "Configure").FirstOrDefault().Item2;
            IOkraAppBuilder appBuilder = args[0] as IOkraAppBuilder;

            Assert.NotNull(appBuilder);
            Assert.Equal(bootstrapper.OkraAppBuilder, appBuilder);
        }

        // *** Test Classes ***

        private class TestableBootstrapper : OkraBootstrapper
        {
            public List<Tuple<string, object[]>> MethodCalls = new List<Tuple<string, object[]>>();
            public MockOkraAppBuilder OkraAppBuilder = new MockOkraAppBuilder();

            protected override void Configure(IOkraAppBuilder app)
            {
                MethodCalls.Add(Tuple.Create("Configure", new object[] { app }));
            }

            protected override void ConfigureServices(IServiceCollection services)
            {
                services.AddSingleton<IOkraAppBuilder>(OkraAppBuilder);
                
                MethodCalls.Add(Tuple.Create("ConfigureServices", new object[] { services }));
            }
        }
        
        private class MockOkraAppBuilder : IOkraAppBuilder
        {
            public IServiceProvider ApplicationServices
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
            public AppLaunchDelegate Build()
            {
                throw new NotImplementedException();
            }
            public IOkraAppBuilder Use(Func<AppLaunchDelegate, AppLaunchDelegate> middleware)
            {
                throw new NotImplementedException();
            }
        }
    }
}
