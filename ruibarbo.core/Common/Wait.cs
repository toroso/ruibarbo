using System;
using System.Linq.Expressions;
using System.Threading;

namespace ruibarbo.core.Common
{
    public static class Wait
    {
        public static bool Until(Expression<Func<bool>> predicateExp)
        {
            return Until(predicateExp, Configuration.MaxRetryTime);
        }

        public static bool Until(Expression<Func<bool>> predicateExp, TimeSpan maxRetryTime)
        {
            var predicate = predicateExp.Compile();
            var startTime = DateTime.Now;
            var sleepTime = TimeSpan.FromMilliseconds(10);
            DateTime retryUntil = startTime + maxRetryTime;
            while (DateTime.Now < retryUntil)
            {
                if (predicate())
                {
                    //Console.WriteLine("Waited for '{0}' for {1} ms", predicateExp.Body, (DateTime.Now - startTime).TotalMilliseconds);
                    return true;
                }

                Thread.Sleep(sleepTime);
            }

            return false;
        }

        public static TRet UntilNotNull<TRet>(Expression<Func<TRet>> funcExp)
            where TRet : class
        {
            return UntilNotNull(funcExp, Configuration.MaxRetryTime);
        }

        public static TRet UntilNotNull<TRet>(Expression<Func<TRet>> funcExp, TimeSpan maxRetryTime)
            where TRet : class
        {
            var func = funcExp.Compile();
            var startTime = DateTime.Now;
            var sleepTime = TimeSpan.FromMilliseconds(10);
            DateTime retryUntil = startTime + maxRetryTime;
            while (DateTime.Now < retryUntil)
            {
                var found = func();
                if (found != null)
                {
                    //Console.WriteLine("Waited for '{0}' != null for {1} ms", funcExp.Body, (DateTime.Now - startTime).TotalMilliseconds);
                    return found;
                }

                Thread.Sleep(sleepTime);
            }

            return null;
        }
    }
}