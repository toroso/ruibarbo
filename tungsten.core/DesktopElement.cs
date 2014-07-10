using System.Collections.Generic;

namespace tungsten.core
{
    public class DesktopElement : ISearchSourceElement
    {
        internal DesktopElement()
        {
        }

        public virtual string Name
        {
            get { return "Desktop"; }
        }

        public virtual string Class
        {
            get { return "Desktop"; }
        }

        public virtual IEnumerable<object> NativeChildren
        {
            get { return ElementFactory.ElementFactory.GetRootElements(); }
        }

        public object NativeParent
        {
            get { return null; }
        }

        public virtual string FoundBy
        {
            get { return string.Empty; }
        }

        public virtual ISearchSourceElement SearchParent
        {
            get { return null; }
        }

        public virtual int InstanceId
        {
            get { return GetHashCode(); }
        }

        public bool IsVisible
        {
            get { return true; }
        }

        public bool IsEnabled
        {
            get { return true; }
        }

        public void Click()
        {
        }
    }
}