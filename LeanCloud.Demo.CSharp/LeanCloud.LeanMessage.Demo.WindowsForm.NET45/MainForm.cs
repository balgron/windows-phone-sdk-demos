using AVOSCloud;
using AVOSCloud.RealtimeMessageV2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDK.Test.WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            InitData();
        }
        #region 声明变量
        AVIMClient selfClient = null;//当前 Client
        BindingList<AVIMConversation> conversations = new BindingList<AVIMConversation>();//当前 Client 参与的对话
        BindingList<string> membersInConversation = new BindingList<string>();

        AVIMConversation currentConversation;

        Dictionary<string, BindingList<string>> historyDictionary;
        #endregion

        #region 初始化
        private void InitData()
        {
            historyDictionary = new Dictionary<string, BindingList<string>>();
        }
        #endregion

        #region 日志打印
        private void Log(string lonString)
        {
            txb_log.AppendText(lonString + "\r\n");
        }
        #endregion

        #region 从服务端加载已有的对话
        private async void LoadData()
        {
            var my_conversations = await selfClient.GetQuery().FindAsync();
            conversations = new BindingList<AVIMConversation>(my_conversations.ToList());

            lbx_conversations.DataSource = conversations;
            lbx_conversations.DisplayMember = "Name";

            Log("对话加载完毕。");
        }
        #endregion

        #region 绑定事件响应
        public void BindEventHandler()
        {
            if (selfClient != null)
            {
                selfClient.OnMessageReceieved += selfClient_OnMessageReceieved;
                selfClient.OnConversationMembersChanged += selfClient_OnConversationMembersChanged;
            }

            if (currentConversation != null)
            {
                currentConversation.OnTextMessageReceived += currentConversation_OnTextMessageReceived;
            }
        }

        private async void selfClient_OnConversationMembersChanged(object sender, AVIMOnMembersChangedEventArgs e)
        {

            if (e.AffectedType == AVIMConversationEventType.Joined)
            {
                await e.Conversation.FetchAsync();
                RefreshUI(() =>
                {
                    conversations.Add(e.Conversation);
                });
            }

            RefreshUI(() =>
            {
                MessageBox.Show(e.Conversation.ConversationId + ": " + e.AffectedMembers[0] + " " + e.AffectedType.ToString());
            });
        }

        private void selfClient_OnMessageReceieved(object sender, AVIMOnMessageReceivedEventArgs e)
        {
            var flag = currentConversation != null;
            if (flag)
            {
                flag = e.Conversation.ConversationId == currentConversation.ConversationId;
            }
            if (flag)
            {
                RefreshUI(() =>
                {
                    CacheMessage(e.Conversation, e.Message);
                });
            }
            else
            {
                CacheMessage(e.Conversation, e.Message);
            }

        }

        private void currentConversation_OnTextMessageReceived(object sender, AVIMTextMessage e)
        {
            var selection = (AVIMConversation)sender;

            var flag = currentConversation != null;
            if (flag)
            {
                flag = selection.ConversationId == currentConversation.ConversationId;
            }
            if (flag)
            {
                RefreshUI(() =>
                {
                    CacheMessage(selection, e);
                });
            }
            else
            {
                CacheMessage(selection, e);
            }

        }
        #endregion

        #region 缓存聊天记录
        private void CacheMessage(AVIMConversation conversation, AVIMMessage message)
        {
            if (historyDictionary == null)
            {
                historyDictionary = new Dictionary<string, BindingList<string>>();
            }
            var record = message.FromClientId + ": " + EscapedMessage(message);
            if (historyDictionary.ContainsKey(conversation.ConversationId))
            {
                historyDictionary[conversation.ConversationId].Add(record);
            }
            else
            {
                var history = new BindingList<string>();
                history.Add(record);
                historyDictionary.Add(conversation.ConversationId, history);
            }

            RefreshUI(() =>
            {
                Log("收到 " + message.FromClientId + " 发送的消息；");
            });

        }
        private string EscapedMessage(AVIMMessage message)
        {
            if (message is AVIMTextMessage)
            {
                return ((AVIMTextMessage)message).TextContent;
            }
            return message.MessageBody;
        }
        /// <summary>
        /// 更新 BindList 刷新 ListBox 的内容
        /// </summary>
        /// <param name="action"></param>
        private void RefreshUI(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    action();
                });
            }
            else
            {
                action();
            }
        }
        #endregion
        private async void btn_Connect_Click(object sender, EventArgs e)
        {
            selfClient = new AVIMClient(txb_clientId.Text.Trim());
            await selfClient.ConnectAsync();
            LoadData();
            BindEventHandler();
            Log(txb_clientId.Text.Trim() + " 登陆成功！");
        }
        private async void btn_createConversation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_friendId.Text.Trim()))
            {
                return;
            }
            if (selfClient != null)
            {
                var con_name = selfClient.ClientId + " & " + txb_friendId.Text.Trim();
                var conversation = await selfClient.CreateConversationAsync(txb_friendId.Text.Trim(), con_name);
                conversations.Add(conversation);

                Log("对话 " + con_name + " 创建成功；");
            }
        }
        private async void btn_send_Click(object sender, EventArgs e)
        {
            var selection = (AVIMConversation)lbx_conversations.SelectedItem;
            var tp = await selection.SendTextMessageAsync(txb_input.Text.Trim(),false,true);
            CacheMessage(this.currentConversation, tp.Item2);
            txb_input.Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_clearLog_Click(object sender, EventArgs e)
        {
            txb_log.ResetText();
        }

        private void lbx_conversations_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = (AVIMConversation)lbx_conversations.SelectedItem;
            currentConversation = selection;
            membersInConversation = new BindingList<string>(selection.MemberIds);
            lbx_members.DataSource = membersInConversation;
            if (!historyDictionary.ContainsKey(selection.ConversationId))
            {
                historyDictionary.Add(selection.ConversationId, new BindingList<string>());
            }

            lbx_history.DataSource = historyDictionary[selection.ConversationId];
            Log("当前选择对话为： " + selection.Name + ",对话最后更新的时间为：" + selection.UpdatedAt.Value.ToShortTimeString());
        }

        private async void btn_addMember_Click(object sender, EventArgs e)
        {
            await currentConversation.AddMembersAsync(txb_memberId.Text.Trim());
        }

        private async void btn_removeMember_Click(object sender, EventArgs e)
        {
            await currentConversation.RemoveMembersAsync(txb_memberId.Text.Trim());
        }

        private async void bnt_quitConversation_Click(object sender, EventArgs e)
        {
            await currentConversation.LeftAsync();
        }

        private async void brn_queryHistory_Click(object sender, EventArgs e)
        {
            var history = await currentConversation.QueryHistory(DateTime.Now, 100, this.currentConversation.CurrentClient.ClientId);
            //var count = history.Count();

            //MessageBox.Show(count.ToString());

            foreach (var hm in history)
            {
                CacheMessage(currentConversation, hm);
            }
        }



    }
}
