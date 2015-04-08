using AVOSCloud;
using AVOSCloud.RealtimeMessageV2;
using LeanCloud.LeanMeaasge.Demo.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanCloud.LeanMeaasge.Demo.ViewModels
{
    public class ConversationViewModel : BaseViewModel
    {
        public ConversationViewModel()
        {
            Messages = new ObservableCollection<DemoMessage>();
            ContactsInCoversation = new List<DemoContact>();
        }

        private void _avConversation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LastMesaageAt")
            {
                NotifyPropertyChangedOnUI("LastActiveTimeFormatString");
            }
        }
        public ConversationViewModel(AVIMConversation avConversation)
            : this()
        {
            this._avConversation = avConversation;
            this._avConversation.PropertyChanged += _avConversation_PropertyChanged;
            RefreshUI();
        }


        public void RefreshUI()
        {
            AVQuery<AVUser> batchQuery = new AVQuery<AVUser>().WhereContainedIn("objectId", AVConversation.MemberIds);
            batchQuery.FindAsync().ContinueWith(t =>
            {
                var result = t.Result;
                foreach (var u in result)
                {
                    if (App.ViewModel.CurrentClient.ClientId != u.ObjectId)
                    {
                        ContactsInCoversation.Add(new DemoContact(u));
                    }
                }
                NotifyPropertyChangedOnUI("ConversationPageTitle");
            });
            NotifyPropertyChangedOnUI("ContactsInCoversation");
        }

        private AVIMConversation _avConversation;
        public AVIMConversation AVConversation
        {
            get
            {
                return _avConversation;
            }
            set
            {
                _avConversation = value;
                RefreshUI();
            }
        }

        public string ConversationPageTitle
        {
            get
            {
                string title = string.Empty;

                if (!string.IsNullOrEmpty(this.AVConversation.Name))
                {
                    title = this.AVConversation.Name;
                }
                else
                {
                    if (ContactsInCoversation != null)
                    {
                        title = string.Join<string>(",", ContactsInCoversation.Select(c => c.Username).ToArray());
                    }
                }

                return title;
            }
        }

        public string LastActiveTimeFormatString
        {
            get
            {
                return this.AVConversation.LastMesaageAt == null ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : this.AVConversation.LastMesaageAt.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        private List<DemoContact> _contactsInCoversation;
        public List<DemoContact> ContactsInCoversation//contact 列表并不包含自己。
        {
            get
            {
                return _contactsInCoversation;
            }
            set
            {
                _contactsInCoversation = value;
            }
        }

        private ObservableCollection<DemoMessage> _messages;
        public ObservableCollection<DemoMessage> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
                NotifyPropertyChangedOnUI("Messages");
            }
        }

        public void LoadMessages()
        {

        }



    }
}
