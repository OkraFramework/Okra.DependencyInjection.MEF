namespace Okra.DependencyInjection.MEF
{
    public interface IViewModelFactory
    {
        bool TryCreateViewModel(string pageName, out object viewModel);
    }
}