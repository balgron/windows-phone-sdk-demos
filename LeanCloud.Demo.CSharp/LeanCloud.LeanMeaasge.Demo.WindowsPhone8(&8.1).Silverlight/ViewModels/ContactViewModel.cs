using AVOSCloud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanCloud.LeanMeaasge.Demo.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {

        public ContactViewModel()
        {
            this.Contacts = new ObservableCollection<DemoContact>();
        }

        public ObservableCollection<DemoContact> Contacts { get; private set; }

    }
}
