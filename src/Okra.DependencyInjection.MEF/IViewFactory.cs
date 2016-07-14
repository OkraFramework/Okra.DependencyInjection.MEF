namespace Okra.DependencyInjection.MEF
{
    public interface IViewFactory
    {
        bool TryCreateView(string pageName, out object view);
    }
}