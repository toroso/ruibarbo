namespace tungsten.core.Win32.Factory
{
    internal class Win32FactoryConfigurator : IWin32FactoryConfigurator
    {
        private readonly Win32ControlFactory _win32ControlFactory;

        public Win32FactoryConfigurator(Win32ControlFactory win32ControlFactory)
        {
            _win32ControlFactory = win32ControlFactory;
        }

        public void AddControl<TWin32Control>()
            where TWin32Control : Win32Control
        {
            _win32ControlFactory.AddControl<TWin32Control>();
        }
    }
}