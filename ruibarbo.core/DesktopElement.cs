using System.Collections.Generic;
using ruibarbo.core.ElementFactory;

namespace ruibarbo.core
{
    public class DesktopElement : ISearchSourceElement
    {
        internal DesktopElement()
        {
        }

        public string Name
        {
            get { return "Desktop"; }
        }

        public string Class
        {
            get { return "Desktop"; }
        }

        public IEnumerable<object> NativeChildren
        {
            get { return ElementFactory.ElementFactory.GetRootElements(); }
        }

        public object NativeParent
        {
            get { return null; }
        }

        public string FoundBy
        {
            get { return string.Empty; }
        }

        public ISearchSourceElement SearchParent
        {
            get { return null; }
        }

        public int InstanceId
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

        public void DoubleClick()
        {
        }
    }
}