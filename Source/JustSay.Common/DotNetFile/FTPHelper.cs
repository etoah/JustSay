using System;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;

namespace JustSay.Common.DotNetFile
{
    public class FTPHelper
    {
        public string Upload(FileUpload fileUpload, string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            string filename = fileUpload.FileName;
            string sRet = "上传成功！";
            FileInfo fileInf = new FileInfo(fileUpload.PostedFile.FileName);
            string uri = "ftp://" + ftpServerIP + "/" + filename;
            FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = "STOR";
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = false;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                for (int contentLen = fs.Read(buff, 0, buffLength); contentLen != 0; contentLen = fs.Read(buff, 0, buffLength))
                {
                    strm.Write(buff, 0, contentLen);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return sRet;
        }

        public string Download(string userId, string pwd, string ftpPath, string filePath, string fileName)
        {
            string sRet = "下载成功！";
            try
            {
                FileStream outputStream = new FileStream(filePath + fileName, FileMode.Create);
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(ftpPath + fileName));
                reqFTP.Method = "RETR";
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
                reqFTP.Credentials = new NetworkCredential(userId, pwd);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];
                for (int readCount = ftpStream.Read(buffer, 0, bufferSize); readCount > 0; readCount = ftpStream.Read(buffer, 0, bufferSize))
                {
                    outputStream.Write(buffer, 0, readCount);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            return sRet;
        }

        public string DeleteFile(string ftpPath, string userId, string pwd, string fileName)
        {
            string sRet = "删除成功！";
            FtpWebResponse Respose = null;
            Stream localfile = null;
            Stream stream = null;
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}{1}", ftpPath, fileName)));
                reqFTP.Credentials = new NetworkCredential(userId, pwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = "DELE";
                Respose = (FtpWebResponse)reqFTP.GetResponse();
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            finally
            {
                if (Respose != null)
                {
                    Respose.Close();
                }
                if (localfile != null)
                {
                    localfile.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return sRet;
        }
    }
}