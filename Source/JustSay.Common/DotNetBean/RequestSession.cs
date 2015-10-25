using System.Web;

namespace JustSay.Common.DotNetBean
{
    public class RequestSession
    {
        private static string SESSION_USER = "SESSION_USER";

        public static void AddSessionUser(SessionUser user)
        {
            HttpContext rq = HttpContext.Current;
            rq.Session[RequestSession.SESSION_USER] = user;
        }

        public static SessionUser GetSessionUser()
        {
            HttpContext rq = HttpContext.Current;
            return (SessionUser)rq.Session[RequestSession.SESSION_USER];
        }
    }
}