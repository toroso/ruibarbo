using System.Collections.Generic;

namespace ruibarbo.core.ElementFactory
{
    public interface IElementFactory
    {
        IEnumerable<ISearchSourceElement> CreateElements(ISearchSourceElement parent, object nativeObject);
        IEnumerable<object> GetRootElements();
    }
}