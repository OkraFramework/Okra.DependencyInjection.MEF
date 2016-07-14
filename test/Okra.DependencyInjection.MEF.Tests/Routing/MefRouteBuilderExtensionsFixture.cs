using System.Threading.Tasks;
using Okra.DependencyInjection.MEF.Tests.Mocks;
using Okra.Routing;
using Xunit;

namespace Okra.DependencyInjection.MEF.Tests.Routing
{
    public class MefRouteBuilderExtensionsFixture
    {
        [Fact]
        public void MapViewsWithMef_AddsRouterToRouteBuilder()
        {
            var routeBuilder = CreateRouteBuilder();

            var result = routeBuilder.MapViewsWithMef(MefRoutingType.ViewsOnly);

            Assert.Equal(routeBuilder, result);
            Assert.Equal(1, routeBuilder.Routers.Count);
        }

        [Fact]
        public async void MapViewsWithMef_ViewsOnly_ReturnsSpecifiedPage()
        {
            var routeBuilder = CreateRouteBuilder();
            var pageServiceProvider = CreatePageServiceProvider();
            routeBuilder.MapViewsWithMef(MefRoutingType.ViewsOnly);
            
            var context = new RouteContext("TestPage", pageServiceProvider);
            var viewRouterDelegate = routeBuilder.Routers[0]((c) =>
                {
                    Assert.True(false, "The subsequent router should not be called");
                    return Task.FromResult<ViewInfo>(null);
                });
            var viewInfo = await viewRouterDelegate(context);

            Assert.NotNull(viewInfo);
            Assert.NotNull(viewInfo.View);
            Assert.Null(viewInfo.ViewModel);
            Assert.IsType<MockPage>(viewInfo.View);
            Assert.Equal("TestPage", ((MockPage)viewInfo.View).PageName);
        }

        [Fact]
        public async void MapViewsWithMef_ViewsAndViewModels_ReturnsSpecifiedPage_WithoutViewModel()
        {
            var routeBuilder = CreateRouteBuilder();
            var pageServiceProvider = CreatePageServiceProvider();
            routeBuilder.MapViewsWithMef(MefRoutingType.ViewsAndViewModels);
            
            var context = new RouteContext("TestPage", pageServiceProvider);
            var viewRouterDelegate = routeBuilder.Routers[0]((c) =>
                {
                    Assert.True(false, "The subsequent router should not be called");
                    return Task.FromResult<ViewInfo>(null);
                });
            var viewInfo = await viewRouterDelegate(context);

            Assert.NotNull(viewInfo);
            Assert.NotNull(viewInfo.View);
            Assert.Null(viewInfo.ViewModel);
            Assert.IsType<MockPage>(viewInfo.View);
            Assert.Equal("TestPage", ((MockPage)viewInfo.View).PageName);
        }

        [Fact]
        public async void MapViewsWithMef_ViewsAndViewModels_ReturnsSpecifiedPage_WithViewModel()
        {
            var routeBuilder = CreateRouteBuilder();
            var pageServiceProvider = CreatePageServiceProvider();
            routeBuilder.MapViewsWithMef(MefRoutingType.ViewsAndViewModels);
            
            var context = new RouteContext("TestPageWithViewModel", pageServiceProvider);
            var viewRouterDelegate = routeBuilder.Routers[0]((c) =>
                {
                    Assert.True(false, "The subsequent router should not be called");
                    return Task.FromResult<ViewInfo>(null);
                });
            var viewInfo = await viewRouterDelegate(context);

            Assert.NotNull(viewInfo);
            Assert.NotNull(viewInfo.View);
            Assert.NotNull(viewInfo.ViewModel);
            Assert.IsType<MockPage>(viewInfo.View);
            Assert.IsType<MockViewModel>(viewInfo.ViewModel);
            Assert.Equal("TestPageWithViewModel", ((MockPage)viewInfo.View).PageName);
            Assert.Equal("TestPageWithViewModel", ((MockViewModel)viewInfo.ViewModel).PageName);
        }

        [Fact]
        public async void MapViewsWithMef_ForwardsToSubRouterIfNoPageAvailable()
        {
            var routeBuilder = CreateRouteBuilder();
            var pageServiceProvider = CreatePageServiceProvider();
            var fallbackPage = new MockPage();
            routeBuilder.MapViewsWithMef(MefRoutingType.ViewsOnly);
            
            var context = new RouteContext("NonExistentPage", pageServiceProvider);
            var viewRouterDelegate = routeBuilder.Routers[0]((c) =>
                {
                    Assert.Equal(context, c);
                    return Task.FromResult(new ViewInfo(fallbackPage));
                });
            var viewInfo = await viewRouterDelegate(context);

            Assert.NotNull(viewInfo);
            Assert.NotNull(viewInfo.View);
            Assert.Null(viewInfo.ViewModel);
            Assert.Equal(fallbackPage, viewInfo.View);
        }

        public MockRouteBuilder CreateRouteBuilder()
        {
            var serviceProvider = new MockServiceProvider();
            var routeBuilder = new MockRouteBuilder() { ApplicationServices = serviceProvider };

            return routeBuilder;
        }

        public MockServiceProvider CreatePageServiceProvider()
        {
            var serviceProvider = new MockServiceProvider();
            
            serviceProvider.With<IViewFactory>(new MockViewFactory());
            serviceProvider.With<IViewModelFactory>(new MockViewModelFactory());

            return serviceProvider;
        }
    }
}