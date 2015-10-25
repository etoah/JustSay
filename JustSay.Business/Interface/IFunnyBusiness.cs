using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public interface IFunnyBusiness : IBusiness<Funny>
    {
        int Up(int id);
        void Review(int id, bool IsShow);
    }
}
