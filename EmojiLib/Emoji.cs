using EmojiLib.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace EmojiLib
{
    /***
  * ===========================================================
  * 创建人：袁建廷
  * 创建时间：2017/09/07 11:46:04
  * 说明：
  * ==========================================================
  * */
    public class Emoji
    {
        /// <summary>
        /// 过滤字符串中的emoji表情
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterEmoji(string str, char sign)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            string json = Resource.emojis;
            List<EmojiModel> emoji_list = JsonConvert.DeserializeObject<List<EmojiModel>>(json);
            foreach (EmojiModel item in emoji_list)
            {
                str = str.Replace(item.keywords, sign + "\\" + item.ResouresName + "\\" + sign);
            }
            return str;
        }

        /// <summary>
        /// 将字符串分割为list集合
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static List<string> DivisionString(string test_data, char sign)
        {
            List<string> arrays = new List<string>();
            string str = EmojiLib.Emoji.FilterEmoji(test_data, sign);
            string[] array = str.Split(sign);
            foreach (string item in array)
            {
                arrays.Add(item);
            }

            return arrays;
        }

        /// <summary>
        /// 字符串转集合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<Object> ConvertToEmojiAndString(string str)
        {
            List<Object> objs = new List<object>();
            char sign = '▓';
            string message = EmojiLib.Emoji.FilterEmoji(str, sign);
            List<string> array = EmojiLib.Emoji.DivisionString(str, sign);
            foreach (string item in array)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                if (item.StartsWith("\\") && item.IndexOf("emoji") != -1)
                {
                    //获取图片
                    string emojiName = item.Substring(1, item.LastIndexOf('\\') - 1);
                    Bitmap bit = EmojiLib.Emoji.GetImageByName(emojiName);
                    objs.Add(bit);
                }
                else
                {
                    objs.Add(item);
                }
            }
            return objs;
        }


        /// <summary>
        /// 更新资源名称返回对应图片
        /// </summary>
        /// <param name="ResouresName"></param>
        /// <returns></returns>
        public static Bitmap GetImageByName(string ResouresName)
        {
            if (string.IsNullOrWhiteSpace(ResouresName))
                return null;
            //去掉后缀
            if (ResouresName.LastIndexOf('.') != -1)
                ResouresName = ResouresName.Substring(0, ResouresName.LastIndexOf('.'));
            Object obj = Resource.ResourceManager.GetObject(ResouresName);
            if (obj != null && obj is Bitmap)
            {
                return (Bitmap)obj;
            }
            return null;
        }




    }
}
