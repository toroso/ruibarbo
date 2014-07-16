using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;

namespace ruibarbo.core.Wpf.Base
{
    public interface IComboBox : ISearchSourceElement
    {
        TWpfItem FindFirstItem<TWpfItem>(params By[] bys)
            where TWpfItem : class, ISearchSourceElement;
    }
}