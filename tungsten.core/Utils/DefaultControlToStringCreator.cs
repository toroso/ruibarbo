using System.Linq;

namespace tungsten.core.Utils
{
    internal class DefaultControlToStringCreator : IControlToStringCreator
    {
        public string ControlToString(object nativeElement)
        {
            var elements = ElementFactory.ElementFactory.CreateElements(null, nativeElement).ToArray();
            var element = elements.FirstOrDefault(); // Any will do

            string matchingTypesAsString = elements.Select(t => t.GetType().Name).Join("; ");
            
            return string.Format("{0} <{1}>",
                element.ControlIdentifier(),
                matchingTypesAsString);
        }
    }
}