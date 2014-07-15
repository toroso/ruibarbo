namespace ruibarbo.core.ElementFactory
{
    public interface IElementFactoryConfigurator
    {
        void AddFactory(IElementFactory factory);
        void RemoveAllFactories();
    }
}
