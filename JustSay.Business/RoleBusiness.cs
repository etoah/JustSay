using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public class RoleBusiness : BaseBusiness<Role>, IRoleBusiness
    {
        public RoleBusiness(DbContext db)
            : base(db)
        {

        }
    }

}
