using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustSay.Config
{
   public static class HotConfig
    {
         // public static string AdminCookieEncryptKey; // 主Cookie的管理员加密
        public static int DefaultFunnyHomeHotByDay;
        public static int DefaultConfessHomeHotByDay;
        public static int ConfessHotControlByDay;
        public static int IsReviewFunny;
        public static int PostTimeLimit;
        public static int IsControlMessage;
        public static int HomeHotFunnyDay;
        public static int HomeHotConfessDay;


        static HotConfig()
        {
            InitConfig();
        }
        //每增加一个,须要增加到此处
        public static void InitConfig()
        {
            HomeHotFunnyDay = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["HomeHotFunnyDay"]);
            HomeHotConfessDay = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["HomeHotConfessDay"]);
            PostTimeLimit = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostTimeLimit"]);
            IsControlMessage = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IsControlMessage"]);
            IsReviewFunny = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IsReviewFunny"]);
            DefaultFunnyHomeHotByDay =Convert.ToInt32( System.Configuration.ConfigurationManager.AppSettings["DefaultFunnyHomeHotByDay"]);
            DefaultConfessHomeHotByDay = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultConfessHomeHotByDay"]);
            ConfessHotControlByDay = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ConfessHotControlByDay"]);

        }
    }
}
