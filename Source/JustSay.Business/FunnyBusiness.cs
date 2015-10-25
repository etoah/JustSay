using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Justsay.Models;
using JustSay.Common.DotNetImage;
using JustSay.Common.DotNetWeb;
using JustSay.Config;

namespace JustSay.Business
{
    public class FunnyBusiness : BaseBusiness<Funny>,IFunnyBusiness
    {
        public FunnyBusiness(DbContext db)
            : base(db)
        {

        }
        public override Funny Add(Funny entity)
        {
            if (HotConfig.IsReviewFunny == 1)
                entity.Status = 0;
            else
                entity.Status = 3;
            Member userInfo = MemberBusiness.GetUserInfo();
            entity.ShowName = userInfo.ShowName;
            entity.MemberID = userInfo.ID;
            entity.Time = DateTime.Now;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpPostedFile img = HttpContext.Current.Request.Files[0];
                entity.ImgUrl = ImageProccess.CompressImageProccess(img.InputStream, HttpContext.Current.Server.MapPath("~"), "www.justsay.cn|爱情点滴");
            }
            entity.Time = DateTime.Now;
            IMemberBusiness iMember = new MemberBusiness(db);
            iMember.AwardScore(10);
            return base.Add(entity);
        }

        #region IFunnyBusiness 成员

        public int Up(int id)
        {


            string IPID = JustSay.Common.DotNetEncrypt.Md5Helper.MD5(HttpContext.Current.User.Identity.Name + id.ToString(), 16);

            if (!CookieHelper.IsExistCookie(JustSay.Config.EncryptConfig.FunnyUpCookieName, IPID))
            {
                CookieHelper.Add(JustSay.Config.EncryptConfig.FunnyUpCookieName, IPID, "1", DateTime.Now.AddDays(1));
                 Funny funny= GetDetail(f=>f.ID==id);
                 funny.Up += 1;
                 db.SaveChanges();
                return funny.Up;
            }
            else
            {
                return -1;
            }
           
        }

        public void Review(int id,bool IsShow)
        {
             Funny funny= GetDetail(f=>f.ID==id);
                 funny.Status = 3;

                 if (IsShow)
                 {
                     funny.Status = 3; //通过
                 }
                 else
                 {
                     funny.Status = 2;//未通过
                 }
                 db.SaveChanges();

        }


        #endregion

        enum FunnyStatus
        {
            New=0,
            Failed=2,
            Pass=3,
        }
    }

    

}
