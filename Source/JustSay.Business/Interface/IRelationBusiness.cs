using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public interface IRelationBusiness : IBusiness<Relation>
    {
        bool IsLoveEachOther(string fromName, string toName);
        IQueryable<Relation> GetFans(string toName);
        bool Inform(Relation relation, string Url);
    }
}
