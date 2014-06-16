using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public interface ISearchSourceElement
    {
        string Name { get; }
        Type Class { get; }

        IEnumerable<FrameworkElement> NativeChildren { get; }
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

        public static IEnumerable<UntypedWpfElement> Children(this ISearchSourceElement me)
        {
            return me.NativeChildren.SelectMany(element => ElementFactory.ElementFactory.CreateWpfElements(me, element));
        }
    }
}