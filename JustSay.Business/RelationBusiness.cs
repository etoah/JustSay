using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public class RelationBusiness : BaseBusiness<Relation>, IRelationBusiness
    {
        public RelationBusiness(DbContext db)
            : base(db)
        {

        }
        public override Relation Add(Relation entity)
        {
            if (string.IsNullOrEmpty(entity.ToPhone))
                entity.ToPhone = "";
            if (string.IsNullOrEmpty(entity.FromPhone))
                entity.FromPhone = "";
            Member userInfo = MemberBusiness.GetUserInfo();
            entity.ShowName = userInfo.ShowName;
            entity.MemberID = userInfo.ID;
            entity.Time = DateTime.Now;
            return base.Add(entity);
        }

        #region IRelationBusiness 成员

        public bool IsLoveEachOther(string fromName, string toName)
        {
            return LoadEntities().Any(r => r.ToName == fromName&&r.FromName==toName);
        }

        public IQueryable<Relation> GetFans(string toName)
        {
            return LoadEntities().Where(r=>r.ToName==toName).AsQueryable();
        }
        public bool Inform(Relation relation, string Url)
        {
            return false;
        }

        #endregion
    }

}
