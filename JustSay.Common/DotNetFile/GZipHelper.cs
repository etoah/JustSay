using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace JustSay.Common.DotNetFile
{
    public class GZipHelper
    {
        public static string Compress(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }
            ms.Position = 0L;
            byte[] compressed = ms.ToArray();
            ms.Read(compressed, 0, compressed.Length);
            byte[] gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string Uncompress(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            MemoryStream ms = new MemoryStream();
            int msgLength = BitConverter.ToInt32(gzBuffer, 0);
            ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
            byte[] buffer = new byte[msgLength];
            ms.Position = 0L;
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
            zip.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        public static T GZip<T>(Stream stream, CompressionMode mode) where T : Stream
        {
            byte[] writeData = new byte[4096];
            T ms = default(T);
            T result;
            using (Stream sg = new GZipStream(stream, mode))
            {
                while (true)
                {
                    Array.Clear(writeData, 0, writeData.Length);
                    int size = sg.Read(writeData, 0, writeData.Length);
                    if (size <= 0)
                    {
                        break;
                    }
                    ms.Write(writeData, 0, size);
                }
                result = ms;
            }
            return result;
        }

        public static byte[] Compress(byte[] bytData)
        {
            byte[] result;
            using (MemoryStream stream = GZipHelper.GZip<MemoryStream>(new MemoryStream(bytData), CompressionMode.Compress))
            {
                result = stream.ToArray();
            }
            return result;
        }

        public static byte[] Decompress(byte[] bytData)
        {
            byte[] result;
            using (MemoryStream stream = GZipHelper.GZip<MemoryStream>(new MemoryStream(bytData), CompressionMode.Decompress))
            {
                result = stream.ToArray();
            }
            return result;
        }

        public static void CompressFile(string sourceFile, string destinationFile)
        {
            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException();
            }
            if (!File.Exists(destinationFile))
            {
                FileHelper.DeleteFile(destinationFile);
            }
            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream compressedStream = null;
            try
            {
                sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] buffer = new byte[sourceStream.Length];
                int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
                if (checkCounter != buffer.Length)
                {
                    throw new ApplicationException();
                }
                destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write);
                compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true);
                compressedStream.Write(buffer, 0, buffer.Length);
            }
            finally
            {
                if (sourceStream != null)
                {
                    sourceStream.Close();
                }
                if (compressedStream != null)
                {
                    compressedStream.Close();
                }
                if (destinationStream != null)
                {
                    destinationStream.Close();
                }
            }
        }

        public static void DecompressFile(string sourceFile, string destinationFile)
        {
            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException();
            }
            FileStream stream = null;
            FileStream stream2 = null;
            GZipStream stream3 = null;
            try
            {
                stream = new FileStream(sourceFile, FileMode.Open);
                stream3 = new GZipStream(stream, CompressionMode.Decompress, true);
                byte[] buffer = new byte[4];
                int num = (int)stream.Length - 4;
                stream.Position = (long)num;
                stream.Read(buffer, 0, 4);
                stream.Position = 0L;
                byte[] buffer2 = new byte[BitConverter.ToInt32(buffer, 0) + 100];
                int offset = 0;
                int count = 0;
                while (true)
                {
                    int num2 = stream3.Read(buffer2, offset, 100);
                    if (num2 == 0)
                    {
                        break;
                    }
                    offset += num2;
                    count += num2;
                }
                stream2 = new FileStream(destinationFile, FileMode.Create);
                stream2.Write(buffer2, 0, count);
                stream2.Flush();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (stream3 != null)
                {
                    stream3.Close();
                }
                if (stream2 != null)
                {
                    stream2.Close();
                }
            }
        }
    }
}