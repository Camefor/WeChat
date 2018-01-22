using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.API.Tools
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 10:20:56
    * 说明：
    * ==========================================================
    * */
    public class Util
    {
        public static string GetMD5(byte[] data)
        {

            System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create();
            var hash = md5Hash.ComputeHash(data);

            // 创建一个 Stringbuilder 来收集字节并创建字符串  
            StringBuilder sBuilder = new StringBuilder();

            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < hash.Length; i++)
            {
                sBuilder.Append(hash[i].ToString("x2"));
            }

            // 返回十六进制字符串  
            return sBuilder.ToString();

        }

        /// <summary>
        /// 对文件进行加密
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }


        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }


        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]</returns>

        public static byte[] AuthGetFileData(string fileUrl)
        {
            using (FileStream fs = new FileStream(fileUrl, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] buffur = new byte[fs.Length];
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(buffur);
                    bw.Close();
                }
                return buffur;
            }
        }


        /// <summary>
        /// Image 转 Byte[]
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image image,out string Format)
        {
            Format = ".png";
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    Format = ".jpeg";
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    Format = ".png";
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    Format = ".bmp";
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    Format = ".gif";
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    Format = ".icon";
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
    }
}
