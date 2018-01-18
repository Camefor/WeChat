using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeChat.API.Result;
using WeChat.API.RPC;
using WeChat.API.Tools;
using WeChat.API.Wx;

namespace WeChat.API
{
    /***
    * ===========================================================
    * 创建人：yuanj
    * 创建时间：2018/01/17 10:32:11
    * 说明：
    * ==========================================================
    * */
    public class WechatAPIService
    {
        private Client client;
        private Thread mMainLoopThread;
        private bool mIsQuit = false;
        private string mLoginSession;
        private BaseRequest mBaseReq;
        private string mPass_ticket;
        private SyncKey mSyncKey;
        private ClientStatusType mStatus = ClientStatusType.None;

        #region Event
        public delegate void GetQRCodeImageHandler(WechatAPIService sender, GetQRCodeImageEvent e);
        public event GetQRCodeImageHandler OnGetQRCodeImage;
        public delegate void UserScanQRCodeHandler(WechatAPIService sender, UserScanQRCodeEvent e);
        public event UserScanQRCodeHandler OnUserScanQRCode;
        public delegate void LoginSucessHandler(WechatAPIService sender, LoginSucessEvent e);
        public event LoginSucessHandler OnLoginSucess;
        public delegate void InitedHandler(WechatAPIService sender, InitedEvent e);
        public event InitedHandler OnInited;
        public delegate void AddMessageHandler(WechatAPIService sender, AddMessageEvent e);
        public event AddMessageHandler OnAddMessage;
        public delegate void StatusChangedHandler(WechatAPIService sender, StatusChangedEvent e);
        public event StatusChangedHandler OnStatusChanged;

        public delegate void GetUserHandler(WechatAPIService sender, GetUserEvent e);
        /// <summary>
        /// 当前用户信息加载完成
        /// </summary>
        public event GetUserHandler OnGetUser;

        #endregion
        /// <summary>
        /// 文件缓存路径
        /// </summary>
        public string CachePath { get; set; }

        public ClientStatusType CurrentStatus
        {
            get
            {
                return mStatus;
            }
            private set
            {
                if (mStatus != value)
                {
                    var changedEvent = new StatusChangedEvent()
                    {
                        FromStatus = mStatus,
                        ToStatus = value
                    };
                    mStatus = value;
                    OnStatusChanged?.Invoke(this, changedEvent);
                }
            }
        }
        public bool IsLogin { get; private set; }
        public static Contact Self { get; private set; }
        private static List<Contact> mContacts;

        public Contact[] Contacts
        {
            get
            {
                return mContacts.ToArray();
            }
            private set
            {
                mContacts = new List<Contact>();
                mContacts.AddRange(value);
            }
        }
        private static List<Group> mGroups;
        public Group[] Groups
        {
            get
            {
                return mGroups.ToArray();
            }
            private set
            {
                mGroups = new List<Group>();
                mGroups.AddRange(value);
            }
        }

        public static Group GetGroup(string ID)
        {
            if (mGroups == null) return null;
            return mGroups.FindLast((group) =>
            {
                return group.ID == ID;
            });
        }

        public static Contact GetContact(string ID)
        {
            if (ID == Self.ID) return Self;
            if (mContacts == null) return null;
            return mContacts.FindLast((contact) =>
            {
                return contact.ID == ID;
            });
        }

        public static Contact GetContactBySeq(string seq)
        {
            if (mContacts == null) return null;
            return mContacts.FindLast(n=>n.Seq== seq);
        }

        public void Start()
        {
            Quit();
            mIsQuit = false;
            IsLogin = false;
            CurrentStatus = ClientStatusType.GetUUID;
            client = new Client();

            mMainLoopThread = new System.Threading.Thread(MainLoop);
            mMainLoopThread.Start();
        }

        public void Quit(bool force = false)
        {
            mIsQuit = true;
            Logout();
            if (force)
            {
                if (mMainLoopThread != null && mMainLoopThread.IsAlive)
                {
                    mMainLoopThread.Abort();
                }
            }
        }


        private void MainLoop()
        {
            while (!mIsQuit)
            {
                HandleStatus();
            }
        }

        private void HandleStatus()
        {
            switch (CurrentStatus)
            {
                case ClientStatusType.GetUUID:
                    HandleGetLoginSession();
                    break;
                case ClientStatusType.GetQRCode:
                    HandleGetQRCode();
                    break;
                case ClientStatusType.Login:
                    HandleLogin();
                    break;
                case ClientStatusType.QRCodeScaned:
                    HandleQRCodeScaned();
                    break;
                case ClientStatusType.WeixinInit:
                    HandleInit();
                    MessageFactory.Init(this);
                    break;

                case ClientStatusType.WeixinSync:
                    HandleSync();
                    break;
            }
        }

        private void HandleSync()
        {
            if (mSyncKey == null)
            {
                CurrentStatus = ClientStatusType.GetUUID;
                return;
            }
            if (mSyncKey.Count <= 0) return;

            var checkResult = client.SyncCheck(mSyncKey.List, mBaseReq, ++mSyncCheckTimes);
            if (checkResult == null) return;


            if (checkResult.retcode != null && checkResult.retcode != "0")
            {
                CurrentStatus = ClientStatusType.GetUUID;
                return;
            }
            if (checkResult.selector == "0") return;
            var syncResult = client.Sync(mSyncKey, mPass_ticket, mBaseReq);
            if (syncResult == null) return;
            mSyncKey = syncResult.SyncKey;

            // 处理同步
            ProcessSyncResult(syncResult);
        }

        private void ProcessSyncResult(SyncResponse result)
        {
            // 处理消息
            if (result.AddMsgCount > 0)
            {
                foreach (var msg in result.AddMsgList)
                {
                    var message = MessageFactory.CreateMessage(msg);
                    OnAddMessage?.Invoke(this, new AddMessageEvent()
                    {
                        Msg = message
                    });
                }
            }
        }

        private void HandleInit()
        {
            List<Contact> list = new List<Contact>();
            var initResult = client.Init(mPass_ticket, mBaseReq);
            if (initResult != null && initResult.BaseResponse.ret == 0)
            {
                Self = CreateContact(initResult.User);
                OnGetUser?.Invoke(this, new GetUserEvent() { Self = Self });
                foreach (var item in initResult.ContactList)
                {
                    list.Add(CreateContact(item));
                }

                mSyncKey = initResult.SyncKey;
                // 开启系统通知
                var statusNotifyRep = client.Statusnotify(Self.ID, Self.ID, mPass_ticket, mBaseReq);
                if (statusNotifyRep != null && statusNotifyRep.BaseResponse != null && statusNotifyRep.BaseResponse.ret == 0)
                {
                    CurrentStatus = ClientStatusType.WeixinSync;
                    IsLogin = true;
                }
                else
                {
                    CurrentStatus = ClientStatusType.GetUUID;
                    return;
                }
            }
            else
            {
                CurrentStatus = ClientStatusType.GetUUID;
                return;
            }

            if (!InitContactAndGroups())
            {
                CurrentStatus = ClientStatusType.WeixinInit;
                IsLogin = false;
                return;
            }


            OnInited?.Invoke(this, new InitedEvent() { LastContact = list });
        }

        private bool InitContactAndGroups()
        {
            mContacts = new List<Contact>();
            mGroups = new List<Group>();

            var contactResult = client.GetContact(mPass_ticket, mBaseReq.Skey);
            if (contactResult == null || contactResult.BaseResponse == null || contactResult.BaseResponse.ret != 0)
            {
                return false;
            }

            List<string> groupIDs = new List<string>();
            foreach (var user in contactResult.MemberList)
            {
                if (user.UserName.StartsWith("@@"))
                {
                    groupIDs.Add(user.UserName);
                }
                else
                {
                    var contact = CreateContact(user);
                    mContacts.Add(contact);
                }
            }

            if (groupIDs.Count <= 0) return true;
            // 批量获得群成员详细信息
            var batchResult = client.BatchGetContact(groupIDs.ToArray(), mPass_ticket, mBaseReq);
            if (batchResult == null || batchResult.BaseResponse.ret != 0) return false;

            foreach (var user in batchResult.ContactList)
            {
                if (!user.UserName.StartsWith("@@") || user.MemberCount <= 0) continue;
                Group group = new Group();
                group.ID = user.UserName;
                group.NickName = user.NickName;
                group.RemarkName = user.RemarkName;
                List<Contact> groupMembers = new List<Contact>();
                foreach (var member in user.MemberList)
                {
                    groupMembers.Add(CreateContact(member));
                }
                group.Members = groupMembers.ToArray();
                mGroups.Add(group);
            }

            return true;
        }

        private long mSyncCheckTimes = 0;
        private void HandleQRCodeScaned()
        {
            mSyncCheckTimes = Util.GetTimeStamp();
            var loginResult = client.Login(mLoginSession, mSyncCheckTimes);
            if (loginResult != null && loginResult.code == 200)
            {
                // 登录成功
                var redirectResult = client.LoginRedirect(loginResult.redirect_uri);
                mBaseReq = new BaseRequest();
                mBaseReq.Skey = redirectResult.skey;
                mBaseReq.Sid = redirectResult.wxsid;
                mBaseReq.Uin = redirectResult.wxuin;
                mBaseReq.DeviceID = CreateNewDeviceID();
                mPass_ticket = redirectResult.pass_ticket;
                CurrentStatus = ClientStatusType.WeixinInit;
                OnLoginSucess?.Invoke(this, new LoginSucessEvent());
            }
            else
            {
                CurrentStatus = ClientStatusType.GetUUID;
            }
        }

        private void HandleLogin()
        {
            var loginResult = client.Login(mLoginSession, Util.GetTimeStamp());
            if (loginResult != null && loginResult.code == 201)
            {
                // 已扫描,但是未确认登录
                // convert base64 to image
                byte[] base64_image_bytes = Convert.FromBase64String(loginResult.UserAvatar);
                MemoryStream memoryStream = new MemoryStream(base64_image_bytes, 0, base64_image_bytes.Length);
                memoryStream.Write(base64_image_bytes, 0, base64_image_bytes.Length);
                var image = Image.FromStream(memoryStream);
                OnUserScanQRCode?.Invoke(this, new UserScanQRCodeEvent()
                {
                    UserAvatarImage = image
                });

                CurrentStatus = ClientStatusType.QRCodeScaned;
            }
            else
            {
                CurrentStatus = ClientStatusType.GetUUID;
            }
        }

        private void HandleGetQRCode()
        {
            var QRCodeImg = client.GetQRCodeImage(mLoginSession);
            if (QRCodeImg != null)
            {
                CurrentStatus = ClientStatusType.Login;
                var wce = new GetQRCodeImageEvent()
                {
                    QRImage = QRCodeImg
                };
                OnGetQRCodeImage?.Invoke(this, wce);
                //OnEvent?.Invoke(this, wce);
            }
            else
            {
                CurrentStatus = ClientStatusType.GetUUID;
            }
        }

        private void HandleGetLoginSession()
        {
            IsLogin = false;
            mLoginSession = client.GetNewQRLoginSessionID();
            if (!string.IsNullOrWhiteSpace(mLoginSession))
            {
                CurrentStatus = ClientStatusType.GetQRCode;
            }
        }

        public Contact CreateContact(User user)
        {
            Contact contact = new Contact();
            contact.client = client;
            contact.ID = user.UserName;
            contact.NickName = user.NickName;
            contact.RemarkName = user.RemarkName;
            contact.HeadImgUrl = user.HeadImgUrl;
            contact.Uin = user.Uin+"";
            return contact;
        }

        public Contact[] GetGroupMembers(string groupID)
        {

            //获取群聊成员
            var batchResult = client.BatchGetContact(new string[] { groupID }, mPass_ticket, mBaseReq);
            if (batchResult == null || batchResult.BaseResponse.ret != 0) return null;

            List<Contact> members = new List<Contact>();
            foreach (var contact in batchResult.ContactList)
            {
                if (contact.UserName.StartsWith("@@")) continue;
                members.Add(CreateContact(contact));
            }

            return members.ToArray();

        }

        public void Logout()
        {
            if (!IsLogin || mMainLoopThread == null || !mMainLoopThread.IsAlive) return;
            client.Logout(mBaseReq.Skey, mBaseReq.Sid, mBaseReq.Uin);
            IsLogin = false;
            mContacts = null;
            mGroups = null;
            CurrentStatus = ClientStatusType.GetUUID;
        }

        private static string CreateNewDeviceID()
        {
            Random ran = new Random();
            int rand1 = ran.Next(10000, 99999);
            int rand2 = ran.Next(10000, 99999);
            int rand3 = ran.Next(10000, 99999);
            return string.Format("e{0}{1}{2}", rand1, rand2, rand3);
        }
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        public Image GetImage(string ImageUrl)
        {
            return client.GetImage(ImageUrl);
        }


        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public async Task<SendMsgResponse> SendMsgAsync(string ToUserName, string Content)
        {
            string FromUserName = Self.ID;
            SendMsgResponse val = await Task.Run(() =>
            {
                Msg msg = new Msg();
                msg.FromUserName = FromUserName;
                msg.ToUserName = ToUserName;
                msg.Content = Content;
                SendMsgResponse rep = client.SendMsg(msg, mPass_ticket, mBaseReq);
                return rep;
            });
            return val;
        }


    }
}
