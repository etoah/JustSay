using System;
using System.IO;
using System.Web.UI.WebControls;

namespace JustSay.Common.DotNetFile
{
    public class UploadHelper
    {
        public static string FileUpload(string path, FileUpload filleupload)
        {
            string result;
            try
            {
                bool fileOk = false;
                string fileExtension = Path.GetExtension(filleupload.FileName).ToLower();
                string[] allowExtension = new string[]
				{
					".rar",
					".zip",
					".rar",
					".ios",
					".jpg",
					".png",
					"bmp",
					".gif",
					".txt"
				};
                if (filleupload.HasFile)
                {
                    for (int i = 0; i < allowExtension.Length; i++)
                    {
                        if (fileExtension == allowExtension[i])
                        {
                            fileOk = true;
                            break;
                        }
                    }
                }
                if (fileOk)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (!FileHelper.IsExistFile(path + filleupload.FileName))
                    {
                        int Size = filleupload.PostedFile.ContentLength / 1024 / 1024;
                        if (Size > 10)
                        {
                            result = "上传失败,文件过大";
                        }
                        else
                        {
                            filleupload.PostedFile.SaveAs(path + filleupload.FileName);
                            result = "上传成功";
                        }
                    }
                    else
                    {
                        result = "上传失败,文件已存在";
                    }
                }
                else
                {
                    result = "不支持【" + fileExtension + "】文件格式";
                }
            }
            catch (Exception)
            {
                result = "上传失败";
            }
            return result;
        }
    }
}