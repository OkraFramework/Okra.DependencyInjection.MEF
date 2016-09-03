using Microsoft.Extensions.DependencyInjection;
using Okra.DependencyInjection.MEF;

namespace Okra.DependencyInjection
{
    public static class MefServiceCollectionExtensions
    {
        public static IServiceCollection AddMefRouting(this IServiceCollection services)
        {
            services.AddScoped<IViewFactory, ViewFactory>()
                    .AddScoped<IViewModelFactory, ViewModelFactory>();

            return services;
        }
    }
}
