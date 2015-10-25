using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Justsay.Models;
using JustSay.Common.DotNetWeb;

namespace JustSay.Business
{
    public class CommentBusiness : BaseBusiness<Comment>,ICommentBusiness
    {
        public CommentBusiness(DbContext db): base(db){}
        public override Comment Add(Comment entity)
        {   entity.Time = DateTime.Now;
            Member userInfo = MemberBusiness.GetUserInfo();
            entity.ShowName = userInfo.ShowName;
            entity.MemberID = userInfo.ID;
            IFunnyBusiness iFunny = new FunnyBusiness(db);
            entity.Funny = iFunny.GetDetail(entity.FunnyID);
            entity.Funny.CommentCount += 1;
            
            IMemberBusiness iMember = new MemberBusiness(db);
            iMember.AwardScore(2);
            return base.Add(entity);
        }
        #region ICommentBusiness 成员

        public int Up(int id)
        {
            string IPID = JustSay.Common.DotNetEncrypt.Md5Helper.MD5(HttpContext.Current.User.Identity.Name + id.ToString(), 16);

            if (!CookieHelper.IsExistCookie(JustSay.Config.EncryptConfig.CommentUpCookieName, IPID))
            {
                CookieHelper.Add(JustSay.Config.EncryptConfig.CommentUpCookieName, IPID, "1", DateTime.Now.AddDays(1));
                Comment comment = GetDetail(f => f.ID == id);
                comment.Up += 1;
                db.SaveChanges();
                return comment.Up;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }

}
