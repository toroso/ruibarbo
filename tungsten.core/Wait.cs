using System;
using System.Threading;
using tungsten.core.Hardware;

namespace tungsten.core
{
    public static class Wait
    {
        public static bool Until(Func<bool> predicate)
        {
            return Until(predicate, HardwareConfiguration.MaxRetryTime);
        }

        public static bool Until(Func<bool> predicate, TimeSpan maxRetryTime)
        {
            var sleepTime = TimeSpan.FromMilliseconds(10);
            DateTime retryUntil = DateTime.Now + maxRetryTime;
            while (DateTime.Now < retryUntil)
            {
                if (predicate())
                {
                    return true;
                }

                Thread.Sleep(sleepTime);
            }

            return false;
        }

        public static TRet UntilNotNull<TRet>(Func<TRet> func)
            where TRet : class
        {
            return UntilNotNull(func, HardwareConfiguration.MaxRetryTime);
        }

        public static TRet UntilNotNull<TRet>(Func<TRet> func, TimeSpan maxRetryTime)
            where TRet : class
        {
            var sleepTime = TimeSpan.FromMilliseconds(10);
            DateTime retryUntil = DateTime.Now + maxRetryTime;
            while (DateTime.Now < retryUntil)
            {
                var found = func();
                if (found != null)
                {
                    return found;
                }

                Thread.Sleep(sleepTime);
            }

            return null;
        }
    }
}