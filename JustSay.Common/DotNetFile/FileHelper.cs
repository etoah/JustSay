using JustSay.Common.DotNetCode;
using JustSay.Common.DotNetConfig;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace JustSay.Common.DotNetFile
{
    public class FileHelper
    {
        protected static LogHelper Logger = new LogHelper("文件帮助类");

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] result;
            try
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));
                result = buffer;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
            return result;
        }

        public static Stream BytesToStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static void StreamToFile(Stream stream, string fileName)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0L, SeekOrigin.Begin);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        public static Stream FileToStream(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            return new MemoryStream(bytes);
        }

        public static bool IsExistFile(string filename)
        {
            return File.Exists(filename);
        }

        public static bool CopyFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            bool result;
            if (!overwrite && File.Exists(destFileName))
            {
                result = false;
            }
            else
            {
                try
                {
                    File.Copy(sourceFileName, destFileName, true);
                    result = true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return result;
        }

        public static bool CopyFile(string sourceFileName, string destFileName)
        {
            return FileHelper.CopyFile(sourceFileName, destFileName, true);
        }

        public static string[] GetDirectoryFileList(string path, string searchPattern)
        {
            string[] result2;
            if (!Directory.Exists(path))
            {
                result2 = new string[0];
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                FileInfo[] fileInfos = dirInfo.GetFiles(searchPattern);
                string[] result = new string[fileInfos.Length];
                for (int i = 0; i < fileInfos.Length; i++)
                {
                    result[i] = fileInfos[i].Name;
                }
                result2 = result;
            }
            return result2;
        }

        public static string[] GetDirectoryFileList(string path)
        {
            return FileHelper.GetDirectoryFileList(path, "*.*");
        }

        public static byte[] FileToBytes(string path)
        {
            FileStream fs = File.OpenRead(path);
            MemoryStream ms = new MemoryStream();
            int bdata;
            while ((bdata = fs.ReadByte()) != -1)
            {
                ms.WriteByte((byte)bdata);
            }
            byte[] data = ms.ToArray();
            fs.Close();
            ms.Close();
            return data;
        }

        public static bool UpLoadFile(byte[] buffer, string filePath)
        {
            bool result;
            try
            {
                if (!FileHelper.IsExistFile(filePath))
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        fs.Write(buffer, 0, buffer.Length);
                    }
                    result = true;
                    return result;
                }
            }
            catch
            {
                result = false;
                return result;
            }
            result = false;
            return result;
        }

        public static string ImgToBase64String(string filePath)
        {
            string result;
            try
            {
                Bitmap bmp = new Bitmap(filePath);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0L;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                result = Convert.ToBase64String(arr);
                return result;
            }
            catch (Exception e)
            {
                FileHelper.Logger.WriteLog("图片转为base64编码出现异常\r\n" + e.Message + "\r\n");
            }
            result = "";
            return result;
        }

        public static bool Base64ToImage(string photo, string filePath)
        {
            bool result;
            try
            {
                string dirpath = ConfigHelper.GetAppSettings("SaveFile") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                byte[] arr = Convert.FromBase64String(photo);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(filePath);
                ms.Close();
                result = true;
            }
            catch (Exception e)
            {
                FileHelper.Logger.WriteLog(string.Concat(new string[]
				{
					"base64编码的文本转为图片出现异常\r\n",
					e.Message,
					"\r\n base64编码",
					photo,
					"\r\n"
				}));
                result = false;
            }
            return result;
        }

        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = Size;
            if ((double)FactSize < 1024.0)
            {
                m_strSize = FactSize.ToString("F2") + " 字节";
            }
            else
            {
                if ((double)FactSize >= 1024.0 && FactSize < 1048576L)
                {
                    m_strSize = ((double)FactSize / 1024.0).ToString("F2") + " KB";
                }
                else
                {
                    if (FactSize >= 1048576L && FactSize < 1073741824L)
                    {
                        m_strSize = ((double)FactSize / 1024.0 / 1024.0).ToString("F2") + " MB";
                    }
                    else
                    {
                        if (FactSize >= 1073741824L)
                        {
                            m_strSize = ((double)FactSize / 1024.0 / 1024.0 / 1024.0).ToString("F2") + " GB";
                        }
                    }
                }
            }
            return m_strSize;
        }

        public static string GetFileSize(string fileName)
        {
            string result;
            if (File.Exists(fileName))
            {
                FileInfo fileinfo = new FileInfo(fileName);
                long fl = fileinfo.Length;
                if (fl > 1073741824L)
                {
                    result = Convert.ToString(Math.Round(((double)fl + 0.0) / 1073741824.0, 2)) + " GB";
                }
                else
                {
                    if (fl > 1048576L)
                    {
                        result = Convert.ToString(Math.Round(((double)fl + 0.0) / 1048576.0, 2)) + " MB";
                    }
                    else
                    {
                        result = Convert.ToString(Math.Round(((double)fl + 0.0) / 1024.0, 2)) + " KB";
                    }
                }
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static string GetDictSize(string path)
        {
            string result;
            if (!Directory.Exists(path))
            {
                result = "0";
            }
            else
            {
                string[] fs = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                long ll = 0L;
                string[] array = fs;
                for (int i = 0; i < array.Length; i++)
                {
                    string f = array[i];
                    object fa = File.GetAttributes(f);
                    FileInfo fi = new FileInfo(f);
                    ll += fi.Length;
                }
                result = FileHelper.CountSize(ll);
            }
            return result;
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static void DeleteDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                DirectoryInfo[] childs = dir.GetDirectories();
                DirectoryInfo[] array = childs;
                for (int i = 0; i < array.Length; i++)
                {
                    DirectoryInfo child = array[i];
                    child.Delete(true);
                }
                dir.Delete(true);
            }
        }

        public byte[] DownloadFileByte(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            return new byte[fs.Length];
        }

        public static void GetDownload(string filePath)
        {
            string Name = Path.GetFileName(filePath);
            string FullFileName = HttpContext.Current.Server.MapPath(filePath);
            FileInfo DownloadFile = new FileInfo(FullFileName);
            if (DownloadFile.Exists)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + Name);
                HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                HttpContext.Current.Response.WriteFile(DownloadFile.FullName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}