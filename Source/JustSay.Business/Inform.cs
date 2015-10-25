using Justsay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustSay.Common.DotNetEmail;
using System.Web.Routing;
using System.Web;

namespace JustSay.Business
{
    public class Inform
    {
        const string ConfessEmailTemplete = @"{0}<br />,你被人表白了,{1}<br /><br /><h1><a href='{2}'>查看表白</a></h1>";
        const string ConfessSMSTemplete = @"{0},你被人表白了,点击查看表白->{1}";
        const string RelationEmailTemplete = @"{0}和{1}相互喜欢，<h1><a href='{2}'>查看表白</a></h1>";
        const string RelationSMSTemplete = @"{0}和{1}相互喜欢,点击查看->{2}";
        const string FindPasswordTemplete = @"{0}，你好，你的邮件找回地址为<h2><a href='{1}'>点击穿越</a></h2>   若不能跳转，点击：<br /> {1}";
         public static bool Email(Confess confess)
        {
            string Url = string.Format("http://www.justsay.cn/confess/Details/{0}", confess.ID);
            string msg = SMTPManager.MailSending(confess.ToEmail,confess.ToName+"你被人表白了", string.Format(ConfessEmailTemplete,confess.ToName,confess.Content,Url), "", 0);

            if (msg != "发送成功")
            {
                ToDoListBusiness.WriteLog(msg, "邮箱异常", "SMTPManager.MailSending", true);
                return false;
            }
            return true;
            
        }
         public static bool SMS(Confess confess)
         {
             string Url = string.Format("http://www.justsay.cn/confess/Details/{0}", confess.ID);
             SmsStatus status = SMSHelper.SendMsg(confess.ToPhone, string.Format(ConfessSMSTemplete, confess.ToName, Url));
             if (!status.IsSuccess)
             {

                 ToDoListBusiness.WriteLog(status.ErrorMessage, "短信异常", "SMSHelper.SendMsg", true);
                 return false;
             }
             return status.IsSuccess;
           //  return true;
         }
         public static bool Email(Relation relation)
         {
             string Url = string.Format("http://www.justsay.cn/relation/Details/{0}", relation.ID);
             string msg = SMTPManager.MailSending(relation.ToEmail+";"+relation.FromEmail, "JustSay[相互喜欢通知邮件]", string.Format(RelationEmailTemplete, relation.ToName, relation.FromName, Url), "", 0);

             if (msg != "发送成功")
             {
                 ToDoListBusiness.WriteLog(msg, "邮箱异常", "SMTPManager.MailSending", true);
                 return false;
             }
             return true;
            
         }
         public static bool SMS(Relation relation)
         {
             string Url = string.Format("http://www.justsay.cn/relation/Details/{0}", relation.ID);
             SmsStatus status = SMSHelper.SendMsg(relation.ToPhone, string.Format(RelationSMSTemplete, relation.FromName, relation.ToName, Url));
             SmsStatus status2 = SMSHelper.SendMsg(relation.FromPhone, string.Format(RelationSMSTemplete, relation.FromName, relation.ToName, Url));
             if (!(status.IsSuccess && status2.IsSuccess))
             {
                 ToDoListBusiness.WriteLog(status.ErrorMessage, "短信异常", "SMSHelper.SendMsg", true);
                 return false;
             }

             return status.IsSuccess && status2.IsSuccess;

            // return true;
         }
         public static bool FindPassword(Member member)
         {

             string Url = string.Format("http://www.justsay.cn/account/NewPassword/{0}", member.Password);
             string msg = SMTPManager.MailSending(member.Email, "JustSay[找回密码邮件]", string.Format(FindPasswordTemplete, member.ShowName, Url), "", 0);

             if (msg != "发送成功")
             {
                 ToDoListBusiness.WriteLog(msg, "邮箱异常", "SMTPManager.MailSending", true);
                 return false;
             }
             return true;
         }


        public static string GetUrl()
        {
            return "";
        }
    }
}
