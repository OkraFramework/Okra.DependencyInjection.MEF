namespace Okra.DependencyInjection.MEF.Tests.Mocks
{
    public class MockViewModelFactory : IViewModelFactory
    {
        public bool TryCreateViewModel(string pageName, out object viewModel)
        {
            if (pageName == "TestPageWithViewModel")
            {
                viewModel = new MockViewModel() {PageName = pageName};
                return true;
            }
            else
            {
                viewModel = null;
                return false;
            }
        }
    }
}