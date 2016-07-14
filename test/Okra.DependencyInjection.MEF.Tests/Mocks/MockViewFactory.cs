using System;
using Okra.DependencyInjection.MEF;

namespace Okra.DependencyInjection.MEF.Tests.Mocks
{
    public class MockViewFactory : IViewFactory
    {
        public bool TryCreateView(string pageName, out object view)
        {
            if (pageName == "TestPage" || pageName == "TestPageWithViewModel")
            {
                view = new MockPage() {PageName = pageName};
                return true;
            }
            else
            {
                view = null;
                return false;
            }
        }
    }
}