using System.Runtime.InteropServices;
using System.Web;

namespace JustSay.Common.DotNetCode
{
    public class RequestHelper
    {
        public static string GetScriptNameQueryString
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
        }

        public static string GetScriptName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
        }

        public static string GetScriptUrl
        {
            get
            {
                return (RequestHelper.GetScriptNameQueryString == "") ? RequestHelper.GetScriptName : string.Format("{0}?{1}", RequestHelper.GetScriptName, RequestHelper.GetScriptNameQueryString);
            }
        }

        public static string GetScriptNameQuery
        {
            get
            {
                return HttpContext.Current.Request.Url.Query;
            }
        }

        [DllImport("wininet")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        public static bool IsConnectedInternet()
        {
            int i = 0;
            return RequestHelper.InternetGetConnectedState(out i, 0);
        }

        public static string UrlEncode(string str)
        {
            string result;
            if (string.IsNullOrEmpty(str))
            {
                result = "";
            }
            else
            {
                str = str.Replace("'", "");
                result = HttpContext.Current.Server.UrlEncode(str);
            }
            return result;
        }

        public static string UrlDecode(string str)
        {
            string result;
            if (string.IsNullOrEmpty(str))
            {
                result = "";
            }
            else
            {
                result = HttpContext.Current.Server.UrlDecode(str);
            }
            return result;
        }

        public static string GetIP()
        {
            string result = string.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            string result2;
            if (string.IsNullOrEmpty(result))
            {
                result2 = "127.0.0.1";
            }
            else
            {
                result2 = result;
            }
            return result2;
        }

        public static string buildurl(string url, string param)
        {
            string result;
            if (url.IndexOf(param) > 0)
            {
                string url2;
                if (url.IndexOf("&", url.IndexOf(param) + param.Length) > 0)
                {
                    url2 = url.Substring(0, url.IndexOf(param) - 1) + url.Substring(url.IndexOf("&", url.IndexOf(param) + param.Length) + 1);
                }
                else
                {
                    url2 = url.Substring(0, url.IndexOf(param) - 1);
                }
                result = url2;
            }
            else
            {
                result = url;
            }
            return result;
        }

        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        public static string GetServerString(string strName)
        {
            string result;
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                result = "";
            }
            else
            {
                result = HttpContext.Current.Request.ServerVariables[strName].ToString();
            }
            return result;
        }

        public static string GetUrlReferrer()
        {
            string retVal = null;
            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch
            {
            }
            string result;
            if (retVal == null)
            {
                result = "";
            }
            else
            {
                result = retVal;
            }
            return result;
        }

        public static string GetCurrentFullHost()
        {
            HttpRequest request = HttpContext.Current.Request;
            string result;
            if (!request.Url.IsDefaultPort)
            {
                result = string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            else
            {
                result = request.Url.Host;
            }
            return result;
        }

        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        public static string GetDnsSafeHost()
        {
            return HttpContext.Current.Request.Url.DnsSafeHost;
        }

        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        public static bool IsBrowserGet()
        {
            string[] BrowserName = new string[]
			{
				"ie",
				"opera",
				"netscape",
				"mozilla",
				"konqueror",
				"firefox"
			};
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            bool result;
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        public static bool IsSearchEnginesGet()
        {
            bool result;
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                result = false;
            }
            else
            {
                string[] SearchEngine = new string[]
				{
					"google",
					"yahoo",
					"msn",
					"baidu",
					"sogou",
					"sohu",
					"sina",
					"163",
					"lycos",
					"tom",
					"yisou",
					"iask",
					"soso",
					"gougou",
					"zhongsou"
				};
                string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                for (int i = 0; i < SearchEngine.Length; i++)
                {
                    if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            return result;
        }

        /// <summary>
        /// µÃµ½url
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[]
			{
				'/'
			});
            return urlArr[urlArr.Length - 1].ToLower();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }
    }
}