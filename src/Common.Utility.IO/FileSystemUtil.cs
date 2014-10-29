using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Common.Utility.IO
{
    public static class FileSystemUtil
    {
        public static bool WaitUntilIsFileIsReadable(string filePath, int maxRetryCount)
        {
            return WaitUntilIsFileIsReadable(filePath, maxRetryCount, 100);
        }

        public static bool WaitUntilIsFileIsReadable(string filePath, int maxRetryCount, int retryInterval)
        {
            int counter = 0;

            while (counter < maxRetryCount)
            {
                FileStream fs = null;

                try
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                    return true;
                }
                catch (IOException)
                {
                }
                finally
                {
                    if (fs != null)
                        fs.Dispose();
                }

                Thread.Sleep(retryInterval);
                counter++;
            }

            var error = string.Format("{0} is still being used by another process after {1} retries with {2}ms interval", filePath, maxRetryCount, retryInterval);
            throw new IOException(error);
        }

        public static void DeleteFiles(string directory, string searchPattern)
        {
            var files = Directory.GetFiles(directory, searchPattern);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
