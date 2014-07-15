using System;
using System.Threading;
using System.Windows.Threading;
using ruibarbo.core.Win32;

namespace ruibarbo.core.Wpf.Invoker
{
    public sealed class OnUiThread
    {
        private static readonly ThreadLocal<OnUiThread> Instances = new ThreadLocal<OnUiThread>();

        private static OnUiThread Instance
        {
            get { return Instances.Value; }
        }

        private readonly Dispatcher _dispatcher;

        private OnUiThread(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        internal static void Create(Dispatcher dispatcher)
        {
            Instances.Value = new OnUiThread(dispatcher);
        }

        public static TRet Get<TRet, TE1>(IHasStrongReference<TE1> e1, Func<TE1, TRet> func)
        {
            var frameworkElement1 = e1.GetStrongReference();
            return Instance.GetImpl(() => func(frameworkElement1));
        }

        public static TRet Get<TRet, TE1, TE2>(IHasStrongReference<TE1> e1, IHasStrongReference<TE2> e2, Func<TE1, TE2, TRet> func)
        {
            var frameworkElement1 = e1.GetStrongReference();
            var frameworkElement2 = e2.GetStrongReference();
            return Instance.GetImpl(() => func(frameworkElement1, frameworkElement2));
        }

        public static TRet Get<TRet>(Func<TRet> func)
        {
            return Instance.GetImpl(func);
        }

        private TRet GetImpl<TRet>(Func<TRet> func)
        {
            TRet ret = default(TRet);
            _dispatcher.Invoke(() =>
                {
                    ret = func();
                });
            return ret;
        }

        public static void Invoke<TE1>(IHasStrongReference<TE1> e1, Action<TE1> action)
        {
            var frameworkElement1 = e1.GetStrongReference();
            Instance.InvokeImpl(() => action(frameworkElement1));
        }

        public static void Invoke<TE1, TE2>(IHasStrongReference<TE1> e1, IHasStrongReference<TE2> e2, Action<TE1, TE2> action)
        {
            var frameworkElement1 = e1.GetStrongReference();
            var frameworkElement2 = e2.GetStrongReference();
            Instance.InvokeImpl(() => action(frameworkElement1, frameworkElement2));
        }

        public static void Invoke(Action action)
        {
            Instance.InvokeImpl(action);
        }

        private void InvokeImpl(Action action)
        {
            _dispatcher.Invoke(action);
        }

        internal static void BeginInvokeShutdown()
        {
            Instance.BeginInvokeShutdownImpl();
        }

        private void BeginInvokeShutdownImpl()
        {
            Win32Api.CloseAllWindows();
            _dispatcher.BeginInvokeShutdown(DispatcherPriority.Send);
        }
    }
}