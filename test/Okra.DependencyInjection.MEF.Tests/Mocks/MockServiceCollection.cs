using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Okra.DependencyInjection.MEF.Tests.Mocks
{
    public class MockServiceCollection : List<ServiceDescriptor>, IServiceCollection
    {
        public void AssertContainsScoped<TService, TImplementation>()
        {
            var descriptor = this.FirstOrDefault(d => d.ServiceType == typeof(TService));

            if (descriptor == null)
                Assert.True(false, $"Service '{typeof(TService)}' is not registered in the collection");

            Assert.Equal(typeof(TImplementation), descriptor.ImplementationType);
            Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime);
        }
    }
}
