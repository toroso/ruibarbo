namespace ruibarbo.core.Wpf.Invoker
{
    public interface IHasStrongReference<out T>
    {
        T GetStrongReference();
    }
}