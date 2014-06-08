using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.Elements;

namespace tungsten.core
{
    public class Invoker
    {
        private static readonly ThreadLocal<Invoker> Instances = new ThreadLocal<Invoker>();

        private static Invoker Instance
        {
            get { return Instances.Value; }
        }

        private readonly Dispatcher _dispatcher;

        private Invoker(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        internal static void Create(Dispatcher dispatcher)
        {
            Instances.Value = new Invoker(dispatcher);
        }

        public static TRet Get<TRet, TE1>(WpfElement<TE1> e1, Func<TE1, TRet> func)
            where TE1 : FrameworkElement
        {
            var frameworkElement1 = e1.GetStrongReference();
            return Instance.GetImpl(() => func(frameworkElement1));
        }

        public static TRet Get<TRet, TE1, TE2>(WpfElement<TE1> e1, WpfElement<TE2> e2, Func<TE1, TE2, TRet> func)
            where TE1 : FrameworkElement
            where TE2 : FrameworkElement
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

        public static void Invoke<TE1>(WpfElement<TE1> e1, Action<TE1> action)
            where TE1 : FrameworkElement
        {
            var frameworkElement1 = e1.GetStrongReference();
            Instance.InvokeImpl(() => action(frameworkElement1));
        }

        public static void Invoke<TE1, TE2>(WpfElement<TE1> e1, WpfElement<TE2> e2, Action<TE1, TE2> action)
            where TE1 : FrameworkElement
            where TE2 : FrameworkElement
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
            _dispatcher.BeginInvokeShutdown(DispatcherPriority.Send);
        }
    }
}