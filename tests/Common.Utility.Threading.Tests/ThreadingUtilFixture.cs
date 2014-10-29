using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Common.Utility.Threading.Tests
{
    /// <summary>
    /// Summary description for ThreadingUtilFixture
    /// </summary>
    [TestClass]
    public class ThreadingUtilFixture
    {
        [TestMethod]
        public void ShouldCreateForegroundThread()
        {
            var thread = ThreadingUtil.CreateForegroundThread("ForegroundThread", () => { });

            Assert.AreEqual("ForegroundThread", thread.Name);
            Assert.AreEqual(ThreadState.Unstarted, thread.ThreadState);
            Assert.IsFalse(thread.IsBackground);
            Assert.IsFalse(thread.IsThreadPoolThread);
            Assert.IsFalse(thread.IsAlive);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotCreateForegroundThread()
        {
            var thread = ThreadingUtil.CreateForegroundThread("ForegroundThread", null);
        }

        [TestMethod]
        public void ShouldCreateBackgroundThread()
        {
            var thread = ThreadingUtil.CreateBackgroundThread("BackgroundThread", () => { });

            Assert.AreEqual("BackgroundThread", thread.Name);
            Assert.AreEqual(ThreadState.Background | ThreadState.Unstarted, thread.ThreadState);
            Assert.IsTrue(thread.IsBackground);
            Assert.IsFalse(thread.IsThreadPoolThread);
            Assert.IsFalse(thread.IsAlive);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotCreateBackgroundThread()
        {
            var thread = ThreadingUtil.CreateBackgroundThread("BackgroundThread", null);
        }

        [TestMethod]
        public void ShouldRunAsForegroundThread()
        {
            var text = "";
            var thread = ThreadingUtil.RunAsForegroundThread("ForegroundThread", () => { text = "I've set it!"; Thread.Sleep(1000); });

            Assert.AreEqual("ForegroundThread", thread.Name);
            Assert.IsFalse(thread.IsBackground);
            Assert.IsFalse(thread.IsThreadPoolThread);
            Assert.IsTrue(thread.IsAlive);

            Thread.Sleep(300);
            ThreadingUtil.SafeJoin(thread);

            Assert.AreEqual("I've set it!", text);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotRunAsForegroundThread()
        {
            var thread = ThreadingUtil.RunAsForegroundThread("ForegroundThread", null);
        }

        [TestMethod]
        public void ShouldRunAsBackgroundThread()
        {
            var text = "";
            var thread = ThreadingUtil.RunAsBackgroundThread("BackgroundThread", () => { text = "I've set it!"; Thread.Sleep(1000); });

            Assert.AreEqual("BackgroundThread", thread.Name);
            Assert.IsTrue(thread.IsBackground);
            Assert.IsFalse(thread.IsThreadPoolThread);
            Assert.IsTrue(thread.IsAlive);

            Thread.Sleep(300);
            ThreadingUtil.SafeJoin(thread);

            Assert.AreEqual("I've set it!", text);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldNotRunAsBackgroundThread()
        {
            var thread = ThreadingUtil.RunAsBackgroundThread("BackgroundThread", null);
        }

        [TestMethod, Ignore]
        public void ShouldAbortBackgroundThread()
        {
            var thread = ThreadingUtil.RunAsBackgroundThread("BackgroundThread", () => { Thread.Sleep(1000); });

            Thread.Sleep(200);
            ThreadingUtil.SafeJoin(thread, 300);

            Assert.IsTrue(thread.ThreadState == ThreadState.Stopped || thread.ThreadState == (ThreadState.Background | ThreadState.AbortRequested));
        }
    }
}
