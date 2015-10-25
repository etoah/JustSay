using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustSay.Config
{
    public static class EncryptConfig
    {

        public static string AdminCookieEncryptKey; // 主Cookie的管理员加密
        public static string UserMainKey; // 用户信息Cookie名
        /// <summary>
        /// Post顶cookie的名
        /// </summary>
        public static string ConfessUpCookieName; //顶cookie的名
        /// <summary>
        /// Secret顶cookie的名
        /// </summary>
        public static string FunnyUpCookieName;  //

        public static string CommentUpCookieName;

        public static string UidEnCryptKey; 
        
        static EncryptConfig()
        {
            InitConfig();
        }
        //每增加一个,须要增加到此处
        public static void InitConfig()
        {
            UidEnCryptKey = System.Configuration.ConfigurationManager.AppSettings["UidEnCryptKey"];
            FunnyUpCookieName = System.Configuration.ConfigurationManager.AppSettings["FunnyUpCookieName"];
            ConfessUpCookieName = System.Configuration.ConfigurationManager.AppSettings["ConfessUpCookieName"];
            CommentUpCookieName = System.Configuration.ConfigurationManager.AppSettings["CommentUpCookieName"];
            UserMainKey = System.Configuration.ConfigurationManager.AppSettings["userMainKey"];
            AdminCookieEncryptKey = System.Configuration.ConfigurationManager.AppSettings["adminCookieEncryptKey"];
        }



    }
}
