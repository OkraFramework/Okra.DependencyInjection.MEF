using System.Composition.Hosting;
using Xunit;

namespace Okra.DependencyInjection.MEF.Tests
{
    public class ViewModelFactoryFixture
    {
        [Fact]
        public void TryCreateViewModel_ReturnsViewModelIfAvailable()
        {
            var viewModelFactory = CreateViewModelFactory();
            
            object viewModel;
            var success = viewModelFactory.TryCreateViewModel("TestPage", out viewModel);

            Assert.True(success);
            Assert.NotNull(viewModel);
            Assert.IsType<TestViewModel>(viewModel);
        }

        [Fact]
        public void TryCreateViewModel_ReturnsFalseIfNotAvailable()
        {
            var viewModelFactory = CreateViewModelFactory();
            
            object viewModel;
            var success = viewModelFactory.TryCreateViewModel("NonExistentPage", out viewModel);

            Assert.False(success);
            Assert.Null(viewModel);
        }

        public ViewModelFactory CreateViewModelFactory()
        {
            var containerConfiguration = new ContainerConfiguration();
            containerConfiguration.WithPart<TestViewModel>();

            var container = containerConfiguration.CreateContainer();

            return new ViewModelFactory(container);
        }

        [ViewModelExport("TestPage")]
        public class TestViewModel
        {

        }
    }
}