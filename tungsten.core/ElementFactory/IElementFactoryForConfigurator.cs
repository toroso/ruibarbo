using tungsten.core.Elements;

namespace tungsten.core.ElementFactory
{
    public interface IElementFactoryForConfigurator
    {
        void Create<TWpfElement>() where TWpfElement : UntypedWpfElement;
    }
}