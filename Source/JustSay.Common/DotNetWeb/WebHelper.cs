using JustSay.Common.DotNetEncrypt;
using System;
using System.Collections;
using System.Text;
using System.Web;

namespace JustSay.Common.DotNetWeb
{
    public class WebHelper
    {


        public static bool SubmitCheckForm()
        {
            bool result;
            if (HttpContext.Current.Request.Form.Get("txt_hiddenToken").Equals(WebHelper.GetToken()))
            {
                WebHelper.SetToken();
                result = true;
            }
            else
            {
                ShowMsgHelper.showWarningMsg("为了保证表单不重复提交，提交无效");
                result = false;
            }
            return result;
        }

        public static string GetToken()
        {
            HttpContext rq = HttpContext.Current;
            string result;
            if (null != rq.Session["Token"])
            {
                result = rq.Session["Token"].ToString();
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }

        public static void SetToken()
        {
            HttpContext rq = HttpContext.Current;
            rq.Session.Add("Token", Md5Helper.MD5(rq.Session.SessionID + DateTime.Now.Ticks.ToString(), 32));
        }

        public string InsertSql(string tableName, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(tableName);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                sb_prame.Append("," + key);
                sp.Append(",:" + key);
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb.ToString();
        }
    }
}