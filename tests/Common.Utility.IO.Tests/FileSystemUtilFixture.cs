using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;

namespace Common.Utility.IO.Tests
{
    [TestClass]
    public class FileSystemUtilFixture
    {
        [ClassCleanup]
        public static void ClassCleanup()
        {
            FileSystemUtil.DeleteFiles(@"C:\temp\", "fs-test-*.txt");
        }

        [TestMethod]
        public void ShouldWaitUntilIsFileIsReadable()
        {
            var fileName = @"C:\temp\fs-test-" + DateTime.Now.ToString("yyMMdd-HHmmssfff") + ".txt";

            ThreadPool.QueueUserWorkItem(_ =>
            {
                using (var sw = new StreamWriter(fileName))
                {
                    Thread.Sleep(500);
                }
            });

            var result = FileSystemUtil.WaitUntilIsFileIsReadable(fileName, 7);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShouldFailWaitUntilIsFileIsReadable()
        {
            var fileName = @"C:\temp\fs-test-" + DateTime.Now.ToString("yyMMdd-HHmmssfff") + ".txt";
            try
            {
                using (var sw = new StreamWriter(fileName))
                {
                    FileSystemUtil.WaitUntilIsFileIsReadable(fileName, 2);
                }
            }
            catch (IOException ex)
            {
                var expected = string.Format("{0} is still being used by another process after {1} retries with {2}ms interval", fileName, 2, 100);
                Assert.AreEqual(expected, ex.Message);
                return;
            }

            Assert.Fail("IOException must be thrown");
        }

        [TestMethod]
        public void ShouldDeleteFiles()
        {
            for (var i = 0; i < 10; i++)
            {
                File.WriteAllText(@"C:\temp\fs-del-" + i + ".txt", "blah blah blah");
            }

            FileSystemUtil.DeleteFiles(@"C:\temp\", "fs-del-*.txt");

            var files = Directory.GetFiles(@"C:\temp\", "fs-del-*.txt");
            Assert.AreEqual(0, files.Length);
        }
    }
}
