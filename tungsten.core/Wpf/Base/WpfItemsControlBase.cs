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

        public TWpfItem FindFirstItem<TWpfItem>()
            where TWpfItem : class, ISearchSourceElement
        {
            return FindFirstItem<TWpfItem>(By.Empty);
        }

        public TWpfItem FindFirstItem<TWpfItem>(params Func<IByBuilder<TWpfItem>, By>[] byBuilders)
            where TWpfItem : class, ISearchSourceElement
        {
            return FindFirstItem<TWpfItem>(byBuilders.Build());
        }

        public TWpfItem FindFirstItem<TWpfItem>(params By[] bys)
            where TWpfItem : class, ISearchSourceElement
        {
            var found = TryRepeatedlyToFindFirstItem<TWpfItem>(TimeSpan.FromSeconds(5), bys);
            if (found == null)
            {
                var controlToStringCreator = new ByControlToStringCreator<TWpfItem>(bys.RemoveByName().ToArray());
                var sb = new StringBuilder();
                foreach (var item in NativeItems)
                {
                    sb.AppendLine(string.Format("   {0}", controlToStringCreator.ControlToString(item)));
                }

                throw ManglaException.FindFailed("Item", this, bys, sb.ToString());
            }

            return found;
        }

        public TWpfItem TryRepeatedlyToFindFirstItem<TWpfItem>(TimeSpan maxRetryTime)
            where TWpfItem : class, ISearchSourceElement
        {
            return TryRepeatedlyToFindFirstItem<TWpfItem>(maxRetryTime, By.Empty);
        }

        public TWpfItem TryRepeatedlyToFindFirstItem<TWpfItem>(TimeSpan maxRetryTime, params Func<IByBuilder<TWpfItem>, By>[] byBuilders)
            where TWpfItem : class, ISearchSourceElement
        {
            return TryRepeatedlyToFindFirstItem<TWpfItem>(maxRetryTime, byBuilders.Build());
        }

        public TWpfItem TryRepeatedlyToFindFirstItem<TWpfItem>(TimeSpan maxRetryTime, params By[] bys)
            where TWpfItem : class, ISearchSourceElement
        {
            TWpfItem found = null;
            Wait.Until(
                () =>
                    {
                        found = TryOnceToFindFirstItem<TWpfItem>(bys);
                        return found != null;
                    },
                maxRetryTime);

            return found;
        }

        public TWpfItem TryOnceToFindFirstItem<TWpfItem>()
            where TWpfItem : class, ISearchSourceElement
        {
            return TryOnceToFindFirstItem<TWpfItem>(By.Empty);
        }

        public TWpfItem TryOnceToFindFirstItem<TWpfItem>(params Func<IByBuilder<TWpfItem>, By>[] byBuilders)
            where TWpfItem : class, ISearchSourceElement
        {
            return TryOnceToFindFirstItem<TWpfItem>(byBuilders.Build());
        }

        public TWpfItem TryOnceToFindFirstItem<TWpfItem>(params By[] bys)
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