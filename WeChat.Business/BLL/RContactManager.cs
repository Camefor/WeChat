using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeChat.Business.APP;
using WeChat.Business.Base;
using WeChat.Business.Model;
using WeChat.Business.Net;
using WeChat.Business.Utils;

namespace WeChat.Business.BLL
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/06/28 16:22:46
    * 说明：
    * ==========================================================
    * */
    public class RContactManager:BaseManager
    {

        public RContactManager(HttpTools http)
            : base(http) 
        {
            
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public void Webwxinit(SendOrPostCallback d)
        {
            UserResponse response = null;
            try
            {
                string url = Context.base_uri + "/webwxinit?lang=zh_CN&pass_ticket=" + Context.pass_ticket;

                string json = http.PostData(url, Context.BaseRequest);

                response = JsonConvert.DeserializeObject<UserResponse>(json);
                if (response != null)
                {
                    Context.user = response.User;
                    Context.SyncKeys = response.SyncKey;
                    string[] array = new string[response.SyncKey.Count];
                    int i = 0;
                    foreach (Model.Keys item in response.SyncKey.List)
                    {
                        array[i] = item.Key + "_" + item.Val;
                        i++;
                    }
                    Context.synckey = System.Web.HttpUtility.UrlEncode(string.Join("|", array));/////接收不到消息


                    foreach (RContact item in response.ContactList)
                    {
                        try
                        {
                            string key = item.UserName;
                            if (item.VerifyFlag == 8 || item.VerifyFlag == 24)
                            {
                                if (Context.PublicUsersList.ContainsKey(key) == false)
                                {
                                    Context.PublicUsersList.Add(key, item);
                                }
                            }
                            else if (key.StartsWith("@@")) //群聊
                            {
                                if (!Context.GroupList.ContainsKey(key))
                                {
                                    Context.GroupList.Add(key, item);
                                }
                            }
                            else if (Context.SpecialUsers.Contains(key))//特殊账号
                            {
                                if (!Context.SpecialUsersList.ContainsKey(key))
                                {
                                    Context.SpecialUsersList.Add(key, item);
                                }
                            }
                            else
                            {
                                if (!Context.ContactList.ContainsKey(key))
                                {
                                    Context.ContactList.Add(key, item);
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

            }
            catch (Exception e)
            {
                LogHandler.e(e);
            }
            if (m_SyncContext != null)
                m_SyncContext.Post(d, response);
        }



        public void GetRContact(SendOrPostCallback d)
        {
            List<RContact> items = new List<RContact>();
            for (int i = 0; i < 20; i++)
            {
                RContact contact = new RContact();
                contact.NickName = "小冰";
                contact.LastMessage = "听多了爱情故事中的那。";
                contact.LastMessageTime = DateTime.Now;
                items.Add(contact);
            }
            if (m_SyncContext != null)
                m_SyncContext.Post(d, items);
        }




    }
}
