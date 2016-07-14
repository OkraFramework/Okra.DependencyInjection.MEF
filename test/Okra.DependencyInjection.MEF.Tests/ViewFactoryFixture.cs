using System.Composition.Hosting;
using Xunit;

namespace Okra.DependencyInjection.MEF.Tests
{
    public class ViewFactoryFixture
    {
        [Fact]
        public void TryCreateView_ReturnsViewIfAvailable()
        {
            var viewFactory = CreateViewFactory();
            
            object view;
            var success = viewFactory.TryCreateView("TestPage", out view);

            Assert.True(success);
            Assert.NotNull(view);
            Assert.IsType<TestView>(view);
        }

        [Fact]
        public void TryCreateView_ReturnsFalseIfNotAvailable()
        {
            var viewFactory = CreateViewFactory();
            
            object view;
            var success = viewFactory.TryCreateView("NonExistentPage", out view);

            Assert.False(success);
            Assert.Null(view);
        }

        public ViewFactory CreateViewFactory()
        {
            var containerConfiguration = new ContainerConfiguration();
            containerConfiguration.WithPart<TestView>();

            var container = containerConfiguration.CreateContainer();

            return new ViewFactory(container);
        }

        [ViewExport("TestPage")]
        public class TestView
        {

        }
    }
}