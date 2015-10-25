using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustSay.Config
{
    public static class UIConfig
    {

       // public static string AdminCookieEncryptKey; // 主Cookie的管理员加密
        public static string HotDefaultImg;
        public static int ProfessPageSize;
        public static string WaterMark;

        
        static UIConfig()
        {
            InitConfig();
        }
        //每增加一个,须要增加到此处
        public static void InitConfig()
        {
            ProfessPageSize = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ProfessPageSize"]);
            HotDefaultImg = System.Configuration.ConfigurationManager.AppSettings["HotDefaultImg"];
            WaterMark = System.Configuration.ConfigurationManager.AppSettings["WaterMark"];
        }
    }
}
