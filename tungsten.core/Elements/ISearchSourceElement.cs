using System;
using System.Collections.Generic;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public interface ISearchSourceElement
    {
        string Name { get; }
        Type Class { get; }

        IEnumerable<FrameworkElement> NativeChildren { get; }
        IEnumerable<UntypedWpfElement> Children { get; } // TODO: Extension method
        // TODO: Add NativeParent: object
        
        IEnumerable<By> SearchConditions { get; }
        ISearchSourceElement SearchParent { get; }

        int InstanceId { get; }
    }

    public static class SearchSourceElementExtensions
    {
        public static IEnumerable<ISearchSourceElement> ElementPath(this ISearchSourceElement me)
        {
            if (me.SearchParent != null)
            {
                foreach (var ancestor in me.SearchParent.ElementPath())
                {
                    yield return ancestor;
                }
            }
            yield return me;
        }
    }
}