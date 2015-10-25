using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Justsay.Models;
using JustSay.Common.DotNetCode;
using JustSay.Common.DotNetEmail;
using JustSay.Common.DotNetImage;
using JustSay.Common.DotNetWeb;
using JustSay.Config;

namespace JustSay.Business
{
    public class ConfessBusiness : BaseBusiness<Confess>, IConfessBusiness
    {


        public ConfessBusiness(DbContext db)
            : base(db)
        {

        }
        public override Confess Add(Confess entity)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpPostedFile img = HttpContext.Current.Request.Files[0];
                entity.ImgUrl = ImageProccess.CompressImageProccess(img.InputStream, HttpContext.Current.Server.MapPath("~"), UIConfig.WaterMark);
            }
            Member userInfo = MemberBusiness.GetUserInfo();
            entity.ShowName = userInfo.ShowName;
            entity.MemberID = userInfo.ID;
            entity.Time = DateTime.Now;
            return base.Add(entity);
        }
        #region IConfessBusiness 成员

        public int Up(int id)
        {


            string IPID = JustSay.Common.DotNetEncrypt.Md5Helper.MD5(HttpContext.Current.User.Identity.Name + id.ToString(), 16);

            if (!CookieHelper.IsExistCookie(JustSay.Config.EncryptConfig.ConfessUpCookieName, IPID))
            {
                CookieHelper.Add(JustSay.Config.EncryptConfig.ConfessUpCookieName, IPID, "1", DateTime.Now.AddDays(1));
                Confess confess = GetDetail(f => f.ID == id);
                confess.Up += 1;
                base.Submit();
                return confess.Up;

            }
            else
            {
                return -1;
            }




        }


        public override Confess GetDetail(int id)
        {
            Confess confess = base.GetDetail(id);
            if (confess != null)
                confess.Click += 1;
            base.Submit();
            return confess;
        }
        #endregion
    }

}
