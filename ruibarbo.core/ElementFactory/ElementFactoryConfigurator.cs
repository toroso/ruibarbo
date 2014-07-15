namespace ruibarbo.core.ElementFactory
{
    internal class ElementFactoryConfigurator : IElementFactoryConfigurator
    {
        public void AddFactory(IElementFactory factory)
        {
            ElementFactory.AddFactory(factory);
        }

        public void RemoveAllFactories()
        {
            ElementFactory.RemoveAllFactories();
        }
    }
}