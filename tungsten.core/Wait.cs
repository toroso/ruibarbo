using System;
using System.Threading;

namespace tungsten.core
{
    public static class Wait
    {
        public static bool Until(Func<bool> found, TimeSpan maxRetryTime)
        {
            var sleepTime = TimeSpan.FromMilliseconds(10);
            DateTime retryUntil = DateTime.Now + maxRetryTime;
            while (DateTime.Now < retryUntil)
            {
                if (found())
                {
                    return true;
                }

                Thread.Sleep(sleepTime);
            }

            return false;
        }
    }
}