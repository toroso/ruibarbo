using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tungsten.core.ElementFactory;
using tungsten.core.Utils;

namespace tungsten.core.Debug
{
    public static class DebugExtensions
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
                .Select(e => e.ClassShort())
                .Join(".");
        }

        private static string ClassShort(this ISearchSourceElement me)
        {
            if (me == null)
            {
                return string.Empty;
            }

            var type = me.Class;
            var index = type.LastIndexOf(".", StringComparison.Ordinal);
            return index == -1 || type.Last() == '.'
                ? type
                : type.Substring(index + 1);
        }

        public static string ElementNameOrClassPath(this ISearchSourceElement me)
        {
            return me.ElementPath()
                .Select(e => !string.IsNullOrEmpty(e.Name)
                    ? e.Name
                    : e.ClassShort())
                .JoinExcludeEmpty(".");
        }

        public static string ElementSearchPath(this ISearchSourceElement me)
        {
            return me.ElementPath()
                .Where(e => e.FoundBy.Any())
                .Select(e => string.Format("<{0}>", e.FoundBy))
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
            return ControlTreeAsString(me, new DefaultControlToStringCreator(), maxDepth);
        }

        public static string ControlTreeAsString(this ISearchSourceElement me, IControlToStringCreator controlToStringCreator, int maxDepth)
        {
            return ControlTreeAsString(me, controlToStringCreator, 0, maxDepth);
        }

        private static string ControlTreeAsString(this ISearchSourceElement parent, IControlToStringCreator controlToStringCreator, int currentDepth, int maxDepth)
        {
            if (currentDepth > maxDepth)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var child in parent.NativeChildren)
            {
                sb.AppendIndentedLine((3 * currentDepth) + 3, "{0}", controlToStringCreator.ControlToString(child));
                var element = ElementFactory.ElementFactory.CreateElements(parent, child).FirstOrDefault();
                if (element != null)
                {
                    sb.Append(element.ControlTreeAsString(controlToStringCreator, currentDepth + 1, maxDepth));
                }
            }

            return sb.ToString();
        }

        public static string ControlAncestorsAsString(this ISearchSourceElement child, IControlToStringCreator controlToStringCreator)
        {
            var ancestorsAsString = child.ControlAncestorsAsStrings(controlToStringCreator).ToArray();
            if (ancestorsAsString.Length == 0)
            {
                return string.Format("   No parents of element {0}", child.ControlIdentifier());
            }

            var sb = new StringBuilder();
            int currentDepth = 0;
            foreach (var ancestor in ancestorsAsString)
            {
                sb.AppendIndentedLine((3 * currentDepth) + 3, "{0}", ancestor);
                currentDepth++;
            }

            sb.AppendIndentedLine((3 * currentDepth) + 3, "{0}", child.ControlIdentifier());
            return sb.ToString();
        }

        private static IEnumerable<string> ControlAncestorsAsStrings(this ISearchSourceElement child, IControlToStringCreator controlToStringCreator)
        {
            var parent = child.NativeParent;
            if (parent == null)
            {
                yield break;
            }

            var element = ElementFactory.ElementFactory.CreateElements(null, parent).First();

            foreach (var each in element.ControlAncestorsAsStrings(controlToStringCreator))
            {
                yield return each;
            }

            yield return controlToStringCreator.ControlToString(parent);
        }

        public static string ControlIdentifier(this ISearchSourceElement me)
        {
            if (me == null)
            {
                return "-";
            }

            string name = me.Name;
            string type = me.Class;
            int instanceId = me.InstanceId;
            string controlIdentifier = string.IsNullOrEmpty(name)
                ? string.Format("{0} #{1}", type, instanceId)
                : string.Format("{0} ({1}) #{2}", name, type, instanceId);
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

        private static IEnumerable<ISearchSourceElement> ElementPath(this ISearchSourceElement me)
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