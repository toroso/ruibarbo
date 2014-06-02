using System.Linq;
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
    }
}