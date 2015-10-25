using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Justsay.Models;
using JustSay.Business;
using JustSay.Common.DotNetWeb;

namespace JustSay.Filter
{
    public class ActionLogAttribute : ActionFilterAttribute
    {
        public string Description { get; set; }
        public ActionLogAttribute()
        { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
          //  Member member;
            using (IActionLogBusiness iLog=new ActionLogBusiness(new  JustSayEntities()) )
            {
                ActionLog log = new ActionLog
                {
                   // Member = member = db.Members.Where(p => p.Account == filterContext.HttpContext.User.Identity.Name).First(),
                    MemberID =Convert.ToInt16(filterContext.HttpContext.User.Identity.Name),
                    Action = filterContext.RouteData.Values["controller"] + "." + filterContext.RouteData.Values["action"],
                    ClientIP = filterContext.HttpContext.Request.UserHostAddress,
                    Description = this.Description,
                    CreateTime = DateTime.Now
                    // AdminReplyTime=DateTime.Now

                };
                iLog.Add(log);
                

            }
        }
    }
}