using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustSay.Config
{
    public static class Config
    {
        /// <summary>
        /// 初始化所有Config类,新增加一个类须加入到此处
        /// </summary>
        public static void IniConfig()
        {
            HotConfig.InitConfig();
            UIConfig.InitConfig();
            EncryptConfig.InitConfig();
        }
    }
}
