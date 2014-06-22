using System.Collections.Generic;

namespace tungsten.core.ElementFactory
{
    public interface IElementFactory
    {
        IEnumerable<ISearchSourceElement> CreateElements(ISearchSourceElement parent, object nativeObject);
    }
}