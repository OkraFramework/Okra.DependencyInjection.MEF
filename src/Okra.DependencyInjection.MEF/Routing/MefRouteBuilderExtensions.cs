using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Okra.DependencyInjection.MEF;

namespace Okra.Routing
{
    public static class MefRouteBuilderExtensions
    {
        public static IRouteBuilder MapViewsWithMef(this IRouteBuilder routeBuilder, MefRoutingType routingType)
        {
            return routeBuilder.AddRoute(context =>
            {
                var viewFactory = context.PageServices.GetRequiredService<IViewFactory>();
                
                object view;
                if (viewFactory.TryCreateView(context.PageName, out view))
                {
                    object viewModel = null;

                    if (routingType == MefRoutingType.ViewsAndViewModels)
                    {
                        var viewModelFactory = context.PageServices.GetRequiredService<IViewModelFactory>();
                        viewModelFactory.TryCreateViewModel(context.PageName, out viewModel);
                    }

                    return Task.FromResult(new ViewInfo(view, viewModel));
                }
                else
                {
                    return Task.FromResult<ViewInfo>(null);
                }
            });
        }
    }
}