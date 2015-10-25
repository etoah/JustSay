using System.Text;
using System.Text.RegularExpressions;

namespace JustSay.Common.DotNetCode
{
    public class StringHelper
    {
        public static string WriteString(object obj, string curValue)
        {
            string result;
            try
            {
                string val = obj.ToString();
                if (val == curValue)
                {
                    result = "selected";
                }
                else
                {
                    result = "";
                }
            }
            catch
            {
                result = "";
            }
            return result;
        }

        public static int GetLength(string str)
        {
            int result;
            if (str.Length == 0)
            {
                result = 0;
            }
            else
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                int tempLen = 0;
                byte[] s = ascii.GetBytes(str);
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == 63)
                    {
                        tempLen += 2;
                    }
                    else
                    {
                        tempLen++;
                    }
                }
                result = tempLen;
            }
            return result;
        }

        public static string GetCenterShow(string p, int size, int font)
        {
            string result;
            if (font == 2)
            {
                int s = 0;
                int len = p.Length;
                if (len >= 11)
                {
                    result = p;
                }
                else
                {
                    if (len == 9)
                    {
                        s = 2;
                    }
                    if (len == 8)
                    {
                        s = 2;
                    }
                    if (len == 7)
                    {
                        s = 3;
                    }
                    if (len == 6)
                    {
                        s = 4;
                    }
                    if (len == 5)
                    {
                        s = 4;
                    }
                    if (len == 4)
                    {
                        s = 5;
                    }
                    if (len == 3)
                    {
                        s = 6;
                    }
                    if (len == 2)
                    {
                        s = 7;
                    }
                    string ttt = "";
                    ttt = ttt.PadLeft(s, ' ');
                    result = ttt + p;
                }
            }
            else
            {
                if (font == 1)
                {
                    int len = StringHelper.GetLength(p);
                    int tmp = 34 - len;
                    if (tmp < 0)
                    {
                        result = p;
                    }
                    else
                    {
                        double index = (double)tmp / 2.0;
                        string ttt = "";
                        int s = (int)index;
                        ttt = ttt.PadLeft(s, ' ');
                        result = ttt + p;
                    }
                }
                else
                {
                    result = p;
                }
            }
            return result;
        }
        /// <summary>
        /// 格式化TextArea字符串，转化为html格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FormatTextArea(string s)
        {
            s = s.Replace(" ", "&nbsp;");
            s = s.Replace("\n", "<br />");
            
            return s;
        }
        /// <summary>
        /// 去除Html标签,并截取长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveHtmlTag(string s,int length)
        {

            s = s.Replace("&nbsp;", "");
            s = s.Replace("<br />", "");
            if (length >= 0)
            {
                if (s.Length > length)
                    s = s.Substring(0, length) + "……";
            }
            return s;
        }


        /// <summary>
        /// 隐藏Email
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HideEmail(string  s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            int index = s.LastIndexOf('@');
            char[] ar= s.ToCharArray();
            for (int i = 2; i < index - 1; i++)
            {
                ar[i] = '*';
            }
            return new string(ar);
        }
        /// <summary>
        /// 隐藏phone
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HidePhone(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            char[] ar = s.ToCharArray();
            for (int i = 3; i < s.Length - 4; i++)
            {
                ar[i] = '*';
            }
            return new string(ar);
        }


        public static string GetOmitString(string str, int length, string status)
        {
            string result;
            if (status == "1")
            {
                int i = 0;
                int j = 0;
                for (int k = 0; k < str.Length; k++)
                {
                    if (Regex.IsMatch(str.Substring(k, 1), "[\\u4e00-\\u9fa5]+"))
                    {
                        i += 2;
                    }
                    else
                    {
                        i++;
                    }
                    if (i <= length)
                    {
                        j++;
                    }
                    if (i >= length)
                    {
                        result = str.Substring(0, j) + "...";
                        return result;
                    }
                }
            }
            result = str;
            return result;
        }
    }
}