using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Justsay.Models;

namespace JustSay.Business
{
    public interface 
        IMemberBusiness:IBusiness<Member>
    {
        bool ValidateLogon(string email, string password);
        bool PersistentLogon(Member member,int days);
        int AwardScore(int score);
        bool PromoteSenior();
        bool IsLimitPost(int id);
        void UpdataPostTime();
        //Member GetUserInfoFromCookie();

    }
}
