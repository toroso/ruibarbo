using tungsten.core.Search;
using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf.Search
{
    public static class ByWpf
    {
        public static By TextBlockText(string text)
        {
            return By.FirstChild<WpfTextBlock>(txb => txb.Text() == text);
        }
    }
}