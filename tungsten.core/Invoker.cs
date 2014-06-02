using System;
using System.Threading;
using System.Windows.Threading;

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