using System;
using System.Collections.Generic;
using Okra.Routing;

namespace Okra.DependencyInjection.MEF.Tests.Mocks
{
    public class MockRouteBuilder : IRouteBuilder
    {
        public List<Func<ViewRouterDelegate, ViewRouterDelegate>> Routers = new List<Func<ViewRouterDelegate, ViewRouterDelegate>>();

        public IServiceProvider ApplicationServices
        {
            get;
            set;
        }

        public IRouteBuilder AddRouter(Func<ViewRouterDelegate, ViewRouterDelegate> router)
        {
            Routers.Add(router);
            return this;
        }

        public ViewRouterDelegate Build()
        {
            throw new NotImplementedException();
        }
    }
}