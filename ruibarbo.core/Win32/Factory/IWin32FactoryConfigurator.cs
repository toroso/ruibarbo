namespace ruibarbo.core.Win32.Factory
{
    public interface IWin32FactoryConfigurator
    {
        void AddControl<TWin32Control>()
            where TWin32Control : Win32Control;
    }
}