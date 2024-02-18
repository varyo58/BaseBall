using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BaseBall
{
    /// <summary>
    /// ログクラス
    /// https://qiita.com/miku_minatsuki/items/e4aa142ee7e2ba371169
    /// </summary>
    public class BaseBallLogger
    {

        private static BaseBallLogger singletonInstance = null;
        private StreamWriter stream = null;
        private readonly bool consoleOut;

        public static BaseBallLogger GetInstance(string logFilePath, bool consoleOut = false)
        {
            if (singletonInstance == null)
            {
                singletonInstance = new BaseBallLogger(logFilePath, consoleOut);
            }
            return singletonInstance;
        }

        public static void Init(string logFilePath, bool consoleOut = false)
        {
            singletonInstance = new BaseBallLogger(logFilePath, consoleOut);
        }

        private BaseBallLogger(string logFilePath, bool consoleOut)
        {
            if (string.IsNullOrWhiteSpace(logFilePath))
            {
                throw new Exception("logFilePath is empty.");
            }

            var logFile = new FileInfo(logFilePath);
            if (!Directory.Exists(logFile.DirectoryName))
            {
                Directory.CreateDirectory(logFile.DirectoryName);
            }

            stream = new StreamWriter(logFile.FullName, true, Encoding.Default);
            stream.AutoFlush = true;
            this.consoleOut = consoleOut;
        }

        public void write( string text)
        {
            //string log = string.Format(LOG_FORMAT, DateTime.Now.ToString(DATETIME_FORMAT), level.ToString(), text);
            stream.WriteLine(text);
            if (consoleOut)
            {
                Debug.WriteLine(text);
                //Console.WriteLine(log);
            }
        }
    }
}
