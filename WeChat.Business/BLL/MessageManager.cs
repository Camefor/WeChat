using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WeChat.Business.APP;
using WeChat.Business.Base;
using WeChat.Business.MessageItems;
using WeChat.Business.Model;
using WeChat.Business.Net;
using WeChat.Business.Utils;

namespace WeChat.Business.BLL
{
    /***
    * ===========================================================
    * 创建人：袁建廷
    * 创建时间：2017/07/17 8:58:40
    * 说明：
    * ==========================================================
    * */
    public class MessageManager : BaseManager
    {
        private PicItem PicItem;
        private bool online = true;
        public Boolean Online
        {
            get { return online; }
            set { online = value; }
        }

        public MessageManager(HttpTools http)
            : base(http)
        {
            PicItem = new PicItem(http);
        }



        public void GetMessage(IMessageCallBack CallBack)
        {
            int code, selector = 0;
            while (online)
            {
                if (synccheck(out code, out selector))
                {
                    if (code == 1100 || code == 1101)
                    {
                        CallBack.OnWeChatOut("手机微信退出,已自动掉线");
                        return;
                    }
                    MessageResponse response = webwxsync();
                    ExplainMessage(response, selector, CallBack);
                }
            }

        }


        private void ExplainMessage(MessageResponse response, int selector, IMessageCallBack CallBack)
        {
            if (response == null)
                return;

            switch (selector)
            {
                case 2://新的消息
                    if (response.ModContactCount > 0)
                    {
                        List<ModContactList> mcl = response.ModContactList;
                        if (mcl != null && mcl.Count > 0)
                        {
                            foreach (ModContactList item in mcl)
                            {
                                if (item == null) continue;
                                RContact c = null;
                                if (item.UserName.StartsWith("@@"))
                                {
                                    //群
                                    if (Context.GroupList.ContainsKey(item.UserName))
                                    {
                                        c = Context.GroupList[item.UserName];
                                    }
                                    else
                                    {
                                        //如果群不存在则，说明为新群
                                        c = new RContact();
                                        c.UserName = item.UserName;
                                        c.NickName = item.NickName;
                                        c.HeadImgUrl = item.HeadImgUrl;
                                        Context.GroupList.Add(item.UserName, c);
                                    }

                                }
                                else
                                {
                                    //好友
                                    if (Context.ContactList.ContainsKey(item.UserName))
                                    {
                                        c = Context.ContactList[item.UserName];
                                    }

                                }
                                if (c != null)
                                {
                                    CallBack.OnNewRContact(c);//新好友
                                }
                            }
                        }
                    }

                    if (response.BaseResponse.Ret == 0)
                    {
                        if (response.AddMsgList.Count == 0)
                            return;

                        foreach (WMessage item in response.AddMsgList)
                        {
                            //判断IsSend
                            if (item.FromUserName == Context.user.UserName)
                            {
                                item.IsSend = true;
                            }
                            if (item.FromUserName.StartsWith("@@"))
                            {
                                item.isGroup = true;
                            }
                            switch (item.MsgType)
                            {
                                case 47://表情图片
                                    string path = PicItem.AnalysisImageMessage(item.Content);
                                    item.FileContent = path;
                                    break;
                                default:
                                    break;
                            }
                            CallBack.OnMessage(item);
                        }
                    }

                    break;
                case 4://通过时发现 、 朋友圈有动态
                    //m_SyncContext.Post(UpdateState, "通过时发现 、 朋友圈有动态");
                    Console.WriteLine("通过时发现 、 朋友圈有动态");
                    //清空本地好友
                    //App.ClearAll();

                    //ContactResponse responseContact = api.GetWeChatCmmand().webwxgetcontact();
                    //this.Invoke((EventHandler)delegate
                    //{
                    //    UpdateRContactList(App.ContactList.Values);//更新列表
                    //});
                    //result = api.GetWeChatCmmand().webwxstatusnotify();
                    break;
                case 6://删除时发现，对方通过好友验证 、 有消息返回结果
                    //m_SyncContext.Post(UpdateState, "删除时发现，对方通过好友验证 、 有消息返回结果");
                    Console.WriteLine("删除时发现，对方通过好友验证 、 有消息返回结果");

                    //responseContact = api.GetWeChatCmmand().webwxgetcontact();
                    //this.Invoke((EventHandler)delegate
                    //{
                    //    UpdateRContactList(App.ContactList.Values);//更新列表
                    //});
                    //result = api.GetWeChatCmmand().webwxstatusnotify();

                    if (response != null)
                    {
                        //添加通过好友到最近聊天
                        foreach (ModContactList item in response.ModContactList)
                        {

                            //CallBack.OnNewRContact(c);//新好友


                        }

                        //如果为新添加好友 则显示新消息
                        foreach (WMessage item in response.AddMsgList)
                        {
                            CallBack.OnMessage(item);
                        }
                    }


                    //开始获取好友
                    //AsyncTask.StartNew(AsyncTaskGetContact);
                    break;
                case 7://进入/离开聊天界面
                    //m_SyncContext.Post(UpdateState, "进入/离开聊天界面");
                    Console.WriteLine("进入/离开聊天界面");
                    break;
                case 0://正常

                    break;

            }
        }




        /// <summary>
        /// 同步刷新（get轮询）
        /// </summary>
        /// <returns></returns>
        private bool synccheck(out int code, out int selector)
        {
            try
            {

                string begin = "开始：" + DateTime.Now;
                string time = Tools.GetTimeStamp();
                string[] array = {
                                     "r="+time,
                                     "skey="+System.Web.HttpUtility.UrlEncode(Context.skey),
                                     "sid="+System.Web.HttpUtility.UrlEncode(Context.sid),
                                     "uin="+Context.uin,
                                     "deviceid="+Context.DeviceID,
                                     "synckey="+Context.synckey,
                                     "_="+Tools.GetTimeStamp()

                                     };
                //352752675839652
                Uri uri = new Uri(Context.base_uri);
                string hos = uri.Authority;

                string param = string.Join("&", array);

                string url = "https://webpush." + hos + "/cgi-bin/mmwebwx-bin/synccheck?" + param;
                string json = http.GetPage(url, 30000, null);
                //string json = httpTools.GetPage(url);
                string end = "结束：" + DateTime.Now;
                string[] array1 = new string[] { begin, json, end };

                Match match = Tools.search("window.synccheck={retcode:\"(\\d+)\",selector:\"(\\d+)\"}", json);
                if (match.Groups.Count > 1)
                {
                    code = Convert.ToInt32(match.Groups[1].Value);
                    selector = Convert.ToInt32(match.Groups[2].Value);
                    return true;
                }
            }
            catch (Exception e)
            {
                LogHandler.e(e);
            }
            code = -1;
            selector = -1;
            return false;
        }


        /// <summary>
        /// 获取 消息
        /// </summary>
        /// <returns></returns>
        private MessageResponse webwxsync()
        {
            try
            {
                string url = Context.base_uri + "/webwxsync?sid=" + Context.sid + "&skey=" + Context.skey + "&lang=zh_CN&pass_ticket=" + Context.pass_ticket;
                string syks = JsonConvert.SerializeObject(Context.SyncKeys.List);
                string param = "{\"BaseRequest\":{\"Uin\":" + Context.uin + ",\"Sid\":\"" + Context.sid + "\",\"Skey\":\"" +
                    Context.skey + "\",\"DeviceID\":\"" + Context.DeviceID + "\"},\"SyncKey\":{\"Count\":" + Context.SyncKeys.Count + ",\"List\":" + syks + "},\"rr\":856481324}";


                string json = http.PostData(url, param);

                MessageResponse response = JsonConvert.DeserializeObject<MessageResponse>(json);
                if (response != null)
                {
                    if (response.BaseResponse.Ret == 0)
                    {
                        Context.SyncKeys = response.SyncKey;
                        string[] array = new string[response.SyncKey.Count];
                        int i = 0;
                        foreach (Model.Keys item in response.SyncKey.List)
                        {
                            array[i] = item.Key + "_" + item.Val;
                            i++;
                        }
                        Context.synckey = System.Web.HttpUtility.UrlEncode(string.Join("|", array));
                    }
                    return response;
                }
            }
            catch (Exception x)
            {
                LogHandler.e(x);
            }
            return null;
        }




        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="FormUserName">发送方</param>
        /// <param name="ToUserName">接受方</param>
        /// <param name="Message">消息内容</param>
        /// <returns></returns>
        public bool SendTextMessage(string FormUserName, string ToUserName, string Message)
        {
            try
            {
                string url = Context.base_uri + "/webwxsendmsg?lang=zh_CN&pass_ticket=" + Context.pass_ticket;
                var param = new
                {
                    BaseRequest = Context.BaseRequest,
                    Msg = new
                    {
                        Type = 1,
                        Content = Message,
                        FromUserName = Context.user.UserName,
                        ToUserName = ToUserName,
                        LocalID = Tools.GetTimeStamp(),
                        ClientMsgId = Tools.GetTimeStamp()
                    },
                    Scene = 0
                };
                string str = JsonConvert.SerializeObject(param);
                string json = http.PostData(url, str);
                StatusNotifyResponse response = JsonConvert.DeserializeObject<StatusNotifyResponse>(json);
                if (response == null || response.BaseResponse.Ret != 0)
                    return false;

            }
            catch (Exception e)
            {
                LogHandler.e(e);
                return false;
            }
            return true;
        }



    }
}
