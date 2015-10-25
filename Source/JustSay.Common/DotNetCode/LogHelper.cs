using JustSay.Common.DotNetConfig;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.IO;

namespace JustSay.Common.DotNetCode
{
    public class LogHelper : IDisposable
    {
        private string LogFile;
        private static StreamWriter sw;
        private string logIsWrite = ConfigHelper.GetAppSettings("LogIsWrite");



        /// <summary>
        /// 写异常信息
        /// </summary>
        /// <param name="msg">异常</param>
        /// <param name="logClass">日志分类</param>
        /// <param name="className">发生错误的类名</param>
        public static void WriteLog(Exception ex ,string logClass,string className)
        {

            LogHelper Logger = new LogHelper(logClass);
            string msg = string.Format(
                "\r\n类：{0};\r\n异常信息：{1}\r\n", "CacheHelper", ex.Message

                );
            Logger.WriteLog(msg);
        }
        /// <summary>
        /// 写异常信息
        /// </summary>
        /// <param name="msg">异常信息</param>
        /// <param name="logClass">日志分类</param>
        /// <param name="className">发生错误的类名</param>
        public static void WriteLog(string Msg, string logClass, string className)
        {

            LogHelper Logger = new LogHelper(logClass);
            string msg = string.Format(
                "\r\n类：{0};\r\n异常信息：{1}\r\n", className, Msg

                );
            Logger.WriteLog(msg);
        }
 
        

        public LogHelper()
        {
            this.CreateLoggerFile(null);
        }


        public LogHelper(string _log)
        {
            this.CreateLoggerFile(_log);
        }

        private void CreateLoggerFile(string fileName)
        {
            if (this.logIsWrite == "true")
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = DateTimeHelper.GetToday();
                }
                object _myLogPath = null;
                if (_myLogPath == null)
                {
                    this.LogFile = ConfigHelper.GetAppSettings("LogFilePath");
                    if (!File.Exists(this.LogFile))
                    {
                        Directory.CreateDirectory(this.LogFile);
                    }
                }
                else
                {
                    this.LogFile = _myLogPath.ToString();
                }
                if (1 > this.LogFile.Length)
                {
                    Console.WriteLine("配置文件中没有指定日志文件目录！");
                }
                else
                {
                    if (!Directory.Exists(this.LogFile))
                    {
                        Console.WriteLine("配置文件中指定日志文件目录不存在！");
                    }
                    else
                    {
                        if (this.LogFile.Substring(this.LogFile.Length - 1, 1).Equals("/") || this.LogFile.Substring(this.LogFile.Length - 1, 1).Equals("\\"))
                        {
                            this.LogFile = this.LogFile + fileName + ".log";
                        }
                        else
                        {
                            this.LogFile = this.LogFile + "\\" + fileName + ".log";
                        }
                        try
                        {
                            FileStream fs = new FileStream(this.LogFile, FileMode.OpenOrCreate);
                            fs.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
            }
        }

        private void writeInfos(string messagestr)
        {
            //DateTime DateNow = default(DateTime);
            try
            {
                this.FileOpen();
                string DateStr = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
                string sWrite = DateStr + "\n" + messagestr;
                LogHelper.sw.WriteLine(sWrite);
                LogHelper.sw.Flush();
                LogHelper.sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void FileOpen()
        {
            LogHelper.sw = new StreamWriter(this.LogFile, true);
        }

        private void FileClose()
        {
            if (LogHelper.sw != null)
            {
                LogHelper.sw.Flush();
                LogHelper.sw.Close();
                LogHelper.sw.Dispose();
                LogHelper.sw = null;
            }
        }
        /// <summary>
        /// 写上日志信息
        /// </summary>
        /// <param name="msg">相关信息</param>
        public void WriteLog(string msg)
        {
            if (msg != null)
            {
               
                this.writeInfos(msg);
            }
        }

        public void Dispose()
        {
        }
    }


    public class nLog
    {
        //public nLog(string file,Exception ex)
        //{
        //    LogHelper Logger = new LogHelper(file);
        //    string msg = string.Format(
        //        "\r\n类：{0};\r\n异常信息：{1}\r\n", typeof().ToString(), ex.Message


        //        );
        //    Logger.WriteLog(msg);
        //}
    }
}