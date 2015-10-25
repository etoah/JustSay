using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public interface IMessageBusiness : IBusiness<Message>
    {
        void Reading(int id);
    }
}
