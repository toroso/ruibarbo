using System.Collections.Generic;
using tungsten.core.Search;

namespace tungsten.core.ElementFactory
{
    public interface IElementFactory
    {
        IEnumerable<ISearchSourceElement> CreateElements(ISearchSourceElement parent, object nativeObject);
        IEnumerable<object> GetRootElements();
    }
}