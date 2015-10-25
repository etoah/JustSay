using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public class ActionLogBusiness : BaseBusiness<ActionLog>, IActionLogBusiness
    {
        public ActionLogBusiness(DbContext db)
            : base(db)
        {

        }

    }

}
