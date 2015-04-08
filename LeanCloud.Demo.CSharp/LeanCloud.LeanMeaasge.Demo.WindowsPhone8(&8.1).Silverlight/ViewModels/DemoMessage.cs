using AVOSCloud.RealtimeMessageV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanCloud.LeanMeaasge.Demo.ViewModels
{
    public class DemoMessage : BaseItemViewModel
    {
        public DemoMessage()
        {

        }

        public DemoMessage(AVIMMessage avMessage)
        {
            var user = App.ViewModel.AllContacts.Where(t => t.UserId == avMessage.FromClientId).FirstOrDefault();
            this.From = user.Nickname;
            TextContent = avMessage.MessageBody;
            this.avMessage = avMessage;
        }

        private AVIMMessage avMessage;

        private string _from;
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
                NotifyPropertyChanged("From");
            }
        }

        private string _textContent;
        public string TextContent
        {
            get
            {
                return _textContent;
            }
            set
            {
                _textContent = value;
                NotifyPropertyChanged("TextContent");
            }
        }

        private DateTime _displayTime;
        public DateTime DisplayTime
        {
            get
            {
                return _displayTime;
            }
            set
            {
                _displayTime = value;
            }
        }

        private DateTime _receivedAt;
        public DateTime ReceivedAt
        {
            get
            {
                return _receivedAt;
            }
            set
            {
                _receivedAt = value;
                NotifyPropertyChanged("ReceivedAt");
            }
        }
    }
}
