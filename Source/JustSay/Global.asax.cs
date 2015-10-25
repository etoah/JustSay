using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace JustSay
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //pageindex为分页，id为其它参数  1、热点的天数或小时数
            routes.MapRoute(
                 "TwoPara", // 路由名称
                "{controller}/{action}/{id}/{pageindex}/",// 带有参数的 URL
                new { pageindex = UrlParameter.Optional }
               // new { id=@"\d+"}
            );
            //id为id或pageindex
            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}/", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
                
            );
            //str  密码找回时用
            routes.MapRoute(
                "code", // 路由名称
                "{controller}/{action}/{code}/" // 带有参数的 URL
            );
            //routes.MapRoute(
            //    "Message", // 路由名称
            //    "{controller}/{action}/{idstr}/{name}/" // 带有参数的 URL
            //);

        }

        protected void Application_Start()
        {
          //  AreaRegistration.RegisterAllAreas();

            // 默认情况下对 Entity Framework 使用 LocalDB
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        //取得对象
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string userData = ticket.UserData;
                        string[] roles = userData.Split(',');
                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                    }

                }
            }
        }
    }
}