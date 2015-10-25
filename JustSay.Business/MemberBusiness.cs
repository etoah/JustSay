using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;
using System.Web.Security;
using System.Web;
using JustSay.Common.DotNetWeb;
using JustSay.Common.DotNetEncrypt;

namespace JustSay.Business
{
    public class MemberBusiness : BaseBusiness<Member>, IMemberBusiness
    {
        public MemberBusiness(DbContext db)
            : base(db)
        {

        }

        
        #region IMemberBusiness 成员

        public override Member Add(Member entity)
        {
            entity.LastPostTime=entity.JoinDate= DateTime.Now;
            if (string.IsNullOrEmpty(entity.ShowName))
                entity.ShowName = "佚名";
            entity.Password = Md5Helper.MD5(entity.Password, 32);
            return base.Add(entity);
        }

        public bool ValidateLogon(string email, string password)
        {
            password=Md5Helper.MD5(password,32);
            try
            {
                Member member = LoadEntities(b => b.Email == email && b.Password == password).First();

                if (member != null)
                {
                    PersistentLogon(member, 7);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 持久化登录
        /// </summary>
        /// <param name="member"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public bool PersistentLogon(Member member,int days)
        {
            //加入持久化的代码
            try
            {

                //当为非管理员帐户时
                if (member.RoleID > 3)
                {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, member.ID.ToString(), DateTime.Now, DateTime.Now.AddDays(days), true, member.Role.RoleName);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    cookie.Expires = DateTime.Now.AddDays(days);
                    cookie.HttpOnly = true;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                else//当为管理员帐户时  设置为几小时
                {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, member.ID.ToString(), DateTime.Now, DateTime.Now.AddHours(3), member.RoleID > 3, member.Role.RoleName);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    cookie.Expires = DateTime.Now.AddHours(3);
                    cookie.HttpOnly = true;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                CookieHelper.AddUserInfo(member.Email, days, member.ID, member.ShowName);
                return true;
            }
            catch 
            {
                return false;
            }

        }
        /// <summary>
        /// 优先从cookie中取到数据
        /// </summary>
        /// <returns></returns>
        public static Member GetUserInfo()
        {
            string showname;//显示名
            string email;
            Member member = new Member();
            if ((member.ID = CookieHelper.VerifyUser(out showname, out email)) > 0)
            {
                member.Email = email;
                member.ShowName = showname;
            }
            else
            {
              IMemberBusiness iMember=new MemberBusiness(new JustSayEntities());
              member.ID=Convert.ToInt32(HttpContext.Current.User.Identity.Name);
              member=iMember.GetDetail(member.ID);                
            }
            return member;
        }   

        public int AwardScore(int score)
        {
            Member member= base.GetDetail(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            member.Score += score;
            member.LastPostTime = DateTime.Now;
            base.UpdateUnSubmit(member);
            return score;
        }

        public void UpdataPostTime()
        {
            Member member = base.GetDetail(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            member.LastPostTime = DateTime.Now;
            base.UpdateUnSubmit(member);
        }

        public bool PromoteSenior()
        {
            Member member = base.GetDetail(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            if (member.Score > 200)
            {
                member.RoleID = (int)RoleNo.Senior;
                base.Update(member);
                return true;
            }
            return false;
            
            
        }


        public bool IsLimitPost(int id)
        {
           return base.LoadEntities(m=>m.ID==id).Select(m=>m.LastPostTime).First().AddMinutes(Config.HotConfig.PostTimeLimit)>DateTime.Now;
        }

        #endregion
    }

    public enum RoleNo
    {
        Owner = 1,
        SuperAdmin = 2,
        AdminL = 3,
        Senior = 4,
        User = 5,
        LimitUser = 6
    }
}
