using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustSay.Common.DotNetCode;
using JustSay.Common.DotNetEncrypt;

namespace JustSay.Common.Extension
{
    public static class Extension
    {
        #region String扩展方法

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string DefaultValue(this string s,string defaultValue)
        {
            return s.IsNullOrEmpty() ? defaultValue : s;
        }

        public static int ToInt(this string s)
        {
            //网上原代码
            // return int.Parse(s);

            // 换成更高效的方法
            int num1;
            if (s.IsNullOrEmpty() || !Int32.TryParse(s, out num1))
            {
                return -1;
            }
            return num1;
        }


        

        /// <summary>
        /// 加密uid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string UidEncrypt(this string uid)
        {
            return DESEncrypt.Encrypt(uid.ToString(), "Sedsfn");
        }

        /// <summary>
        /// 加密uid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string UidEncrypt(this int uid)
        {
            return DESEncrypt.Encrypt(uid.ToString(), "Sweeadsfuan" + DateTime.Now.Day);
        }
        /// <summary>
        /// 解密uid
        /// </summary>
        /// <param name="uidEncryptString"></param>
        /// <returns></returns>
        public static int UidDescrypt(this string uidEncryptString)
        {
            return Convert.ToInt32(DESEncrypt.Decrypt(uidEncryptString, "Sweeadsfuan" + DateTime.Now.Day));
        }

        public static string SubstringLength(this string str, int length)
        {
            return str.Length < length ? str : str.Substring(0, length) + "... ...";
        }

        public static string HideEmail(this string str)
        {
            return StringHelper.HideEmail(str);
        }

        public static string HidePhone(this string str)
        {
            return StringHelper.HidePhone(str);
        }



        #endregion
        public static void WriteLog(this Exception ex, string logClass, string className)
        {

            LogHelper Logger = new LogHelper(logClass);
            string msg = string.Format(
                "\r\n类：{0};\r\n异常信息：{1}\r\n", "CacheHelper", ex.Message

                );
            Logger.WriteLog(msg);
        }




    }
}
