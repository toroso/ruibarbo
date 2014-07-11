namespace tungsten.core
{
    public interface IHasStrongReference<out T>
    {
        T GetStrongReference();
    }
}