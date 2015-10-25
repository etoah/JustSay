using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using JustSay.Common.DotNetCode;
using JustSay.Common.DotNetWeb;

namespace JustSay.Common.DotNetEmail
{
     public class SMSHelper
    {
       public const string SMSAPI = @"http://86api.com/sms/SmsHttpPort.aspx?Action=SendMessage&UserId={0}&UserPwd={1}&SendPhone={2}&SendMessage={3}&SendPort=81";

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="phone">电话号码，可以发给多个号码，用逗号隔开</param>
        /// <param name="msg">信息内容，58个字符以内</param>
        /// <returns></returns>
       public static SmsStatus SendMsg(string phone, string msg)
        {
            if (msg.Length > 58)
                msg = msg.Substring(0, 58);
            msg += @"【就说】";
            msg = HttpContext.Current.Server.UrlEncode(msg);
            string url = string.Format(SMSAPI, "justsay", "justsay@phone", phone, msg);
            HttpWebResponse nRps;
            try
            {
               nRps = HttpRequestHelper.CreateGetHttpResponse(url, 10000, null, null);
            }
            catch
            {
                return new SmsStatus("0,999");
            }
           StreamReader reader = new StreamReader(nRps.GetResponseStream(),Encoding.UTF8);
           string content = reader.ReadToEnd();
            return new SmsStatus(content);

        }



    }

     public class SmsStatus
     {

         public override string ToString()
         {
             return "IsSuccess:" + IsSuccess.ToString() + "<br />Surplus:" + Surplus + "<br />SuccessCount:" + SuccessCount + "<br />ErrorMessage:" + ErrorMessage;
         }


         public SmsStatus(string returnmsg)
         {

             string[] arr = returnmsg.Split(',');
             if (arr.Length == 3)
             {
                 IsSuccess = true;
                 Surplus = Convert.ToInt32(arr[1]);
                 successCount = Convert.ToInt32(arr[2]);
             }
             else if (arr.Length == 2)
             {
                 IsSuccess = true;
                 Surplus = Convert.ToInt32(arr[1]);
             }


         }
         public bool IsSuccess
         {
             set;
             get;
         }

         public int successCount;

         public int SuccessCount
         {
             set
             {
                 successCount = value;
             }
             get
             {
                 return successCount;
             }
         }

         private int surplus;

         /// <summary>
         /// 剩余数
         /// </summary>
         public int Surplus
         {
             set
             {

                 if (surplus < 20)
                 {
#warning 当余额少于20条时通知我
                 }
                 surplus = value;
             }
             get
             {
                 return surplus;
             }
         }
         public string ErrorMessage
         {
             get
             {
                 if (surplus >= 0)
                     return "短信发送成功";
                 else
                 {
                     LogHelper.WriteLog("发送失败，错误码" + surplus, "短信发送异常", "SMSHelper");
                     return "发送失败，错误码" + surplus;
                 }
             }
         }

     }
}
