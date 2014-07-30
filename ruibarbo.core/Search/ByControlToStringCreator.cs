using System.Linq;
using ruibarbo.core.Debug;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Utils;

namespace ruibarbo.core.Search
{
    internal class ByControlToStringCreator<TElement> : IControlToStringCreator
        where TElement : class, ISearchSourceElement
    {
        private readonly By[] _bys;

        public ByControlToStringCreator(By[] bys)
        {
            _bys = bys;
        }

        public string ControlToString(object nativeElement)
        {
            var elements = ElementFactory.ElementFactory.CreateElements(null, nativeElement).ToArray();
            var elementsWithMatchingType = elements.OfType<TElement>().ToArray();
            var element = elementsWithMatchingType.Any()
                ? elementsWithMatchingType.First()
                : elements.FirstOrDefault(); // Any will do

            var bysAsString = BysAsStringFor(element);

            string matchingTypesAsString = elements.Select(t => t.GetType().Name).Join("; ");

            return string.Format("{0}{1} <{2}>\n",
                element.ControlIdentifier(),
                bysAsString,
                matchingTypesAsString);
        }

        private string BysAsStringFor(ISearchSourceElement element)
        {
            if (_bys.Length == 0)
            {
                return string.Empty;
            }

            var asTElement = element as TElement;
            return asTElement != null
                ? string.Format(" <{0}>", _bys.Select(by => by.ExtractedToString(asTElement)).Join("; "))
                : string.Empty;
        }
    }
}