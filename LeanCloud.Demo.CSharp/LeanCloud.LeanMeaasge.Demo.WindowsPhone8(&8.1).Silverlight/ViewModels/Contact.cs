using AVOSCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanCloud.LeanMeaasge.Demo.ViewModels
{
    public class DemoContact : BaseItemViewModel
    {
        public DemoContact()
        {
 
        }
        public AVUser BindingUser { get; set; }
        public DemoContact(AVUser user)
        {
            this.BindingUser = user;
        }

        private string _userId;

        public string UserId
        {
            get
            {
                if (string.IsNullOrEmpty(_userId))
                {
                    _userId = BindingUser.ObjectId;
                }
                return _userId;
            }
            set
            {
                _userId = value;
                NotifyPropertyChanged("UserId");
            }
        }

        private string _username;
        public string Username
        {
            get
            {
                if (string.IsNullOrEmpty(_username))
                {
                    _username = BindingUser.Username;
                }
                return _username;
            }
            set
            {
                _username = value;
                NotifyPropertyChanged("Username");
            }
        }

        private string _nickname;
        public string Nickname
        {
            get 
            {
                if (string.IsNullOrEmpty(_nickname))
                {
                    _nickname = BindingUser.Username;
                }
                return _nickname;
            }
            set
            {
                _nickname = value;
                NotifyPropertyChanged("Nickname");
            }
        }
    }
}
