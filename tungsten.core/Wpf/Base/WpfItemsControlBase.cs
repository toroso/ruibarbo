using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Wpf.Base
{
    public class WpfItemsControlBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ItemsControl
    {
        public WpfItemsControlBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public TWpfItem FindFirstItem<TWpfItem>(params Func<IByBuilder<TWpfItem>, By>[] byBuilders)
            where TWpfItem : class, ISearchSourceElement
        {
            return FindFirstItem<TWpfItem>((By[]) byBuilders.Build());
        }

        public TWpfItem FindFirstItem<TWpfItem>(params By[] bys)
            where TWpfItem : class, ISearchSourceElement
        {
            var found = TryFindFirstItem<TWpfItem>(bys);
            if (found == null)
            {
                var sb = new StringBuilder();
                foreach (var item in NativeItems)
                {
                    IEnumerable<ISearchSourceElement> wpfElements = ElementFactory.ElementFactory.CreateElements(this, item).ToArray();
                    var matchingTypes = wpfElements.Select(t => t.GetType().Name).Join(", ");
                    var wpfElement = wpfElements.FirstOrDefault(); // Any will do
                    sb.AppendLine(string.Format("   {0} <{1}>", wpfElement.ControlIdentifier(), matchingTypes));
                }

                throw ManglaException.FindFailed("Item", this, bys, sb.ToString());
            }

            return found;
        }

        public TWpfItem TryFindFirstItem<TWpfItem>(params Func<IByBuilder<TWpfItem>, By>[] byBuilders)
            where TWpfItem : class, ISearchSourceElement
        {
            return TryFindFirstItem<TWpfItem>(byBuilders.Build());
        }

        public TWpfItem TryFindFirstItem<TWpfItem>(params By[] bys)
            where TWpfItem : class, ISearchSourceElement
        {
            return AllItems<TWpfItem>().FirstOrDefault(item => bys.All(by => by.Matches(item)));
        }

        public virtual IEnumerable<TWpfItem> AllItems<TWpfItem>()
            where TWpfItem : ISearchSourceElement
        {
            IEnumerable<object> nativeItems = NativeItems;
            return nativeItems
                .SelectMany(item => ElementFactory.ElementFactory.CreateElements(this, item))
                .OfType<TWpfItem>();
        }

        private IEnumerable<object> NativeItems
        {
            get
            {
                return Invoker.Get(this, frameworkElement =>
                    frameworkElement.Items
                        .Cast<object>()
                        .Select(item => item is System.Windows.FrameworkElement
                            ? item
                            : frameworkElement.ItemContainerGenerator.ContainerFromItem(item))
                        .ToArray());
            }
        }
    }
}