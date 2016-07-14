using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting.Core;

namespace Okra.DependencyInjection.MEF
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CompositionContext _compositionContext;

        public ViewModelFactory(CompositionContext compositionContext)
        {
            _compositionContext = compositionContext;
        }
        
        public bool TryCreateViewModel(string pageName, out object viewModel)
        {
            Dictionary<string, object> metadataConstriants = new Dictionary<string, object>();
            metadataConstriants["PageName"] = pageName;

            return _compositionContext.TryGetExport(new CompositionContract(typeof(object), "OkraViewModel", metadataConstriants), out viewModel);
        }
    }
}