using System;
using System.Threading;

namespace Common.Utility.Threading
{
    public static class ThreadingUtil
    {
        public static void SafeJoin(Thread thread)
        {
            SafeJoin(thread, 2000);
        }

        public static void SafeJoin(Thread thread, int timeout)
        {
            if (thread != null && thread.ThreadState != System.Threading.ThreadState.Running)
            {
                if (!thread.Join(timeout))
                {
                    try
                    {
                        thread.Abort();
                    }
                    catch (ThreadAbortException)
                    {
                        Thread.ResetAbort();
                    }
                }
            }
        }

        public static Thread CreateBackgroundThread(string threadName, Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var thread = new Thread(_ => action());
            thread.IsBackground = true;
            thread.Name = threadName;

            return thread;
        }

        public static Thread CreateForegroundThread(string threadName, Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var thread = new Thread(_ => action());
            thread.Name = threadName;

            return thread;
        }

        public static Thread RunAsBackgroundThread(string threadName, Action action)
        {
            var thread = CreateBackgroundThread(threadName, action);
            thread.Start();

            return thread;
        }

        public static Thread RunAsForegroundThread(string threadName, Action action)
        {
            var thread = CreateForegroundThread(threadName, action);
            thread.Start();

            return thread;
        }
    }
}
