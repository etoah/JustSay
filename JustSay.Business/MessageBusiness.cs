using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;
using JustSay.Config;

namespace JustSay.Business
{
    public class MessageBusiness : BaseBusiness<Message>, IMessageBusiness
    {
        public MessageBusiness(DbContext db)
            : base(db)
        {

        }
        public override Message Add(Message entity)
        {
            Member userInfo = MemberBusiness.GetUserInfo();
            entity.FromName = userInfo.ShowName;
            entity.FromID = userInfo.ID;
            entity.Time = DateTime.Now;
            IMemberBusiness iMember = new MemberBusiness(db);
            if (HotConfig.IsControlMessage==1)
            {
                iMember.UpdataPostTime();
            }
            //To
            entity.Member1 = iMember.GetDetail(entity.ToID);
            entity.Member1.MessageCount += 1;
            entity.IsNew = true;
            return base.Add(entity);
        }
        public override Message GetDetail(int id)
        {
            Message message = base.GetDetail(id);
            message.IsNew = false;
            base.Submit();
            return message;
        }

        public void Reading(int id)
        {
            Message message = base.GetDetail(id);
            message.IsNew = false;
            base.Submit();
        }
    }

}
