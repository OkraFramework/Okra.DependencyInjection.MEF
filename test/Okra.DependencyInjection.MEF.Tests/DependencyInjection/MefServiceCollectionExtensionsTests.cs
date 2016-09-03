using Okra.DependencyInjection.MEF.Tests.Mocks;
using Xunit;

namespace Okra.DependencyInjection.MEF.Tests
{
    public class MefServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddMefRouting_ReturnsServiceCollection()
        {
            var serviceCollection = new MockServiceCollection();

            var result = serviceCollection.AddMefRouting();

            Assert.Equal(serviceCollection, result);
        }

        [Fact]
        public void AddMefRouting_AddsServices()
        {
            var serviceCollection = new MockServiceCollection();

            serviceCollection.AddMefRouting();

            serviceCollection.AssertContainsScoped<IViewFactory, ViewFactory>();
            serviceCollection.AssertContainsScoped<IViewModelFactory, ViewModelFactory>();
        }
    }
}