using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LeanCloud.LeanMeaasge.Demo.Resources;
using AVOSCloud;
using GalaSoft.MvvmLight.Threading;
using AVOSCloud.RealtimeMessageV2;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeanCloud.LeanMeaasge.Demo.Control.Classes;
using System.Threading;

namespace LeanCloud.LeanMeaasge.Demo.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            this.ContactsListVM = new ObservableCollection<AlphaKeyGroup<DemoContact>>();
            this.SelectedConversationVM = new ConversationViewModel();
            this.AllContacts = new List<DemoContact>();
            this.ConversationsListVM = new ObservableCollection<ConversationViewModel>();
        }

        public ObservableCollection<ConversationViewModel> ConversationsListVM { get; private set; }//主页中会话列表绑定的数据源。
        public ObservableCollection<AlphaKeyGroup<DemoContact>> ContactsListVM { get; private set; }//主页中联系人列表绑定的数据源。

        public List<DemoContact> AllContacts { get; set; }//所有联系人的列表，用于逻辑操作，不用于绑定。
       
        public ConversationViewModel SelectedConversationVM { get; set; }//会话详情页中绑定的数据源。

        public AVIMClient CurrentClient { get; set; }//聊天中一个用户的唯一标识。

        public bool IsDataLoaded
        {
            get;
            set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            
            this.IsDataLoaded = true;
            LoadContactAsync();
            //LoadContact();
            LoadConversations();

        }
        public async void LoadContactAsync()
        {
            #region 获取所有系统中的用户，注：理论上这里是获取该用户的好友列表，以下代码仅仅是为了演示聊天的基础功能，所以默认是获取所有用户的列表。
            AVQuery<AVUser> query = new AVQuery<AVUser>();
            var users = await query.FindAsync();

            if (users != null)
            {
                foreach (var u in users)
                {
                    if (u.ObjectId != AVUser.CurrentUser.ObjectId)//当前用户不显示在联系人列表里面。
                        AllContacts.Add(new DemoContact(u));
                }
                ContactsListVM = AlphaKeyGroup<DemoContact>.CreateGroups(AllContacts, Thread.CurrentThread.CurrentUICulture,
                    (DemoContact s) =>
                    {
                        return s.Nickname;
                    }, true);
                NotifyPropertyChanged("ContactsListVM");
            }
            #endregion
        }

        public async void LoadConversations()
        {
            var cons = await await this.CurrentClient.ConnectAsync().ContinueWith<Task<IEnumerable<AVIMConversation>>>(t =>
               {
                   return this.CurrentClient.GetQuery().FindAsync();
               });
            if (cons != null)
            {
                foreach (var c in cons)
                {
                    var cListVM = new ConversationViewModel(c);
                    this.ConversationsListVM.Add(cListVM);
                    NotifyPropertyChanged("ConversationListVM");
                }
            }
        }
    }
}