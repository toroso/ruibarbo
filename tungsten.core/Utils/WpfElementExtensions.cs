using System.Linq;
using System.Text;
using tungsten.core.Elements;

namespace tungsten.core.Utils
{
    public static class WpfElementExtensions
    {
        public static string ElementNamePath(this UntypedWpfElement me)
        {
            return me.ElementPath
                .Select(e => e.Name)
                .JoinExcludeEmpty(".");
        }

        public static string ElementClassPath(this UntypedWpfElement me)
        {
            return me.ElementPath
                .Select(e => e.Class)
                .Where(t => t != null)
                .Select(t => t.Name)
                .Join(".");
        }

        public static string ElementNameOrClassPath(this UntypedWpfElement me)
        {
            return me.ElementPath
                .Select(e => !string.IsNullOrEmpty(e.Name)
                    ? e.Name
                    : e.Class != null
                        ? e.Class.Name
                        : null)
                .JoinExcludeEmpty(".");
        }

        public static string ElementSearchPath(this UntypedWpfElement me)
        {
            return me.ElementPath
                .Where(e => e.SearchConditions.Any())
                .Select(e => string.Format("<{0}>",
                    e.SearchConditions
                        .Select(by => by.ToString())
                        .Join(", ")))
                .Join("; ");

        }

        public static string ControlTreeAsString(this SearchSourceElement me, int maxDepth)
        {
            return me.ControlTreeAsString(0, maxDepth);
        }

        private static string ControlTreeAsString(this SearchSourceElement me, int currentDepth, int maxDepth)
        {
            if (currentDepth > maxDepth)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.AppendIndentedLine(
                (3 * currentDepth) + 3,
                "{0}",
                me.ControlIdentifier());

            foreach (var child in me.Children)
            {
                // TODO: Don't show all possible instances of children, just one per FrameworkElement
                sb.Append(child.ControlTreeAsString(currentDepth + 1, maxDepth));
            }

            return sb.ToString();
        }

        public static string ControlIdentifier(this SearchSourceElement me)
        {
            string name = me.Name;
            string controlIdentifier = string.IsNullOrEmpty(name)
                ? me.Class.ToString()
                : string.Format("{0} ({1})", name, me.Class);
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