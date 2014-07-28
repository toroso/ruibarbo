﻿using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfTextBlockBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TextBlock
    {
        public WpfTextBlockBase(ISearchSourceElement searchParent, TNativeElement textBlock)
            : base(searchParent, textBlock)
        {
        }
    }

    public static class WpfTextBlockBaseExtensions
    {
        public static string Text<TNativeElement>(this WpfTextBlockBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TextBlock
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.Text);
        }
    }
}