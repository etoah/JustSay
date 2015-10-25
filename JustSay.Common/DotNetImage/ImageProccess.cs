using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JustSay.Common.DotNetCode;
using JustSay.Common.DotNetImage;

namespace JustSay.Common.DotNetImage
{
    public class ImageProccess
    {
        public static string CompressImageProccess(Stream inputStream, string root, string waterMark)
        {
            string fname = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            try
            {
                ImageHelper.MakeThumbnailAndMark(inputStream, (root + "/Upload/Img/"+ fname), 600, waterMark);
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog(ex, "图像处理异常", "RelationBLL");
                return "";
            }

            return fname;
        }
    
    }
}
