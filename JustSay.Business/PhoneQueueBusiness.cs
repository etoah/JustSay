using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public class EmailQueueBusiness : BaseBusiness<EmailQueue>, IEmailQueueBusiness
    {
        public EmailQueueBusiness(DbContext db)
            : base(db)
        {

        }
    }

}
