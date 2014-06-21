using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using tungsten.core.Elements;

namespace tungsten.core.Utils
{
    public static class WpfElementExtensions
    {
        public static string ElementNamePath(this ISearchSourceElement me)
        {
            return me.ElementPath()
                .Select(e => e.Name)
                .JoinExcludeEmpty(".");
        }

        public static string ElementClassPath(this ISearchSourceElement me)
        {
            return me.ElementPath()
                .Select(e => e.Class)
                .Where(t => t != null)
                .Select(t => t.Name)
                .Join(".");
        }

        public static string ElementNameOrClassPath(this ISearchSourceElement me)
        {
            return me.ElementPath()
                .Select(e => !string.IsNullOrEmpty(e.Name)
                    ? e.Name
                    : e.Class != null
                        ? e.Class.Name
                        : null)
                .JoinExcludeEmpty(".");
        }

        public static string ElementSearchPath(this ISearchSourceElement me)
        {
            return me.ElementPath()
                .Where(e => e.FoundBys.Any())
                .Select(e => string.Format("<{0}>",
                    e.FoundBys
                        .Select(by => by.ToString())
                        .Join(", ")))
                .Join("; ");
        }

        public static string ControlIdentifierPath(this ISearchSourceElement me)
        {
            var sb = new StringBuilder();
            int currentDepth = 0;
            foreach (var ancestor in me.ElementPath())
            {
                sb.AppendIndentedLine((3 * currentDepth) + 3, "{0} ({1})", ancestor.ControlIdentifier(), ancestor.GetType().Name);
                currentDepth++;
            }

            return sb.ToString();
        }

        public static string ControlTreeAsString(this ISearchSourceElement me, int maxDepth)
        {
            return ControlTreeAsString(me, 0, maxDepth);
        }

        private static string ControlTreeAsString(this ISearchSourceElement parent, int currentDepth, int maxDepth)
        {
            if (currentDepth > maxDepth)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var frameworkElement in parent.NativeChildren)
            {
                IEnumerable<ISearchSourceElement> wpfElements = ElementFactory.ElementFactory.CreateWpfElements(parent, frameworkElement).ToArray();
                var matchingTypes = wpfElements.Select(t => t.GetType().Name).Join(", ");
                var wpfElement = wpfElements.FirstOrDefault(); // Any will do
                sb.AppendIndentedLine((3 * currentDepth) + 3, "{0} <{1}>", wpfElement.ControlIdentifier(), matchingTypes);
                if (wpfElement != null)
                {
                    sb.Append(wpfElement.ControlTreeAsString(currentDepth + 1, maxDepth));
                }
            }

            return sb.ToString();
        }

        public static string ControlAncestorsAsString(this ISearchSourceElement child)
        {
            var ancestors = child.ControlAncestorsAsStrings().ToArray();
            if (ancestors.Length == 0)
            {
                return string.Format("   No parents of element {0}", child.ControlIdentifier());
            }

            var sb = new StringBuilder();
            int currentDepth = 0;
            foreach (var ancestor in ancestors)
            {
                sb.AppendIndentedLine((3 * currentDepth) + 3, "{0}", ancestor);
                currentDepth++;
            }

            sb.AppendIndentedLine((3 * currentDepth) + 3, "{0}", child.ControlIdentifier());
            return sb.ToString();
        }

        private static IEnumerable<string> ControlAncestorsAsStrings(this ISearchSourceElement child)
        {
            FrameworkElement frameworkElement = child.NativeParent;
            if (frameworkElement == null)
            {
                yield break;
            }

            IEnumerable<ISearchSourceElement> wpfElements = ElementFactory.ElementFactory.CreateWpfElements(null, frameworkElement).ToArray();
            var wpfElement = wpfElements.First(); // Any will do

            foreach (var each in wpfElement.ControlAncestorsAsStrings())
            {
                yield return each;
            }

            var matchingTypes = wpfElements.Select(t => t.GetType().Name).Join(", ");
            yield return string.Format("{0} <{1}>", wpfElement.ControlIdentifier(), matchingTypes);
        }

        public static string ControlIdentifier(this ISearchSourceElement me)
        {
            if (me == null)
            {
                return "-";
            }

            string name = me.Name;
            Type type = me.Class;
            string typeAsString = type != null ? type.ToString() : "Desktop";
            int instanceId = me.InstanceId;
            string controlIdentifier = string.IsNullOrEmpty(name)
                ? string.Format("{0} #{1}", typeAsString, instanceId)
                : string.Format("{0} ({1}) #{2}", name, typeAsString, instanceId);
            return controlIdentifier;
        }

        private static void AppendIndentedLine(this StringBuilder me, int indentLevel, string format, params object[] args)
        {
            for (int i = 0; i < indentLevel; i++)
            {
                me.Append(" ");
            }

            me.AppendLine(string.Format(format, args));
        }
    }
}