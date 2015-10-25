using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public interface IConfessBusiness : IBusiness<Confess>
    {
        int Up(int id);
      //  bool Inform(Confess confess, string Url);
    }
}
