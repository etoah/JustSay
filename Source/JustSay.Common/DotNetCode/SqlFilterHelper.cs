using System.Text.RegularExpressions;

namespace JustSay.Common.DotNetCode
{
    /// <summary>
    /// 过滤敏感字符
    /// </summary>
    public class SqlFilterHelper
    {
        /// <summary>
        /// 过滤敏感字符
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string Filter(string inputString)
        {
            string result;
            if (inputString != "")
            {
                string sql = SqlFilterHelper.SqlFilters(inputString);
                if (sql == "")
                {
                    sql = "敏感字符";
                }
                result = sql;
            }
            else
            {
                result = inputString;
            }
            return result;
        }

        private static string SqlFilters(string source)
        {
            source = source.Replace("'", "'''").Replace(";", "；").Replace("(", "（").Replace(")", "）");
            source = Regex.Replace(source, "select", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "insert", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "update", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "delete", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "drop", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "truncate", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "declare", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "/add", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "net user", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "exec", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "execute", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "xp_", "x p_", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "sp_", "s p_", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "0x", "0 x", RegexOptions.IgnoreCase);
            return source;
        }
    }
}